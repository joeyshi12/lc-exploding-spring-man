# r2modman profile to test with
$Profile = "Dev"
$ProfilePath = "$env:APPDATA\r2modmanPlus-local\LethalCompany\profiles\$Profile"

# Build and copy dll to profile plugins folder
dotnet build
$OutputPath = ".\SpringManKamikaze\bin\Debug\net7.0\SpringManKamiKaze.dll"
$PluginPath = "$ProfilePath\BepInEx\plugins"

Copy-Item "$PluginPath" ".\Thunderstore" -Force
Copy-Item "$OutputPath" "$PluginPath" -Force
Write-Host "Copied plugin to $PluginPath"
