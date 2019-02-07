


function CND
{
	Param(
		[Parameter(Mandatory, ValueFromPipeline)][bool]$Condition,
		[Parameter(Position = 0)]$TrueValue,
		[Parameter(Position = 1)]$FalseValue
	)
	if ($Condition) { return $TrueValue; } else { return $FalseValue; }
}

function Get-Secret
{
	Param(
		[Parameter(Mandatory)]
		[string]$SecretsFilePath,

		[Parameter(Mandatory)]
		[string]$Key
	)

	$result = $null;
	$secrets = Get-Content $SecretsFilePath | ConvertFrom-Json;
	foreach ($propertyName in $Key.Split(@('.', '-', '/', '\')))
	{
		if  ($secrets | Get-Member $propertyName)
		{
			$result = $secrets.$propertyName;
		}
		else { throw "Could not find property '$Key' at '$(Split-Path $SecretsFilePath -Leaf)'."; }
	}

	return $result;
}

function Publish-NugetPackage
{
	Param(
		[Parameter(Mandatory)]
		$ApiKey,

		[Parameter(Mandatory)]
		$ArtifactsFolder
	)

	foreach ($nupkg in (Get-ChildItem $ArtifactsFolder -Recurse -Filter "*.nupkg"))
	{
		Write-Header "dotent: nuget push";
		Exec { &dotnet nuget push $nupkg.FullName --source "https://api.nuget.org/v3/index.json" --api-key $ApiKey; }
	}
}

function Remove-GeneratedSolutionItems
{
	Param(
		$AdditionalItems = @(),

		[Parameter(ValueFromPipeline)]
		[string]$SolutionFolder
	)
	PROCESS
	{
		$itemsToBeRemoved =  @("artifacts") + $AdditionalItems | Select-Object -Unique;
		foreach ($target in $itemsToBeRemoved)
		{
			[string]$itemPath = Join-Path $SolutionFolder $target;
			if (Test-Path $itemPath)
			{
				Remove-Item $itemPath -Recurse -Force;
				Write-Host "  * removed '$(Split-Path $itemPath -Leaf)'.";
			}
		}
	}
}

function Remove-GeneratedProjectItems
{
	Param(
		$AdditionalItems = @(),

		[Parameter(ValueFromPipeline)]
		[IO.FileInfo]$ProjectFile
	)
	PROCESS
	{
		$itemsToBeRemoved =  @("bin", "obj") + $AdditionalItems | Select-Object -Unique;
		foreach ($target in $itemsToBeRemoved)
		{
			$itemPath = Join-Path $ProjectFile.DirectoryName $target;
			if (Test-Path $itemPath)
			{
				Remove-Item $itemPath -Recurse -Force;
				Write-Host "  * removed '.../$($ProjectFile.Directory.Name)/$($target)'.";
			}
		}
	}
}

function Install-Dotfuscator
{
	Install-PSModules @("VSSetup");
    $instance = Get-VSSetupInstance -All | Select-VSSetupInstance -Latest;
    $dotfuscator = Join-Path $instance.InstallationPath "Common7/IDE/Extensions/PreEmptiveSolutions/DotfuscatorCE/dotfuscatorCLI.exe";

    return (Test-Path $dotfuscator) | CND $dotfuscator "";
}

function Install-Flyway([Parameter(Mandatory)][string]$InstallationFolder,  [string]$version="5.1.4")
{
	[string]$flyway = Join-Path $InstallationFolder "flyway/$version/flyway";
    [string]$url = "http://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/{1}/flyway-commandline-{1}-{0}-x64.zip";
    switch ([Environment]::OSVersion.Platform)
    {
        default
        {
            $flyway = "$flyway.cmd";
            $url = [string]::Format($url, "windows", $version);
        }
        $([PlatformId]::Unix) { $url = [string]::Format($url, "linux", $version); }
        $([PlatformId]::MacOSX) { $url = [string]::Format($url, "macosx", $version); }
    }

	if (-not (Test-Path $flyway))
	{
		$zip = Join-Path ([IO.Path]::GetTempPath()) "flyway-$version.zip";
		try
		{
			Invoke-WebRequest $url -OutFile $zip;

			$dest = Join-Path $InstallationFolder "flyway";
			Expand-Archive $zip -DestinationPath $dest -Force;
			Get-Item "$dest/*" | Rename-Item -NewName $version;
		}
		finally { if (Test-Path($zip)) { Remove-Item $zip -Force; } }
	}

    return $flyway;
}

function Install-MSBuild([string]$version = "*")
{
	Install-PSModules @("VSSetup");
    $instance = Get-VSSetupInstance -All | Select-VSSetupInstance -Latest;
    return (Join-Path $instance.InstallationPath "msbuild/$version/bin/msbuild.exe" | Resolve-Path) -as [string];
}

function Install-PSModules
{
	Param(
		[Parameter(Mandatory)]
		[string]$installationFolder, 
		
		$modules = @()
	)

	foreach ($moduleId in $Modules)
	{
		$modulePath = Join-Path $installationFolder "$moduleId/*/*.psd1";
		if (-not (Test-Path $modulePath)) { Save-Module $moduleId -Path $installationFolder; }
		Import-Module $modulePath -Force;
		Write-Host "  * imported the '$moduleId.$(Split-Path (Get-Item $modulePath).DirectoryName -Leaf)' powershell module.";
	}
}

function Install-WAWSDeploy([Parameter(Mandatory)][string]$InstallationFolder, [string]$version="1.8.0")
{
	$zip = Join-Path ([IO.Path]::GetTempPath()) "wawsdeploy.zip";
	[string]$waws = Join-Path $InstallationFolder "WAWSDeploy/$version/tools/WAWSDeploy.exe";

	if (-not (Test-Path $waws))
	{
		try
		{
			Invoke-WebRequest "https://chocolatey.org/api/v2/package/WAWSDeploy/$version" -OutFile $zip;
			Expand-Archive $zip -DestinationPath (Join-Path $InstallationFolder "WAWSDeploy/$version") -Force;
		}
		finally { if (Test-Path $zip) { Remove-Item $zip -Force; } }
	}

    return $waws;
}

function Invoke-MSBuild
{
	Param(
		[Parameter(Mandatory)]
		[string]$Configuration,

		$PackageSources = @(),

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$SolutionFile
	)

	PROCESS
	{
		Write-Header "dotnet: build '($SolutionFile.BaseName)'";
		&dotnet restore $SolutionFile.FullName --verbosity minimal;
		&dotnet build $SolutionFile.FullName --configuration $Configuration --verbosity minimal;
		if ($LASTEXITCODE -ne 0) { throw "$($SolutionFile.Name) build failed."; }
	}

}

function Invoke-MSTest
{
	Param(

		$Configuration = "Debug",

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$ProjectFile
	)
	PROCESS
	{
		try
		{
			Write-Header "dotnet: test '$($ProjectFile.Name)'";
			Push-Location $ProjectFile.DirectoryName;
			&dotnet test $ProjectFile.FullName --configuration $Configuration --verbosity minimal;
			if ($LASTEXITCODE -ne 0) { throw "$($ProjectFile.Name) tests failed."; }
		}
		finally { Pop-Location; }
	}
}

function Invoke-NugetPack
{
	Param(
		[Parameter(Mandatory)]
		[string]$ArtifactsFolder,

		[string]$Configuration,

		[string]$Version,

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$ProjectFile
	)
	PROCESS
	{
		try
		{
			Write-Header "dotnet: pack '$($ProjectFile.BaseName)'";
			&dotnet pack $ProjectFile.FullName --output $ArtifactsFolder --configuration $Configuration /p:PackageVersion=$Version;
			if ($LASTEXITCODE -ne 0) { throw "dotnet: pack $($ProjectFile.Name) cause an error."; }
		}
		finally { Pop-Location; }
	}
}

function Invoke-PowershellTest
{
	Param(
		$InstallationFolder,

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$TestScript
	)

	BEGIN { Install-PSModules $InstallationFolder @("Pester"); }
	PROCESS
	{
		Write-Header "pester: $($TestScript.Name)";
		$results = Invoke-Pester -Script $TestScript.FullName -PassThru;
		if ($results.FailedCount -gt 0) { throw "Test: $($TestScript.Name) Failed: $($results.FailedCount)"; }
	}
}

function New-GitTag
{
	Param(
		[Parameter(Mandatory)]
		[string]$CurrentBranch,

		[Parameter(Mandatory, ValueFromPipeline)]
		[string]$Version
	)

	if (($CurrentBranch -eq "master") -and (Test-Git))
	{
		Write-Header "git tag $Version";
		&git tag v$Version;
		if ($LASTEXITCODE -ne 0) { throw "git tag failed."; }
	}
	else { Write-Warning "The current branch ($CurrentBranch) is not master or the git is not installed on this machine."; }
}

function Publish-NugetPackage
{
	Param(
		[Parameter(Mandatory)]
		[string]$SecretsFilePath,

		[Parameter(Mandatory)]
		[string]$Key,

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$PackageFile
	)

	BEGIN { $apikey = Get-Secret $SecretsFilePath $Key; }
	PROCESS
	{
		Write-Header "dotnet: nuget-push '$($PackageFile.Name)'";
		&dotnet nuget push $PackageFile.FullName --source "https://api.nuget.org/v3/index.json" --api-key $apiKey;
		if ($LASTEXITCODE -ne 0) { throw "nuget-push $($PackageFile.Name) failed."; }
	}
}

function Test-Git
{
	return (&git version | Out-String) -match '(?i)(v|ver|version)\s*\d+\.\d+\.\d+';
}

function Write-FormatedMessage
{
	Param(
		[Parameter(Mandatory)]
		[string]$FormatString,

		[Parameter(ValueFromPipeline)]
		$InputObject
	)

	PROCESS
	{
		if ($InputObject)
		{
			$value = $InputObject;
			if ($InputObject | Get-Member "Name") { $value = $InputObject.Name; }
			Write-Host ([string]::Format($FormatString, @($value)));
		}
	}
}

function Write-Header
{
	Param([string]$Title = "", [int]$length = 70, [switch]$ReturnAsString)

	$header = [string]::Join('', [System.Linq.Enumerable]::Repeat('-', $length));
	if (-not [String]::IsNullOrEmpty($Title))
	{
		$header = $header.Insert(4, " $Title ");
		if ($header.Length -gt $length) { $header = $header.Substring(0, $length); }
	}

	if ($ReturnAsString) { return $header; } else { Write-Host ''; Write-Host $header -ForegroundColor DarkGray; Write-Host ''; }
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