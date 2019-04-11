function Add-XsdAnnotation
{
	Param(
		[Parameter(Mandatory)]
		[ValidateNotNull()]
		[xml]$XmlDocument,

		[Parameter(Mandatory)]
		[ValidateNotNull()]
		$TargetNode,

		[Parameter(Mandatory)]
		[ValidateNotNull()]
		[string]$Text,

		[string]$XmlNamespace = "http://www.w3.org/2001/XMLSchema"
	)

	if ($TargetNode.annotation.documentation)
	{
		$TargetNode.annotation.documentation = $Text;
		return $TargetNode.annotation.documentation;
	}
	else
	{
		$annotation = $XmlDocument.CreateElement("xs", "annotation", $XmlNamespace);
		$documentation = $XmlDocument.CreateElement("xs", "documentation", $XmlNamespace);
		$documentation.InnerText = $Text;

		$annotation.AppendChild($documentation) | Out-Null;
		$TargetNode.PrependChild($annotation) | Out-Null;
		return $annotation;
	}
}

function CND
{
	Param(
		[Parameter(Mandatory, ValueFromPipeline)][bool]$Condition,
		[Parameter(Position = 0)]$TrueValue,
		[Parameter(Position = 1)]$FalseValue
	)
	if ($Condition) { return $TrueValue; } else { return $FalseValue; }
}

function Convert-MixedXmlElementToText
{
	[OutputType([string])]
	Param(
		[Parameter(Mandatory)]
		$XmlNode
	)

	if ($XmlNode.GetType() -eq [string]) { return $XmlNode.Trim(); }
	else
	{
		$text = [System.Text.StringBuilder]::new();
		foreach ($node in $XmlNode.ChildNodes)
		{
			if (($node.LocalName -eq "#text"))
			{
				$text.Append($node.InnerText) | Out-Null;
			}
			elseif ($node.HasAttributes)
			{
				foreach ($attr in $node.Attributes)
				{
					$text.Append($attr.Value) | Out-Null;
				}
			}
		}
		return $text.ToString().Trim();
	}

	return "";
}

function Expand-NugetPackage
{
	Param(
		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$Destination,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$NupkgFile
	)

	PROCESS
	{
		$zip = [IO.Path]::ChangeExtension($NupkgFile.FullName, ".zip");
		Copy-Item $NupkgFile.FullName -Destination $zip;
		Expand-Archive $zip -DestinationPath $Destination;
		if (Test-Path $zip) { Remove-Item $zip; }
	}
}

function Export-XmlSchemaFromDll
{
	Param(
		[Parameter(Mandatory)]
		[ValidateSet("Debug", "Release")]
		[string]$Configuraiton,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$FullyQualifiedTypeName,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$ProjectFile,

		[switch]$Force
	)

	BEGIN { [string]$xsd = Test-XsdExe; }
	PROCESS
	{
		[string]$dll = Join-Path $ProjectFile.DirectoryName "bin/$Configuraiton/**/publish/*$($ProjectFile.BaseName).dll" | Resolve-Path;
		if (($dll -eq $null) -or (-not (Test-Path $dll)) -or $Force)
		{
			Write-Header "dotnet: publish '$($ProjectFile.BaseName)'";
			Invoke-Tool { &dotnet publish $ProjectFile.FullName --configuration $Configuraiton; }
			$dll = Join-Path $ProjectFile.DirectoryName "bin/$Configuraiton/**/publish/*$($ProjectFile.BaseName).dll" | Resolve-Path;
			Write-Header;
		}

		try
		{
			Split-Path $dll -Parent | Push-Location;
			Invoke-Tool { &$xsd $dll /type:$FullyQualifiedTypeName | Out-Null; }
			$generatedSchema = Join-Path $PWD "schema*.xsd" | Get-Item;
			Write-Host "  * generated '$($ProjectFile.BaseName)::$($FullyQualifiedTypeName)' xml-schema.";

			$dllDocumentation = ([IO.Path]::ChangeExtension($dll, ".xml"));
			if (Test-Path $dllDocumentation)
			{
				Merge-DllDocumentationWithXSD $generatedSchema $dllDocumentation;
				Write-Host "  * merged '$($ProjectFile.BaseName)' xml-documentation with it's xml-schema.";
			}

			[string]$destination = ([IO.Path]::ChangeExtension($ProjectFile.FullName, ".xsd"));
			Copy-Item $generatedSchema $destination -Force;
			Write-Host "  * added '.../$($ProjectFile.Directory.Name)/$(Split-Path $destination -Leaf)' to project."
		}
		finally { Pop-Location; }
	}
}

