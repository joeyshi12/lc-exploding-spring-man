using HarmonyLib;
using SpringManKamikaze.MonoBehaviours;
using System.Collections;
using UnityEngine;

namespace SpringManKamikaze.Patches
{
    [HarmonyPatch(typeof(SpringManAI))]
    internal class SpringManAIPatch
    {
        [HarmonyPatch("OnCollideWithPlayer")]
        [HarmonyPrefix]
        static bool OnCollideWithPlayerPrefix(SpringManAI __instance)
        {
            __instance.KillEnemyServerRpc(true);
            __instance.isEnemyDead = true;
            SerializableVector3 position = new SerializableVector3(__instance.transform.position);
            SmkNetworkManager.instance.SpawnExplosionServerRpc(position);
            return false;
        }
    }
}
