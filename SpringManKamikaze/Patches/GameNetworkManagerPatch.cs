using HarmonyLib;
using Unity.Netcode;

namespace SpringManKamikaze.Patches
{
    [HarmonyPatch(typeof(GameNetworkManager))]
    internal class GameNetworkManagerPatcher
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void StartPostFix(ref GameNetworkManager __instance)
        {
            __instance.GetComponent<NetworkManager>().AddNetworkPrefab(Plugin.instance.smkNetworkManagerPrefab);
        }
    }
}
