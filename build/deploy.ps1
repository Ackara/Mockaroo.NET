Properties {
    # Credentials
    $NuGetKey = $null;
    
    # Paths
    $ProjectDirectory = (Split-Path $PSScriptRoot -Parent);
    $DeployDirectory = "$ProjectDirectory\build\bin\nuget";
    $TempDirectory = "$ProjectDirectory\build\Temp";
    $NugetEXE = "$ProjectDirectory\tools\nuget.exe";
    
    # Msbuild Args
    $BuildConfiguration = "Release";
    $BuildPlatform = "Any CPU";
}

Task default -depends Init, Create-NuGetPackages, Publish-NuGetPackages;

Task Init -description "Initialize the build n' deploy procedure." -action {
    Assert(Test-Path $ProjectDirectory -PathType Container) "`$ProjectionDirectory cannot be null or empty.";

    # Cleanup directories.
        foreach($dir in @($DeployDirectory))
        {
            if(Test-Path $dir -PathType Container)
            {
               Remove-Item $dir -Force -Recurse;
               New-Item $dir -ItemType Directory | Out-Null;
            }
            else { New-Item $dir -ItemType Directory | Out-Null; }
        }
}

Task Compile -description "Build the solution." -depends Init -action {
    Assert("Debug", "Release" -contains $BuildConfiguration) "Value must be 'Debug' or 'Release'.";
    Assert("x86", "x64", "Any CPU" -contains $BuildPlatform) "Value must be 'x86', 'x64' or 'Any CPU'.";

    # Build Visual Studio solution.
        $solution = (Get-ChildItem $ProjectDirectory -Recurse -Filter "*.sln" | Select-Object -ExpandProperty FullName -First 1);
        Exec { msbuild $solution "/p:Configuration=$BuildConfiguration;Platform=$BuildPlatform" | Out-Null }
}

Task Create-NuGetPackages -description "Create a nuget package for all non test projects." -depends Compile -action {
    Push-Location $DeployDirectory;
    try
    {
        $projects = Get-ChildItem "$ProjectDirectory\src" -Recurse -Exclude @("Test*.csproj") -Filter "*.csproj" | Select-Object -ExpandProperty FullName;
        foreach($project in $projects)
        {
            $nuspec = [System.IO.Path]::ChangeExtension($project, ".nuspec");
            if(Test-Path $nuspec -PathType Leaf)
            {
                Exec { (& $NugetEXE pack $($project) -IncludeReferencedProjects -Prop Configuration=$($BuildConfiguration)) | Out-Null; }
            }
        }
    }
    finally { Pop-Location; }
}

Task Publish-NuGetPackages -description "Publish nuget packages to nuget.org" -depends Create-NuGetPackages -action {
    Assert(-not [System.String]::IsNullOrEmpty($NuGetKey)) "The 'NugetKey' cannot be null or empty.";

    foreach($package in (Get-ChildItem $DeployDirectory | Select-Object -ExpandProperty FullName))
    {
        Exec { & $NugetEXE push $($package) $($NuGetKey) -Source "https://www.nuget.org"; }
    }
}
