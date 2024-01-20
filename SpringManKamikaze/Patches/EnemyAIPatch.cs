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
                SpringManAIPatch.ExplodeSpringMan(springManAI);
            }
        }
    }
}
