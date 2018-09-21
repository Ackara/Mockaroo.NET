# Project-Specific tasks.

Properties {
	[string]$MigrationDirectory = Join-Path $RootDir "src/*/*Migrations" | Resolve-Path;
}

Task "Deploy" -alias "publish" -description "This task compiles, test then publishes the solution." `
-depends @("version", "build", "test", "pack", "push-nuget", "tag");

# ===============

Task "clean" -action {
}

Task "Setup-Project" -alias "configure" -description "This initializes the project." `
-depends @("restore") -action {
    [string]$sln = Resolve-Path "$RootDir/*.sln";
    Write-Header "dotnet restore";
    Exec { &dotnet restore $sln; }

	[string]$projectDir = Join-Path $RootDir "tests/*mstest" | Resolve-Path;
	[string]$apikey = Join-Path $projectDir "your_mockaroo_key.txt";
	if (-not (Test-Path $apikey))
	{
		New-Item $apikey -ItemType File | Out-Null;
		"you need to get a key from https://mockaroo.com/" | Out-File -FilePath $apikey -Encoding utf8;
		Invoke-Item $apikey;
	}
}

Task "Update-DataTypeList" -alias "update" -description "This task updates the list of know mockaroo data-types." `
-action {
	[string]$projectDir = Join-Path $RootDir "tests/*mstest" | Resolve-Path;
	[string]$knowTypesFiles = Join-Path $RootDir "src/*/knownTypes.txt" | Resolve-Path;
	[string]$apikey = Get-Content (Join-Path $projectDir "*key.txt" | Resolve-Path) | Out-String;

	[string]$doc = New-TemporaryFile;
	Invoke-WebRequest "https://api.mockaroo.com/api/types.json?key=$apikey" -OutFile $doc;
	$typeList = Get-Content $doc | ConvertFrom-Json;
	$typeList.types | Select-Object -ExpandProperty name | Out-File -FilePath $knowTypesFiles -Encoding utf8;
}

Task "Package-Solution" -alias "pack" -description "This task generates all deployment packages." `
-depends @("restore") -action {
	if (Test-Path $ArtifactsDir) { Remove-Item $ArtifactsDir -Recurse -Force; }
	New-Item $ArtifactsDir -ItemType Directory | Out-Null;

	$proj = Get-Item "$RootDir\src\*\*.csproj";
	Write-Header "dotnet: pack '$($proj.BaseName)'";
    $version = Get-NcrementManifest $ManifestJson | Convert-NcrementVersionNumberToString $Branch -AppendSuffix;
	Exec { &dotnet pack $proj.FullName --output $ArtifactsDir --configuration $Configuration /p:PackageVersion=$version; }
}