using HarmonyLib;
using SpringManKamikaze.MonoBehaviours;
using System.Numerics;

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
                springManAI.KillEnemyServerRpc(true);
                springManAI.isEnemyDead = true;
                SerializableVector3 position = new SerializableVector3(springManAI.transform.position);
                SmkNetworkManager.instance.SpawnExplosionServerRpc(position);
            }
        }
    }
}
