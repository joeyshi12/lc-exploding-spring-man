using HarmonyLib;
using SpringManKamikaze.MonoBehaviours;

namespace SpringManKamikaze.Patches
{
    [HarmonyPatch(typeof(EnemyAI))]
    internal class EnemyAIPatch
    {
        [HarmonyPatch("HitEnemyOnLocalClient")]
        [HarmonyPostfix]
        static void HitEnemyOnLocalClientPostfix(EnemyAI __instance)
        {
            SerializableVector3 explosionPosition = new SerializableVector3(__instance.transform.position);
            SmkNetworkManager.instance.SpawnExplosionServerRpc(explosionPosition);
            __instance.KillEnemyServerRpc(true);
        }
    }
}