function Get-MSBuildElement
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$ProjectFile,

		[Parameter(Mandatory)]
		[string]$XPath
	)

	[xml]$proj = Get-Content $ProjectFile;
	$ns = [System.Xml.XmlNamespaceManager]::new($proj.NameTable);
	$ns.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

	return $proj.SelectSingleNode($XPath, $ns);
}

function Get-Secret
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$SecretsFilePath,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
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
		else { throw "Could not find property '$Key' in '$(Split-Path $SecretsFilePath -Leaf)'."; }
	}

	return $result;
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
	if (Test-Path $InstallationFolder -PathType Container) { throw "Could not find folder at '$InstallationFolder'."; }

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

function Install-PSModules
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$installationFolder,

		$Modules = @()
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

function Invoke-BenchmarkDotNet
{
	Param(
		[string]$Filter = "*",

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$ProjectFile,

		[switch]$DryRun
	)

	PROCESS
	{
		$job = "Default";
		if ($DryRun) { $job = "Dry"; }

		Invoke-Tool { &dotnet build $ProjectFile.FullName --configuration "Release"; }
		$dll = Join-Path $ProjectFile.DirectoryName "bin/Release/*/*$($ProjectFile.BaseName).dll" | Get-Item | Select-Object -Last 1;
		try
		{
			Push-Location $ProjectFile.DirectoryName;
			Write-Header "benchmark: '$($ProjectFile.BaseName)'";
			Invoke-Tool { &dotnet $dll.FullName --filter $Filter --job $job | Write-Host; }
			$report = Join-Path $PWD "BenchmarkDotNet.Artifacts/results" | Get-ChildItem -File -Filter "*vbench*.html" | Select-Object -First 1 -ExpandProperty FullName | Invoke-Item;
		}
		finally { Pop-Location; }
	}
}

function Invoke-MochaTest
{
	Param(
		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$ProjectFile
	)

	PROCESS
	{
		$packageJson = Join-Path $ProjectFile.DirectoryName "package.json";
		if (Test-Path $packageJson)
		{
			try
			{
				Push-Location $ProjectFile.DirectoryName;
				$mocha = Join-Path $ProjectFile.DirectoryName "node_modules\mocha\bin\mocha";
				if (-not (Test-Path $mocha -PathType Leaf))
				{
					Write-Header "npm: insall";
					Invoke-Tool { &npm install; }
				}

				if (Test-Path $mocha -PathType Leaf)
				{
					foreach ($testScript in (Get-ChildItem -Recurse -Filter "*.test.js"))
					{
						Write-Header "mocha: '$($testScript.BaseName)'";
						Invoke-Tool { &node $mocha $testScript.FullName; }
					}
				}
				else { Write-Warning "Could not find the 'mocha' module; check if it is missing from the 'package.json' file."; }
			}
			finally { Pop-Location; }
		}
		else { Write-Warning "The '$($ProjectFile.BaseName)' is missing a 'package.json' file."; }
	}
}

function Invoke-MSBuild
{
	Param(
		[Parameter(Mandatory)]
		[ValidateSet("Debug", "Release")]
		[string]$Configuration,

		$PackageSources = @(),

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_})]
		[IO.FileInfo]$SolutionFile
	)

	PROCESS
	{
		Write-Header "dotnet: build '$($SolutionFile.BaseName)'";
		Invoke-Tool{ &dotnet restore $SolutionFile.FullName --verbosity minimal; }
		Invoke-Tool { &dotnet build $SolutionFile.FullName --configuration $Configuration --verbosity minimal; }
	}
}

function Invoke-MSBuild15
{
	Param(
		[Parameter(Mandatory)]
		[string]$InstallationFoler,

		[Parameter(Mandatory)]
		[string]$Configuration,

		$PackageSources = @(),

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$SolutionFile
	)
	PROCESS
	{
		$msbuild = Resolve-MSBuildPath $InstallationFoler;
		Write-Header "msbuild '$($SolutionFile.BaseName)'";
		Invoke-Tool { &$msbuild $SolutionFile.FullName /t:restore /p:Configuration=$Configuration /verbosity:minimal; };
		Invoke-Tool { &$msbuild $SolutionFile.FullName /p:Configuration=$Configuration /verbosity:minimal; };
	}
}

function Invoke-MSTest
{
	Param(
		[Parameter(Mandatory)]
		[ValidateSet("Debug", "Release")]
		$Configuration,

		[Parameter(Mandatory, ValueFromPipeline)]
		[IO.FileInfo]$ProjectFile
	)
	PROCESS
	{
		try
		{
			Push-Location $ProjectFile.DirectoryName;
			Write-Header "dotnet: test '$($ProjectFile.Name)'";
			Invoke-Tool { &dotnet test $ProjectFile.FullName --configuration $Configuration --verbosity minimal; };
		}
		finally { Pop-Location; }
	}
}

