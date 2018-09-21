# SYNOPSIS: This is a psake task file.

$projectTasks = (Join-Path $PSScriptRoot "tasks.psake.ps1");
if (Test-Path $projectTasks -PathType Leaf) { Include $projectTasks; }

Properties {
	# Constants
    $RootDir = "$(Split-Path $PSScriptRoot -Parent)";
	$ManifestJson = "$PSScriptRoot/manifest.json";
	$SecretsJson = "$PSScriptRoot/secrets.json";
	$ArtifactsDir = "$RootDir/artifacts";
	$MigrationsDir = "";
    $SolutionName = "";
    $ToolsDir = "";
    $TempDir = "";

	# Args
    $DeleteExistingFiles = $false;
	$FallbackBranch = "preview";
	$SkipCompilation = $false;
	$NonInteractive = $false;
	$Configuration = "";
    $Commit = $true;
	$Debug = $false;
	$Secrets = @{ };
	$Major = $false;
	$Minor = $false;
	$Branch = "";
}

Task "Default" -depends @("configure", "build", "test", "pack");

#region ----- COMPILATION -----

Task "Import-Dependencies" -alias "restore" -description "This task imports all build dependencies." -action {
	#  Importing all required powershell modules.
	foreach ($moduleId in @("Ncrement", "VSSetup", "Pester"))
	{
		$modulePath = "$ToolsDir/$moduleId/*/*.psd1";
		if (-not (Test-Path $modulePath))
		{
			Save-Module $moduleId -Path $ToolsDir;
		}
		Import-Module $modulePath -Force;
		Write-Host "  * imported the '$moduleId.$(Split-Path (Get-Item $modulePath).DirectoryName -Leaf)' powershell module.";
	}

    # Creating the 'manifest.json' file.
    if (-not (Test-Path $ManifestJson))
    {
        New-NcrementManifest $ManifestJson -Author $([System.Environment]::UserName) | Save-NcrementManifest $ManifestJson;
    }

    # Create the 'secrets.json' file
    if (-not (Test-Path $SecretsJson))
    {
        $credentials = '{ "jdbcurl": "jdbc:mysql://{0}/{1}", "userStore": "server=;user=;password=;database=;", "database": "server=;user=;password=;database=;" }';
        [string]::Format('{{ "nugetKey": null, "psGalleryKey": null, "local": {0}, "preview": {0} }}', $credentials) | Out-File $SecretsJson -Encoding utf8;
    }
}

Task "Increment-VersionNumber" -alias "version" -description "This task increments the project's version numbers" `
-depends @("restore") -action {
    $manifest = Get-NcrementManifest $ManifestJson;

    $releaseNotes = Join-Path $RootDir "releaseNotes.txt";
    if (Test-Path $releaseNotes)
    {
        $manifest.ReleaseNotes = Get-Content $releaseNotes | Out-String;
    }

    $oldVersion = $manifest | Convert-NcrementVersionNumberToString;
	$result = $manifest | Step-NcrementVersionNumber $Branch -Break:$Major -Feature:$Minor -Patch | Update-NcrementProjectFile "$RootDir/src" -Commit:$Commit;
    $newVersion = $manifest | Convert-NcrementVersionNumberToString;

	Write-Host "  * incremented version number from '$oldVersion' to '$newVersion'.";
	foreach ($file in $result.ModifiedFiles)
	{
		Write-Host "  * updated $(Split-Path $file -Leaf).";
	}
}

Task "Build-Solution" -alias "build" -description "This task compiles the solution." `
-depends @("restore") -precondition { return (-not $SkipCompilation); } -action {
	Write-Header "dotnet: msbuild";
	Exec { &dotnet msbuild $((Get-Item "$RootDir/*.sln").FullName) "/p:Configuration=$Configuration" "/verbosity:minimal"; }
}

