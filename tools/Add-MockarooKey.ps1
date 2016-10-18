<#

.SYNOPSIS
This script adds your Mockaroo API key to the solution

.PARAMETER Key
The Mockaroo API key.

#>

[CmdletBinding()]
Param(
	[Parameter(Position=1, Mandatory=$true)]
	[string]$Key
)

$rootDirectory = (Split-Path $PSScriptRoot -Parent);
$keyTXT = "$rootDirectory\src\Test.Mockaroo\your_mockaroo_key.txt";

if(-not (Test-Path $keyTXT -PathType Leaf))
{
	New-Item $keyTXT -ItemType File -Value $Key;
}
else
{
	Out-File $keyTXT -InputObject $Key;
}