function Invoke-NShellit
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_ -PathType Container})]
		[string]$ArtifactsFolder,

		[Parameter(Mandatory)]
		[ValidateSet("Debug", "Release")]
		[string]$Configuration,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_})]
		[IO.FileInfo]$ProjectFile
	)

	PROCESS
	{
		Write-Header "dotnet: publish '$($ProjectFile.BaseName)'";
		Invoke-Tool { &dotnet publish $ProjectFile.FullName --configuration $Configuration --verbosity minimal; }

		# Moving the powershell module.
		$psd1 = Join-Path $ProjectFile.DirectoryName "bin/$Configuration/*/NShellit/*/*/*.psd1" | Get-Item;
		Copy-Item $psd1.DirectoryName -Destination $ArtifactsFolder -Recurse;
	}
}

function Invoke-NugetPack
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$ArtifactsFolder,

		[Parameter(Mandatory)]
		[ValidateSet("Debug", "Release")]
		[string]$Configuration,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$Version,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$ProjectFile
	)
	PROCESS
	{
		try
		{
			Write-Header "dotnet: pack '$($ProjectFile.BaseName)' $Version";
			Invoke-Tool { &dotnet pack $ProjectFile.FullName --output $ArtifactsFolder --configuration $Configuration /p:PackageVersion=$Version; }
		}
		finally { Pop-Location; }
	}
}

function Invoke-Tool
{
    param(
        [Parameter(Mandatory)]
        [scriptblock]$Action,

        [string]$WorkingDirectory = $null
    )

    if ($WorkingDirectory) { Push-Location -Path $WorkingDirectory; }

	try
	{
		$global:lastexitcode = 0;
		& $Action;
		if ($global:lastexitcode -ne 0) { throw "The command [ $Action ] throw an exception."; }
	}
	finally { if ($WorkingDirectory) { Pop-Location; } }
}

function Invoke-PowershellTest
{
	Param(
		[ValidateScript({Test-Path $_})]
		$InstallationFolder,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_})]
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

function Merge-DllDocumentationWithXSD
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$XmlSchemaFilePath,

		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$DllDocumentationFilePath
	)

	[xml]$documentation = Resolve-Path $DllDocumentationFilePath | Get-Content;

	[xml]$xsd = Get-Content $XmlSchemaFilePath;
	$ns = [Xml.XmlNamespaceManager]::new($xsd.NameTable);
	[string]$xmlns = "http://www.w3.org/2001/XMLSchema";
	$ns.AddNamespace("xs", $xmlns);

	foreach ($type in $xsd.schema.complexType)
	{
		$pattern = "(?i)T:[a-z\.]+\.$($type.name)";
		$match = $documentation.SelectNodes("//member") | Where-Object { $_.Attributes["name"].Value -match $pattern; } | Select-Object -First 1;
		if ($match -ne $null)
		{
			Add-XsdAnnotation $xsd $type (Convert-MixedXmlElementToText $match.summary) | Out-Null;
			Write-Host "  * updated '$($type.name)' node documentation.";
		}

		foreach ($attribute in $type.attribute)
		{
			$pattern = "(?i)P:[a-z\.]+\.$($type.name).$($attribute.name)";
			$match = $documentation.SelectNodes("//member") | Where-Object { $_.Attributes["name"].Value -match $pattern; } | Select-Object -First 1;
			if ($match -ne $null)
			{
				Add-XsdAnnotation $xsd $attribute (Convert-MixedXmlElementToText $match.summary) | Out-Null;
				Write-Host "    * updated '$($attribute.name)' attribute documentation.";
			}
		}

		foreach ($element in $type.sequence.element)
		{
			$pattern = "(?i)P:[a-z\.]+\.$($type.name).$($element.name)";
			$match = $documentation.SelectNodes("//member") | Where-Object { $_.Attributes["name"].Value -match $pattern; } | Select-Object -First 1;
			if ($match -ne $null)
			{
				Add-XsdAnnotation $xsd $element (Convert-MixedXmlElementToText $match.summary) | Out-Null;
				Write-Host "    * updated '$($element.name)' element documentation.";
			}
		}
	}

	$xsd.Save($XmlSchemaFilePath);
	return $XSDFilePath;
}

function New-ConnectionInfo
{
	Param(
		[ValidateNotNull()]
		[Parameter(Mandatory)]
		$Node,

		[ValidateNotNullOrEmpty()]
		[Parameter(Mandatory)]
		[string]$ConnectionString
	)

	return [ConnectionInfo]::new($Node, $ConnectionString);
}

