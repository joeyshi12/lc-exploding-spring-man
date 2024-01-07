using BepInEx;
using BepInEx.Logging;
using CreativeMode.Patches;
using HarmonyLib;

namespace CreativeMode
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class CreativeModeBase : BaseUnityPlugin
    {
        private const string modGUID = "coolcat0.LC_CreativeMode";
        private const string modName = "LC_CreativeMode";
        private const string modVersion = "0.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static CreativeModeBase Instance;

        public static ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo($"{modName} version {modVersion} has been loaded");

            harmony.PatchAll(typeof(CreativeModeBase));
            harmony.PatchAll(typeof(ExplodingSpringManPatch));
        }
    }
}
