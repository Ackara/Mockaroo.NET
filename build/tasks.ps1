Properties {
    # Credentials
    $NuGetKey = "";

    $Cloudinary_CloudName = "";
    $Cloudinary_APIKey = "";
    $Cloudinary_Secret = "";
    
    # Paths and URIs
    $ProjectDirectory = (Split-Path $PSScriptRoot -Parent);
    $TempDirectory = "$ProjectDirectory\build\temp";
    $BinDirectory = "$ProjectDirectory\build\bin";
    
    $NuGetSource = "https://www.nuget.org/api/v2/package";
    $NugetEXE = "$ProjectDirectory\tools\nuget.exe";
    $NuGetPackageDirectory = "$BinDirectory\\nupkg";
    $NuGetIcon = "";
    
    # Msbuild Args
    $BuildConfiguration = "Release";
    $BuildPlatform = "Any CPU";
}

Task default -depends Init, Compile, Push-NuGetIconToCDN, Publish-NuGetPackages;

Task Init -description "Initialize the build n' deploy procedure." -action {
    Assert(Test-Path $ProjectDirectory -PathType Container) "`$ProjectionDirectory cannot be null or empty.";
    Import-Module ((Get-ChildItem "$ProjectDirectory\src\packages\Gigobyte.DevOps.*\tools\Gigobyte.DevOps.dll").FullName | Sort-Object $_ | Select-Object -Last 1);

    # Cleanup directories.
    foreach($dir in @($BinDirectory, $NuGetPackageDirectory, $TempDirectory))
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
    Assert("Debug", "Release" -contains $BuildConfiguration) "'BuildConfiguration' must be 'Debug' or 'Release'.";
    Assert("x86", "x64", "Any CPU" -contains $BuildPlatform) "'BuildPlatform' must be 'x86', 'x64' or 'Any CPU'.";

    # Build Visual Studio solution.
    $solution = (Get-ChildItem $ProjectDirectory -Recurse -Filter "*.sln" | Select-Object -ExpandProperty FullName -First 1);
    Exec { msbuild $solution "/p:Configuration=$BuildConfiguration;Platform=$BuildPlatform" | Out-Null }
}

Task Create-NuGetPackages -description "Create a nuget package for all non test projects." -depends Compile -action {
    Assert(Test-Path $NuGetPackageDirectory -PathType Container) "Could not find '$NuGetPackageDirectory'.";

    Push-Location $NuGetPackageDirectory;
    try
    {
        foreach($project in (Get-ChildItem "$ProjectDirectory\src" -Recurse -Filter "*.csproj" -Exclude @("*Test*") | Select-Object -ExpandProperty FullName))
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
    Assert(-not [System.String]::IsNullOrEmpty($NuGetSource)) "The 'NuGetSource' cannot be null nor empty.";
    Assert(-not [System.String]::IsNullOrEmpty($NuGetKey)) "The 'NugetKey' cannot be null nor empty.";
    
    foreach($package in (Get-ChildItem $NuGetPackageDirectory | Select-Object -ExpandProperty FullName))
    {
        Exec { & $NugetEXE push $($package) $($NuGetKey) -Source $($NuGetSource); }
    }
}

Task Push-NuGetIconToCDN -description "Upload an image to https://cloudinary.com CDN." -action {
    Assert(-not [String]::IsNullOrEmpty($Cloudinary_APIKey)) "'Cloudinary_APIKey' cannot be null nor empty.";
    Assert(-not [String]::IsNullOrEmpty($Cloudinary_Secret)) "'Cloudinary_Secret' cannot be null nor empty.";
    Assert(-not [String]::IsNullOrEmpty($Cloudinary_CloudName)) "'Cloudinary_CloundName' cannot be null nor empty.";
    
    Write-Host "`t* Uploading 'mockaroo-net.png' to 'https://api.cloudinary.com'...";
    $imageUrl = Push-ImageToCloudinary "$ProjectDirectory\mockaroo-net.png" -CloudName $Cloudinary_CloudName -APIKey $Cloudinary_APIKey -Secret $Cloudinary_Secret;

    Write-Host "`t* Replacing all .nuspec files <iconUrl> element with image url...";
    foreach($nuspec in (Get-ChildItem "$ProjectDirectory\src" -Recurse -Filter "*.nuspec" | Select-Object -ExpandProperty FullName))
    {
        Edit-Xml $nuspec -XPath "package/metadata/iconUrl" -Value $imageUrl;
    }
}