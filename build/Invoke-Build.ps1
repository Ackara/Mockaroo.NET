<#

.SYNOPSIS
This script initiates the project's build and deployment process.

.PARAMETER TaskList
A collection of psake tasks to invoke.

.NOTES
This script depends on the psake module.

#>

Param(
    [Parameter()]
    [string]$NuGetSource = "https://www.nuget.org/api/v2/package",
    
    [Parameter()]
    [string]$NuGetAPIKey = "ba2b3405-5416-4c78-addd-da65ef116c6c",
    
    [Parameter()]
    [string]$Cloudinary_APIKey = "182668521722121",

    [Parameter()]
    [string]$Cloudinary_Secret = "4zfbOlHTMBfo0Di_QNYXX6m01YE",

    [Parameter()]
    [string]$Cloudinary_CloudName = "ackara",

    [Parameter()]
    [string[]]$TaskList = @("default")
)

Clear-Host;
Push-Location (Split-Path $PSScriptRoot -Parent);

# Restore NuGet packages.
    $nuget = "$PWD\tools\nuget.exe";
    if(-not (Test-Path $nuget -PathType Leaf))
    {
        $toolsDir = "$PWD\tools";
        if(-not (Test-Path $toolsDir -PathType Container)) { New-Item  $toolsDir -ItemType Directory | Out-Null; }
        Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nuget;
    }
    & $nuget restore $(Get-ChildItem -Filter "*.sln" -Recurse | Select-Object -ExpandProperty FullName -First 1);

# Import psake module
    Remove-Module [p]sake;
    $psakeModule = (Get-ChildItem "$PWD\src\[Pp]ackages\psake*\tools\psake.psm1").FullName | Sort-Object $_ | Select-Object -Last 1;
    Import-Module $psakeModule;

# Start deployment
Invoke-psake `
    -buildFile "$PWD\build\build.ps1" `
    -taskList $TaskList `
    -framework 4.5.2 `
    -properties @{
        "NugetEXE" = $nuget;
        "NuGetKey" = $NuGetAPIKey;
        "NuGetSource" = $NuGetSource;

        "Cloudinary_APIKey" = $Cloudinary_APIKey;
        "Cloudinary_Secret" = $Cloudinary_Secret;
        "Cloudinary_CloudName" = $Cloudinary_CloudName;
    };

Pop-Location;
if(-not $psake.build_success) { exit 1; }