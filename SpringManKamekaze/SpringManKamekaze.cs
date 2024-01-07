using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SpringManKamekaze
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class SpringManKamekazeBase : BaseUnityPlugin
    {
        private const string modGUID = "coolcat0.LC_SpringManKamekaze";
        private const string modName = "LC_SpringManKamekaze";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static SpringManKamekazeBase Instance;

        public static ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo($"{modName} version {modVersion} has been loaded");

            harmony.PatchAll(typeof(SpringManKamekazeBase));
            harmony.PatchAll(typeof(SpringManAIPatch));
        }
    }

    [HarmonyPatch(typeof(SpringManAI))]
    internal class SpringManAIPatch
    {
        [HarmonyPatch("OnCollideWithPlayer")]
        [HarmonyPostfix]
        static void SelfDestructSpringMan(SpringManAI __instance)
        {
            Landmine.SpawnExplosion(__instance.transform.position, true);
            __instance.KillEnemy(true);
        }
    }
}
