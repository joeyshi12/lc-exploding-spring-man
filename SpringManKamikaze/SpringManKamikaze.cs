using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SpringManKamikaze
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class SpringManKamikazeBase : BaseUnityPlugin
    {
        private const string modGUID = "coolcat0.LC_SpringManKamikaze";
        private const string modName = "LC_SpringManKamikaze";
        private const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static SpringManKamikazeBase Instance;
        public static ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo($"{modName} version {modVersion} has been loaded");
            harmony.PatchAll(typeof(SpringManKamikazeBase));
            harmony.PatchAll(typeof(SpringManAIPatch));
        }
    }

    [HarmonyPatch(typeof(SpringManAI))]
    internal class SpringManAIPatch
    {
        [HarmonyPatch("OnCollideWithPlayer")]
        [HarmonyPrefix]
        static bool OnCollideWithPlayerPrefix(SpringManAI __instance)
        {
            Vector3 explosionPosition = __instance.transform.position + Vector3.up;
            Landmine.SpawnExplosion(explosionPosition, true);
            Landmine.SpawnExplosion(explosionPosition, true);
            __instance.KillEnemy(true);
            return false;
        }
    }
}
