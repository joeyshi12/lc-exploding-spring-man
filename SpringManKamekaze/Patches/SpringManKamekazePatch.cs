using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SpringManKamekaze.Patches
{
    internal class SpringManKamekazePatch
    {
        //static ManualLogSource mls = CreativeModeBase.mls; 
        private static RoundManager currentRound;
        private static int mine;
        //private static bool hasLevelLoaded = false;

        [HarmonyPatch(typeof(RoundManager), "LoadNewLevel")]
        [HarmonyPrefix]
        private static void LoadNewLevelPatch()
        {
            currentRound = RoundManager.Instance;
        }

        [HarmonyPatch(typeof(RoundManager), "FinishGeneratingNewLevelClientRpc")]
        [HarmonyPostfix]
        private static void FinishGeneratingNewLevelClientRpcPatch()
        {
            int num = currentRound.currentLevel.spawnableMapObjects.Count();
            for (int i = 0; i < num; i++)
            {
                if ((currentRound.currentLevel.spawnableMapObjects[i].prefabToSpawn).name == "Landmine")
                {
                    mine = i;
                    break;
                }
            }
            //mls.LogDebug($"Mine ID = {ExplodingSpringManPatch.mine}");
            //hasLevelLoaded = true;
        }

/*        [HarmonyPatch(typeof(Landmine), "Start")]
        [HarmonyPrefix]
        private static void LandminePatch(ref Landmine __instance)
        {
            mls.LogInfo("Landmine Spawned");
            if (hasLevelLoaded)
            {
                mls.LogInfo("Forcing mine explosion");
                __instance.ExplodeMineServerRpc();
                mls.LogInfo("Mine forcefully activated");
            }
        }
*/
        [HarmonyPatch(typeof(SpringManAI))]
        [HarmonyPatch("OnCollideWithPlayer")]
        [HarmonyPostfix]
        static void SelfDestructSpringMan(SpringManAI __instance)
        {
            SpawnableMapObject mapObj = currentRound.currentLevel.spawnableMapObjects[SpringManKamekazePatch.mine];
            GameObject mine = UnityEngine.Object.Instantiate<GameObject>(mapObj.prefabToSpawn, __instance.mainCollider.transform.position, Quaternion.identity, currentRound.mapPropsContainer.transform);
            Unity.Netcode.NetworkObject networkObject = mine.GetComponent<Unity.Netcode.NetworkObject>();
            networkObject.Spawn(true);
            //mls.LogDebug($"Spawned mine at {__instance.mainCollider.transform.position}");

            //__instance.KillEnemy(true);
        }
    }
}
