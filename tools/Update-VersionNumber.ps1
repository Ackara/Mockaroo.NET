<#

.SYSNOPSIS
This 


#>

[CmdletBinding()]
Param(

)

function GetRevisionNumber()
{
	
	return 1;
}

$major = 1;
$minor = 1;
$build = [Convert]::ToInt32([DateTime]::UtcNow.ToString("yyMMdd"));
$revision = GetRevisionNumber;
$version = "$major.$minor.$build.$revision";
echo $version;
