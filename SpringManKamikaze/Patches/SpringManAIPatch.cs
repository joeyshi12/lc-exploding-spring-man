using HarmonyLib;
using SpringManKamikaze.MonoBehaviours;

namespace SpringManKamikaze.Patches
{
    [HarmonyPatch(typeof(SpringManAI))]
    internal class SpringManAIPatch
    {
        [HarmonyPatch("OnCollideWithPlayer")]
        [HarmonyPrefix]
        static bool OnCollideWithPlayerPrefix(SpringManAI __instance)
        {
            ExplodeSpringMan(__instance);
            return false;
        }

        public static void ExplodeSpringMan(SpringManAI springManAI)
        {
            springManAI.KillEnemyServerRpc(true);
            springManAI.isEnemyDead = true;
            SerializableVector3 position = new SerializableVector3(springManAI.transform.position);
            SmkNetworkManager.instance.SpawnExplosionServerRpc(position);
        }
    }
}