Task "Run-Tests" -alias "test" -description "This task invoke all tests within the 'tests' folder." `
-depends @("restore") -action {
	try
	{
        # Running all MSTest assemblies.
        Push-Location $RootDir;
		foreach ($testFile in (Get-ChildItem "$RootDir/tests/*/bin/$Configuration" -Recurse -Filter "*Test.dll"))
		{
			Write-Header "dotnet: vstest '$($testFile.BaseName)'";
			Exec { &dotnet vstest $testFile.FullName; }
		}

		# Running all Pester scripts.
		$testsFailed = 0;
		foreach ($testFile in (Get-ChildItem "$RootDir/tests/*/" -Recurse -Filter "*tests.ps1" -ErrorAction Ignore))
		{
			Write-Header "Pester '$($testFile.BaseName)'";
			$results = Invoke-Pester -Script $testFile.FullName -PassThru;
			$testsFailed += $results.FailedCount;
            if ($results.FailedCount -gt 0) { throw "'$($testFile.BaseName)' failed '$($results.FailedCount)' test(s)."; }
		}
	}
	finally { Pop-Location; }
}

Task "Run-Benchmarks" -alias "benchmark" -description "This task runs all project benchmarks." `
-depends @("restore") -action {
	$benchmarkProject = Get-ChildItem "$RootDir/tests" -Recurse -Filter "*Benchmark.csproj" | Select-Object -First 1;

	if (Test-Path $benchmarkProject.FullName)
	{
		Write-Header "dotnet: clean + build";
        [string]$sln = Resolve-Path "$RootDir/*.sln";
		Exec { &dotnet clean $sln; }
		Exec { &dotnet build $sln --configuration Release; }

		try
		{
			$dll = Get-ChildItem "$($benchmarkProject.DirectoryName)/bin/Release" -Recurse -Filter "*Benchmark.dll" | Select-Object -First 1;
			Push-Location $dll.DirectoryName;

			Write-Header "dotnet: run benchmarks";
			Exec { &dotnet $dll.FullName; }

			# Copying benchmark results to report.
			$reportFile = Get-Item "$($benchmarkProject.DirectoryName)/*.md";
			if (Test-Path $reportFile)
			{
				$summary = Get-Item "$($dll.DirectoryName)/*artifacts*/*/*.md" | Get-Content | Out-String;
				$report = $reportFile | Get-Content | Out-String;
				$match = [Regex]::Match($report, '(?i)#+\s+(Summary|Results?|Report)');
				$report = $report.Substring(0, ($match.Index + $match.Length));
				"$report`r`n`r`n$summary" | Out-File $reportFile -Encoding utf8;
				Get-Item "$($dll.DirectoryName)/*artifacts*/*/*.html" | Invoke-Item;
			}
		}
		finally { Pop-Location; }
	}
    else { Write-Host " no benchmarks found." -ForegroundColor Yellow; }
}

#endregion

#region ----- DB Migration -----

Task "Rebuild-FlywayLocalDb" -alias "rebuild-db" -description "This task rebuilds the local database using flyway." `
-depends @("restore") -action{
	[string]$flyway = Get-Flyway;
	$credential = Get-Secret "local";
	Assert (-not [string]::IsNullOrEmpty($credential.database)) "A connection string for your local database was not provided.";

	$db = [ConnectionInfo]::new($credential, $credential.database);
	Write-Header "flyway: clean ($($db.ToFlywayUrl()))";
	Exec { &$flyway clean $db.ToFlywayUrl() $db.ToFlyUser() $db.ToFlyPassword(); }
	Write-Header "flyway: migrate ($($db.ToFlywayUrl()))";
	Exec { &$flyway migrate $db.ToFlywayUrl() $db.ToFlyUser() $db.ToFlyPassword() $([ConnectionInfo]::ConvertToFlywayLocation($MigrationDirectory)); }
	Exec { &$flyway info $db.ToFlywayUrl() $db.ToFlyUser() $db.ToFlyPassword() $([ConnectionInfo]::ConvertToFlywayLocation($MigrationDirectory)); }
}

#endregion

#region ----- PUBLISHING -----

Task "Publish-NuGetPackages" -alias "push-nuget" -description "This task publish all nuget packages to nuget.org." `
-precondition { return Test-Path $ArtifactsDir -PathType Container } `
-depends @("restore") -action {
	$apiKey = Get-Secret "nugetKey" -Assert;

	foreach ($nupkg in (Get-ChildItem $ArtifactsDir -Recurse -Filter "*.nupkg"))
	{
		Write-Header "dotnet: nuget push '$($nupkg.Name)'";
		Exec { &dotnet nuget push $nupkg.FullName --source "https://api.nuget.org/v3/index.json" --api-key $apiKey; }
	}
}

Task "Publish-PowershellModules" -alias "push-ps" -description "This task publish all powershell modules to powershellgallery.org." `
-depends @("restore") -action {
    $apiKey = Get-Secret "psGalleryKey" -Assert;

    foreach ($psd1 in (Get-ChildItem $ArtifactsDir -Recurse -Filter "*.psd1"))
    {
        if (Test-ModuleManifest $psd1.FullName)
        {
            Write-Header "PS: publish '$($psd1.BaseName)'";
            Publish-Module -Path $psd1.DirectoryName -NuGetApiKey $apiKey;
        }
    }
}

