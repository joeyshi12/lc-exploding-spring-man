$sourceDirectories = @(
    "SpringManKamikaze\bin\Debug\net7.0\SpringManKamikaze.dll",
    "README.md",
    "icon.png",
    "manifest.json"
)
Compress-Archive -Force -Path $sourceDirectories -DestinationPath "build.zip"