function New-GitTag
{
	Param(
		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$CurrentBranch,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateNotNullOrEmpty()]
		[string]$Version
	)

	if (($CurrentBranch -eq "master") -and (Test-Git))
	{
		Invoke-Tool { &git tag v$Version; }
		return $Version;
	}
	else { Write-Warning "The current branch ($CurrentBranch) is not master or the git is not installed on this machine."; }
	return $null;
}

function Publish-PackageToNuget
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$SecretsFilePath,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$Key,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$PackageFile
	)

	BEGIN { $apikey = Get-Secret $SecretsFilePath $Key; }
	PROCESS
	{
		Write-Header "dotnet: nuget-push '$($PackageFile.Name)'";
		Invoke-Tool { &dotnet nuget push $PackageFile.FullName --source "https://api.nuget.org/v3/index.json" --api-key $apiKey; }
	}
}

function Publish-PackageToVSIXGallery
{
	Param(
		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$InstallationFolder,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$VSIXFile
	)

	BEGIN
	{
		$publishScript = Join-Path $InstallationFolder "vsix-gallery/1.0/vsix.ps1";
		if (-not (Test-Path $publishScript))
		{
			$folder = Split-Path $publishScript -Parent;
			if (-not (Test-Path $folder)) { New-Item $folder -ItemType Directory | Out-Null; }
			Invoke-WebRequest "https://raw.github.com/madskristensen/ExtensionScripts/master/AppVeyor/vsix.ps1" -OutFile $publishScript;
		}
		Import-Module $publishScript -Force;
	}

	PROCESS
	{
		Write-Header "vsix-gallery: publish '$($VSIXFile.Name)'";
		Vsix-PublishToGallery $VSIXFile.FullName;

		if ([Environment]::UserInteractive)
		{
			try { Start-Process "http://vsixgallery.com/"; } catch { Write-Warning "Could not open web-browser."; }
		}
	}
}

function Publish-PackageToPowershellGallery
{
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$SecretsFilePath,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$Key,

		[Parameter(Mandatory, ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$ModuleManifest
	)

	BEGIN { [string]$apikey = Get-Secret $SecretsFilePath $Key; }

	PROCESS
	{
		if (Test-ModuleManifest $ModuleManifest.FullName)
		{
			Publish-Module -Path $ModuleManifest.DirectoryName -NuGetApiKey $apikey;
			Write-Host "  * published '$($ModuleManifest.BaseName)' to https://www.powershellgallery.com/";
		}
	}
}

function Remove-GeneratedProjectItem
{
	[CmdletBinding(SupportsShouldProcess)]
	Param(
		$AdditionalItems = @(),

		[Parameter(ValueFromPipeline)]
		[ValidateScript({Test-Path $_.FullName})]
		[IO.FileInfo]$ProjectFile
	)
	PROCESS
	{
		$itemsToBeRemoved =  (@("bin", "obj", "node_modules") + $AdditionalItems) | Select-Object -Unique;
		foreach ($target in $itemsToBeRemoved)
		{
			$itemPath = Join-Path $ProjectFile.DirectoryName $target;
			if ((Test-Path $itemPath) -and $PSCmdlet.ShouldProcess($itemPath))
			{
				Remove-Item $itemPath -Recurse -Force;
				Write-Host "  * removed '.../$($ProjectFile.Directory.Name)/$($target)'.";
			}
		}
	}
}

function Resolve-MSBuildPath
{
	[OutputType([string])]
	Param(
		[Parameter(Mandatory)]
		[ValidateScript({Test-Path $_})]
		[string]$InstallationFolder,

		[string]$version = "*"
	)

	Install-PSModules $InstallationFolder @("VSSetup");
    $instance = Get-VSSetupInstance -All | Select-VSSetupInstance -Latest;
    return (Join-Path $instance.InstallationPath "msbuild/$version/bin/msbuild.exe" | Resolve-Path) -as [string];
}

function Test-Dotnet
{
	$available = (&dotnet --version | Out-String) -match '\d+\.\d+';
	if ($available) { return $true; }
	else { Write-Warning ""; }
}

function Test-Git
{
	return (&git version | Out-String) -match '(?i)(v|ver|version)\s*\d+\.\d+\.\d+';
}

function Test-XsdExe
{
	[string]$xsd = Join-Path ${env:ProgramFiles(x86)} "Microsoft SDKs\Windows\*\bin\NETFX * Tools\xsd.exe" | Resolve-Path;
	if (($xsd -ne $null) -and (Test-Path $xsd -PathType Leaf)) { return $xsd; }
	else
	{
		Write-Warning "Could not find 'xsd.exe' on this machine; try executing the command 'where xsd' in the 'vs developer propmt' to find where it is located."
		return $false;
	}
}

function Write-FormatedMessage
{
	Param(
		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
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