Task "Publish-Database" -alias "push-db" -description "This task publishes the application database to the appropriate host." `
-depends @("restore", "rebuild-db") -action {
	$credentials = $null;
	foreach ($key in @($Branch, $FallbackBranch))
	{
		$credentials = Get-Secret $key;
		if (-not [string]::IsNullOrEmpty($credentials)) { break; }
	}
	Assert(-not [string]::IsNullOrEmpty($credentials.database)) "Unable to update database because no connection info was provided for the '$Branch' branch. Verify the secrets.json file.";
	$db = [ConnectionInfo]::new($credentials, $credentials.database);
	
    Write-Header "flyway: migrate ($($db.ToFlywayUrl()))";
	[string]$flyway = Get-Flyway;
	Exec { &$flyway migrate $db.ToFlywayUrl() $db.ToFlyUser() $db.ToFlyPassword() $([ConnectionInfo]::($MigrationDirectory)); }
	Exec { &$flyway info $db.ToFlywayUrl() $db.ToFlyUser() $db.ToFlyPassword() $([ConnectionInfo]::ConvertToFlywayLocation($MigrationDirectory)); }
}

Task "Publish-Websites" -alias "push-web" -description "This task publish all websites to their respective host." `
-precondition { return Test-Path $ArtifactsDir -PathType Container } `
-depends @("restore")  -action {
	foreach ($package in (Get-ChildItem $ArtifactsDir -Recurse -Filter "web-*"))
	{
		$id = $package.BaseName.TrimStart("web-");

		$credentials = $null;
		$errorMsg = "Unable to publish '$($package.Name)' because the web-host password was not defined. Verify the secrets.json.";
		foreach ($key in @($Branch, $FallbackBranch))
		{
			$credentials = Get-Secret $key;
			if (($credentials -eq $null) -or ($credentials.PSObject.Properties.Match($id) -eq $null)) { continue; }
			$webHost = [ConnectionInfo]::new($credentials, $credentials.$id);
			if ([string]::IsNullOrEmpty($webHost.Password)) { throw $errorMsg; } else { break; }
		}

		[string]$publishData = Get-ChildItem $PSScriptRoot -Recurse -Filter "*$id-$key.publishsettings" | Select-Object -First 1 -ExpandProperty FullName;
		if ([string]::IsNullOrEmpty($publishData)) { throw "Unable to publish '$($package.BaseName)' because a respective .publshsetting file do not exist."; }
		else
		{
            [string]$waws = Get-WAWSDeploy;
            $del = $DeleteExistingFiles | CND "/deleteexistingfiles" "";
			Exec { &$waws $package.FullName $publishData /password $webHost.Password /appoffline $del; }
			if (-not $NonInteractive)
			{
				[xml]$doc = Get-Content $publishData;
				$appUrl = $doc.SelectSingleNode("//publishProfile[@destinationAppUrl]").Attributes["destinationAppUrl"].Value;
                if (-not [string]::IsNullOrEmpty($appUrl))
                {
                    Start-Process $appUrl;
                }
			}
		}
	}
}

Task "Tag-Release" -alias "tag" -description "This task tags the last commit with the version number." `
-depends @("restore") -action {
    $version = Get-NcrementManifest $ManifestJson | Convert-NcrementVersionNumberToString;
    if ($Branch -ieq "master")
    {
        Exec { &git tag v$version | Out-Null; }
        Exec { &git push "origin" | Out-Null; }
        Exec { &git push "origin" --tags | Out-Null; }
    }
    else
    {
        Exec { &git push "origin" | Out-Null; }
    }
}

#endregion

#region ----- HELPER FUNCTIONS -----

function Get-MSBuild([string]$version = "*")
{
    $instance = Get-VSSetupInstance -All | Select-VSSetupInstance -Latest;
    return (Join-Path $instance.InstallationPath "msbuild/$version/bin/msbuild.exe" | Resolve-Path) -as [string];
}

function Get-Dotfuscator()
{
    $instance = Get-VSSetupInstance -All | Select-VSSetupInstance -Latest;
    $dotfuscator = Join-Path $instance.InstallationPath "Common7/IDE/Extensions/PreEmptiveSolutions/DotfuscatorCE/dotfuscatorCLI.exe";

    return (Test-Path $dotfuscator) | CND $dotfuscator "";
}

function Get-Flyway([string]$version="5.1.4")
{
	[string]$flyway = Join-Path $ToolsDir "flyway/$version/flyway";
    [string]$url = "http://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/{1}/flyway-commandline-{1}-{0}-x64.zip";
    switch ([Environment]::OSVersion.Platform)
    {
        default 
        {
            $flyway = "$flyway.cmd";
            $url = [string]::Format($url, "windows", $version);
        }
        $([PlatformId]::Unix)
        {
            $url = [string]::Format($url, "linux", $version);
        }
        $([PlatformId]::MacOSX)
        {
            $url = [string]::Format($url, "macosx", $version);
        }
    }

	if (-not (Test-Path $flyway))
	{
		$zip = Join-Path $TempDir "flyway-$version.zip";
		try
		{
			Invoke-WebRequest $url -OutFile $zip;

			$dest = Join-Path $ToolsDir "flyway";
			Expand-Archive $zip -DestinationPath $dest -Force;
			Get-Item "$dest/*" | Rename-Item -NewName $version;
		}
		finally { if (Test-Path($zip)) { Remove-Item $zip -Force; } }
	}

    return $flyway;
}

