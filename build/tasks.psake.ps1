# SYNOPSIS: This is a psake task file.
Join-Path $PSScriptRoot "toolkit.psm1" | Import-Module -Force;
FormatTaskName "$(Write-Header -ReturnAsString)`r`n  {0}`r`n$(Write-Header -ReturnAsString)";

Properties {
	$Dependencies = @("Ncrement");

	# Files & Folders
    $SolutionFolder = (Split-Path $PSScriptRoot -Parent);
	$ManifestFilePath = (Join-Path $PSScriptRoot  "manifest.json");
	$SecretsFilePath = (Join-Path $SolutionFolder "secrets.json");
	$ArtifactsFolder = (Join-Path $SolutionFolder "artifacts");
    $ToolsFolder = "";

	# Arguments
    $ShouldCommitChanges = $true;
	$CurrentBranch = "";
	$Configuration = "";
	$Secrets = @{ };
	$Major = $false;
	$Minor = $false;
}

Task "Default" -depends @("configure", "build", "test", "pack");

Task "Deploy" -alias "publish" -description "This task compiles, test then publish all packages to their respective destination." `
-depends @("clean", "version", "build", "test", "pack", "push-nuget", "tag");

# ======================================================================

Task "Configure-Environment" -alias "configure" -description "This task generates all files required for development." `
-depends @("restore") -action {
	# Generating the build manifest file.
	if (-not (Test-Path $ManifestFilePath)) { New-NcrementManifest | ConvertTo-Json | Out-File $ManifestFilePath -Encoding utf8; }
	Write-Host "  * added 'build/$(Split-Path $ManifestFilePath -Leaf)' to the solution.";

	# Generating a secrets file template to store sensitive information.
	if (-not (Test-Path $SecretsFilePath))
	{
		$content = "{ nugetKey: null, mockarooKey: null }";
		$content | ConvertTo-Json | Out-File $SecretsFilePath -Encoding utf8;
	}
	Write-Host "  * added '$(Split-Path $SecretsFilePath -Leaf)' to the solution.";
}

Task "Package-Solution" -alias "pack" -description "This task generates all deployment packages." `
-depends @("restore") -action {
	if (Test-Path $ArtifactsFolder) { Remove-Item $ArtifactsFolder -Recurse -Force; }
	New-Item $ArtifactsFolder -ItemType Directory | Out-Null;

	$version = ConvertTo-NcrementVersionNumber $ManifestFilePath $CurrentBranch;
	Join-Path $SolutionFolder "src/*/*" | Get-ChildItem -File -Filter "*.*proj" | Invoke-NugetPack $ArtifactsFolder $Configuration $version.FullVersion;
}

Task "Update-MockarooTypeList" -alias "types" -description "This task updates the list of existing mockaroo data-types." `
-action {
	$tempFile = New-TemporaryFile;
	$apikey = Get-Secret $SecretsFilePath "mockarooKey";
	Invoke-WebRequest "https://api.mockaroo.com/api/types.json?key=$apikey" -OutFile $tempFile.FullName;
	$results = Get-Content $tempFile.FullName | ConvertFrom-Json;

	$targetFile = Join-Path $SolutionFolder "src/*/mockaroo_data_types.txt" | Get-Item;
	$results.types | Select-Object -ExpandProperty name | Out-File $targetFile.FullName -Encoding utf8;
	Write-Host "  * updated '$($targetFile.Name)'.";
}

#region ----- COMPILATION -----

Task "Clean" -description "This task removes all generated files and folders from the solution." `
-action {
	$SolutionFolder | Remove-GeneratedSolutionItems -AdditionalItems @();
	Get-ChildItem $SolutionFolder -Recurse -File -Filter "*.*proj" | Remove-GeneratedProjectItems -AdditionalItems @();
}

Task "Install-BuildDependencies" -alias "restore" -description "This task imports all build dependencies." `
-action {
	# Installing all required dependencies.
	foreach ($moduleId in $Dependencies)
	{
		$modulePath = Join-Path $ToolsFolder "$moduleId/*/*.psd1";
		if (-not (Test-Path $modulePath)) { Save-Module $moduleId -Path $ToolsFolder; }
		Import-Module $modulePath -Force;
		Write-Host "  * imported the '$moduleId.$(Split-Path (Get-Item $modulePath).DirectoryName -Leaf)' powershell module.";
	}
}

Task "Increment-VersionNumber" -alias "version" -description "This task increments all of the projects version number." `
-depends @("restore") -action {
	$manifest = $ManifestFilePath | Step-NcrementVersionNumber -Major:$Major -Minor:$Minor -Patch;
	$manifest | ConvertTo-Json | Out-File $ManifestFilePath -Encoding utf8;
	Exec { &git add $ManifestFilePath | Out-Null; }

	Join-Path $SolutionFolder "src/*/*.*proj" | Get-ChildItem -File | Update-ProjectFile $manifest -Commit:$ShouldCommitChanges `
		| Write-FormatedMessage "  * updated '{0}' version number to '$(ConvertTo-NcrementVersionNumber $manifest | Select-Object -ExpandProperty Version)'.";
}

Task "Build-Solution" -alias "build" -description "This task compiles projects in the solution." `
-action {
	Get-Item "$SolutionFolder/*.sln" | Invoke-MSBuild $Configuration;
}

Task "Run-Tests" -alias "test" -description "This task invoke all tests within the 'tests' folder." `
-action {
	Join-Path $SolutionFolder "tests" | Get-ChildItem -Recurse -File -Filter "*.tests.ps1" | Invoke-PowershellTest $ToolsFolder;
	Join-Path $SolutionFolder "tests" | Get-ChildItem -Recurse -File -Filter "*.*proj" | Invoke-MSTest $Configuration;
}

#endregion

#region ----- PUBLISHING -----

Task "Publish-NuGetPackages" -alias "push-nuget" -description "This task publish all nuget packages to nuget.org." `
-precondition { return Test-Path $ArtifactsFolder -PathType Container } `
-action { Get-ChildItem $ArtifactsFolder -Recurse -Filter "*.nupkg" | Publish-NugetPackage $SecretsFilePath "nugetKey"; }

Task "Add-GitReleaseTag" -alias "tag" -description "This task tags the last commit with the version number." `
-depends @("restore") -action { $ManifestFilePath | ConvertTo-NcrementVersionNumber | Select-Object -ExpandProperty Version | New-GitTag $CurrentBranch; }

#endregion