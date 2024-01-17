# r2modman profile to test with
$Profile = "Dev"
$ProfilePath = "$env:APPDATA\r2modmanPlus-local\LethalCompany\profiles\$Profile"
$DoorstopTarget = "$ProfilePath\BepInEx\core\BepInEx.Preloader.dll"

# Start game
& 'C:\Program Files (x86)\Steam\steamapps\common\Lethal Company\Lethal Company.exe' `
    --doorstop-enable true `
    --doorstop-target $DoorstopTarget
