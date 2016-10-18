Properties {
	# Paths
	$RootDirectory = (Split-Path $PSScriptRoot -Parent);
	$PackageDirectory = "$RootDirectory\build\packages";

	# Nuget
	$Nuget = "$RootDirectory\tools\nuget.exe";
	$NugetSource = $null;
	$NugetKey = $null;

	# Msbuild Args
	$BuildConfiguration = "Release";
	$BuildPlatform = "Any CPU";
}

Task VSTS -depends Init, Create-Packages, Publish-Packages;
Task default -depends Init, Build-Solution, Run-Tests, Create-Packages, Publish-Packages;

Task Init -description "Create and cleanup all working folders." `
-action{
	foreach($directory in @($PackageDirectory))
	{
		if(Test-Path $directory -PathType Container) { Remove-Item $directory -Recurse; }
		New-Item $directory -ItemType Directory | Out-Null;
	}
}


Task Build-Solution -description "Build and compile the solution." `
-action {
	Assert("Debug", "Release" -contains $BuildConfiguration) "'BuildConfiguration' must be 'Debug' or 'Release'.";
	Assert("x86", "x64", "Any CPU" -contains $BuildPlatform) "'BuildPlatform' must be 'x86', 'x64' or 'Any CPU'.";

	# Build Visual Studio solution.
	$solution = (Get-ChildItem $RootDirectory -Recurse -Filter "*.sln" | Select-Object -ExpandProperty FullName -First 1);
	Exec { msbuild $solution "/p:Configuration=$BuildConfiguration;Platform=$BuildPlatform" | Out-Null }
}


Task Run-Tests -description "Run all automated tests." `
-action {
	$vstestEXE = Get-ChildItem "C:\Program Files (x86)\Microsoft Visual Studio*\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" `
		| Select-Object -ExpandProperty FullName `
		| Sort-Object $_ | Select-Object -Last 1;

	Push-Location "$RootDirectory\src";
	Exec { & $vstestEXE "$RootDirectory\src\Test.Mockaroo\bin\$BuildConfiguration\Test.Mockaroo.dll" /Logger:trx; }
	Pop-Location;
}


Task Create-Packages -description "Create all packages." `
-depends Init `
-action {
	Assert("Debug", "Release" -contains $BuildConfiguration) "'BuildConfiguration' must be 'Debug' or 'Release'.";

	foreach($nuspec in (Get-ChildItem "$RootDirectory\src" -Filter "*.nuspec" -Recurse | Select-Object -ExpandProperty FullName))
	{
		$nupkg = [IO.Path]::ChangeExtension($nuspec, ".csproj");
		if(Test-Path $nupkg -PathType Leaf)
		{
			Exec { & $nuget pack $nupkg -Prop Configuration=$BuildConfiguration -OutputDirectory $PackageDirectory -IncludeReferencedProjects; }
		}
	}
}


Task Publish-Packages -description "Publish all nuget packages." `
-depends Init `
-action {
	foreach($nupkg in (Get-ChildItem $PackageDirectory -Filter "*.nupkg" | Select-Object -ExpandProperty FullName))
	{
		Exec { & $nuget push $nupkg $NugetKey -Source $NugetSource; }
	}
}
