<#

.SYNOPSIS
This script creates a file containing a specified Mockaroo API key
in the solution's test project.

.PARAMETER Key
The Mockaroo API key.

.EXAMPLE
New-MockarooKey "your_api_key";

#>

Param(
    [Parameter(Position=1)]
    [string]$Key = "your_api_key"
)
Clear-Host;

$ProjectDiretory = Split-Path $PSScriptRoot -Parent;
$apiKeyPath = "$ProjectDiretory\src\Tests\Tests.Mockaroo\apikey.txt";
[System.IO.File]::WriteAllText($apiKeyPath, $Key);
