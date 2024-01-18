using GameNetcodeStuff;
using HarmonyLib;
using SpringManKamikaze.MonoBehaviours;

namespace SpringManKamikaze.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HUDManagerPatch
    {
        [HarmonyPatch("AddChatMessage")]
        [HarmonyPrefix]
        static void AddChatMessagePrefix(HUDManager __instance, ref string chatMessage, ref string nameOfUserWhoTyped)
        {
            if (chatMessage[0] != '!')
            {
                return;
            }
            Plugin.mls.LogInfo(nameOfUserWhoTyped + " executed " + chatMessage);
            String[] args = chatMessage.Split(' ');
            switch (args[0])
            {
                case "!explode":
                    ExplodePlayer(args);
                    break;
                default:
                    break;
            }
        }

        private static void ExplodePlayer(String[] args)
        {
            if (args.Length < 2) {
                return;
            }
            String targetName = args[1];
            foreach (PlayerControllerB controller in StartOfRound.Instance.allPlayerScripts)
            {
                if (!controller.isPlayerDead && targetName == controller.name)
                {
                    SerializableVector3 explosionPosition = new SerializableVector3(controller.transform.position);
                    SmkNetworkManager.instance.SpawnExplosionServerRpc(explosionPosition);
                }
            }
        }
    }
}
