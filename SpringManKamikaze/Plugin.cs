using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RuntimeNetcodeRPCValidator;
using SpringManKamikaze.MonoBehaviours;
using SpringManKamikaze.Patches;

namespace SpringManKamikaze
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        const string modGUID = "coolcat0.LC_SpringManKamikaze";
        const string modName = "LC_SpringManKamikaze";
        const string modVersion = "1.1.2";
        private readonly Harmony harmony = new Harmony(modGUID);

        public static Plugin instance;
        public static ManualLogSource mls;
        private NetcodeValidator netcodeValidator;

        void Awake()
        {
            instance = this;

            netcodeValidator = new NetcodeValidator(modGUID);
            netcodeValidator.PatchAll();
            netcodeValidator.BindToPreExistingObjectByBehaviour<SmkNetworkManager, Terminal>();

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo($"{modName} version {modVersion} has been loaded");

            harmony.PatchAll(typeof(EnemyAIPatch));
            harmony.PatchAll(typeof(SpringManAIPatch));
            harmony.PatchAll(typeof(HUDManagerPatch));
        }
    }
}
