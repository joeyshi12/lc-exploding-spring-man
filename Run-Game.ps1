# r2modman profile to test with
$Profile = "Dev"
$ProfilePath = "C:\Users\Joey Shi\AppData\Roaming\r2modmanPlus-local\LethalCompany\profiles\$Profile"
$DoorstopTarget = "$ProfilePath\BepInEx\core\BepInEx.Preloader.dll"

# Build and copy dll to profile plugins folder
dotnet build
Copy-Item ".\SpringManKamikaze\bin\Debug\net7.0\SpringManKamiKaze.dll" "$ProfilePath\BepInEx\plugins"

# Start game
& 'C:\Program Files (x86)\Steam\steamapps\common\Lethal Company\Lethal Company.exe' `
    --doorstop-enable true `
    --doorstop-target $DoorstopTarget