function Get-WAWSDeploy([string]$version="1.8.0")
{
	[string]$waws = Join-Path $ToolsDir "WAWSDeploy/$version/tools/WAWSDeploy.exe";

	if (-not (Test-Path $waws))
	{
		$zip = Join-Path $TempDir "wawsdeploy.zip";
		try
		{
			Invoke-WebRequest "https://chocolatey.org/api/v2/package/WAWSDeploy/$version" -OutFile $zip;
			Expand-Archive $zip -DestinationPath (Join-Path $ToolsDir "WAWSDeploy/$version") -Force;
		}
		finally { if (Test-Path $zip) { Remove-Item $zip -Force; } }
	}

    return $waws;
}

function Get-Nuget([string]$version = "latest")
{
	[string]$nuget = Join-Path $ToolsDir "Nuget/$version/nuget.exe";

	if (-not (Test-Path $nuget))
	{
		$dir = Split-Path $nuget -Parent;
		if (-not (Test-Path $dir)) { New-Item $dir -ItemType Directory | Out-Null; }
		Invoke-WebRequest "https://dist.nuget.org/win-x86-commandline/$version/nuget.exe" -OutFile $nuget;
	}

	return $nuget;
}

function Get-Secret([Parameter(ValueFromPipeline)][string]$key, [string]$customMsg = "", [switch]$Assert)
{
	$value = $Secrets.ContainsKey($key) | CND $Secrets.$key $null;
	if ([string]::IsNullOrEmpty($value) -and (Test-Path $SecretsJson))
	{
		$value = Get-Content $SecretsJson | Out-String | ConvertFrom-Json | Select-Object -ExpandProperty $key -ErrorAction Ignore;
	}

	if ($Assert) { Assert (-not [string]::IsNullOrEmpty($value)) ([string]::IsNullOrEmpty($customMsg) | CND "A '$key' property was not specified. Provided a value via the `$Secrets parameter eg. `$Secrets=@{'$key'='your_sercret_value'}" $customMsg); }
	return $value;
}

function Write-Header([string]$Title = "", [int]$length = 70, [switch]$AsValue)
{
	$header = [string]::Join('', [System.Linq.Enumerable]::Repeat('-', $length));
	if (-not [String]::IsNullOrEmpty($Title))
	{
		$header = $header.Insert(4, " $Title ");
		if ($header.Length -gt $length) { $header = $header.Substring(0, $length); }
	}

	if ($AsValue) { return $header; } else { Write-Host ''; Write-Host $header -ForegroundColor DarkGray; Write-Host ''; }
}

function CND([Parameter(Mandatory, ValueFromPipeline)][bool]$Condition, [Parameter(Position = 0)]$TrueValue, [Parameter(Position = 1)]$FalseValue)
{
	if ($Condition) { return $TrueValue; } else { return $FalseValue; }
}

Class ConnectionInfo {
	ConnectionInfo($dbNode, [string]$connectionString) {
		if ([string]::IsNullOrEmpty($connectionString)) { throw "The '`$connectionString' parameter cannot be null or empty."; }

		$this.Host = [Regex]::Match($connectionString, '(?i)(server|data source|host)=(?<value>[^;]+);?').Groups["value"].Value;
		$this.User = [Regex]::Match($connectionString, '(?i)(user|usr)=(?<value>[^;]+);?').Groups["value"].Value;
		$this.Password = [Regex]::Match($connectionString, '(?i)(password|pwd)=(?<value>[^;]+);?').Groups["value"].Value;
		$this.Resource = [Regex]::Match($connectionString, '(?i)(database|catalog)=(?<value>[^;]+);?').Groups["value"].Value;
		$this.ConnectionString = $connectionString;
		$this.JDBCUrl = $dbNode.JDBCUrl;
	}

	[string]$Host;
	[string]$User;
	[string]$Password;
	[string]$Resource;
	[string]$ConnectionString;

    [string]$JDBCUrl;

	[string] ToFlywayUrl(){
		return "-url=$([string]::Format($this.JDBCUrl, $this.Host, $this.Resource))";
	}

	[string] ToFlyUser() {
		return "-user=$($this.User)";
	}

	[string] ToFlyPassword() {
		return "-password=$($this.Password)";
	}

	static [string] ConvertToFlywayLocation([string]$path) {
		return "-locations=filesystem:$path";
	}
}

#endregion

FormatTaskName "$(Write-Header -AsValue)`r`n  {0}`r`n$(Write-Header -AsValue)";