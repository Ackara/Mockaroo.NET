<#
.SYNOPSIS
A psake bootstraper; This script runs one or more tasks defined in the psake file.

.EXAMPLE
.\build.ps1 -Help;
This example prints a list of all the available tasks.
#>

Param(
	[Alias('t')]
	[string[]]$Tasks = @("default"),

    [Alias('s', "keys")]
	[hashtable]$Secrets = @{},

    [Alias('no-commit')]
    [switch]$SkipCommit,

	[Alias('h', '?')]
    [switch]$Help,

	[switch]$Debug,
	[switch]$Major,
	[switch]$Minor
)

# Initializing required variables.
$Configuration = "Release";
if ($Debug) { $Configuration = "Debug"; }

# Getting the current branch of source control.
$branchName = $env:BUILD_SOURCEBRANCHNAME;
if ([string]::IsNullOrEmpty($branchName))
{
	$match = [Regex]::Match((& git branch), '\*\s*(?<name>\w+)');
	if ($match.Success) { $branchName = $match.Groups["name"].Value; }
}

# Installing then invoking the Psake tasks.
$toolsFolder = Join-Path $PSScriptRoot "tools";
$psakeModule = Join-Path $toolsFolder "psake/*/*.psd1";
if (-not (Test-Path $psakeModule))
{ 
	if (-not (Test-Path $toolsFolder)) { New-Item $toolsFolder -ItemType Directory | Out-Null; }
	Save-Module "psake" -Path $toolsFolder; 
}
Import-Module $psakeModule -Force;

$taskFile = Join-Path $PSScriptRoot "build/tasks.psake.ps1";
if ($Help) { Invoke-Psake -buildFile $taskFile -docs; }
else
{
	Write-Host -ForegroundColor DarkGray "User:          $([Environment]::UserName)@$([Environment]::MachineName)";
	Write-Host -ForegroundColor DarkGray "Platform:      $([Environment]::OSVersion.Platform)";
	Write-Host -ForegroundColor DarkGray "Branch:        $branchName";
    Write-Host -ForegroundColor DarkGray "Configuration: $Configuration";
	Invoke-psake $taskFile -nologo -taskList $Tasks -properties @{
        "Secrets"=$Secrets;
		"Major"=$Major.IsPresent;
		"Minor"=$Minor.IsPresent;
        "ToolsFolder"=$toolsFolder;
		"CurrentBranch"=$branchName;
		"Configuration"=$Configuration;
        "SolutionFolder"=$PSScriptRoot;
        "SolutionName"=(Split-Path $PSScriptRoot -Leaf);
        "ShouldCommitChanges"=(-not $SkipCommit.IsPresent);
	}
	if (-not $psake.build_success) { exit 1; }
}