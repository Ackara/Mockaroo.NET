<#
.SYNOPSIS
A psake bootstraper; This script runs one or more tasks defined in the psake file.

.EXAMPLE
.\build.ps1 -Help;
This example prints a list of all the available tasks.
#>

Param(
	[ValidateNotNullorEmpty()]
	[string[]]$Tasks = @("default"),

	[Alias('f')]
	[string]$Filter = $null,

	[Alias('no-commit')]
	[switch]$SkipCommit,

	[Alias('h', '?')]
	[switch]$Help,

	[Alias('d', "dry")]
	[switch]$DryRun,

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
	Write-Host "";
	Invoke-psake $taskFile -nologo -taskList $Tasks -properties @{
		"Filter"=$Filter;
		"Major"=$Major.IsPresent;
		"Minor"=$Minor.IsPresent;
		"DryRun"=$DryRun.IsPresent;
		"ToolsFolder"=$toolsFolder;
		"CurrentBranch"=$branchName;
		"Configuration"=$Configuration;
		"SolutionFolder"=$PSScriptRoot;
		"ShouldCommitChanges"=(-not $SkipCommit.IsPresent);
	}
	if (-not $psake.build_success) { exit 1; }
}