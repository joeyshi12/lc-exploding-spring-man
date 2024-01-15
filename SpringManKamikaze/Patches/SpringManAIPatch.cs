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
            SerializableVector3 explosionPosition = new SerializableVector3(__instance.transform.position);
            SmkNetworkManager.instance.SpawnExplosionServerRpc(explosionPosition);
            __instance.KillEnemyServerRpc(true);
            return false;
        }
    }
}
