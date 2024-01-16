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
            if (__instance is SpringManAI springManAI && !springManAI.isEnemyDead)
            {
                SerializableVector3 explosionPosition = new SerializableVector3(springManAI.transform.position);
                SmkNetworkManager.instance.SpawnExplosionServerRpc(explosionPosition);
                springManAI.KillEnemyServerRpc(true);
            }
        }
    }
}
