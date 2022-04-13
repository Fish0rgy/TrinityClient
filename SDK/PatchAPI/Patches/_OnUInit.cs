using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Threading;
using VRC.Core;

namespace Trinity.SDK.Patching.Patches
{
    public static class _OnUInit
    {
        private static List<int> blacklistedPlayers = new List<int>();
        public static void OnUIInit()
        {
            try
            {
                AlienPatch.Instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod("Awake"), null, new HarmonyMethod(AccessTools.Method(typeof(Main), "OnUIInit")));
                while (NetworkManager.field_Internal_Static_NetworkManager_0 == null){Thread.Sleep(25);}
                VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
                VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
                field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(OnPlayerJoin));
                field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(OnPlayerLeave));
                LogHandler.Log(LogHandler.Colors.Green, "[Patch] All Patching Procedures Are Complete, Now Starting Client", false, false);
            }
            catch
            {
                LogHandler.Log(LogHandler.Colors.Red, "[Patch] [Error] Networking", false, false);
            }
        }

            private static void OnPlayerJoin(VRC.Player player)
            {
                if (player == PlayerWrapper.LocalPlayer) { WorldWrapper.Init(); }
                for (int i = 0; i < Main.Instance.OnPlayerJoinEventArray.Length; i++)
                    Main.Instance.OnPlayerJoinEventArray[i].OnPlayerJoin(player);
                if (PlayerWrapper.PlayersActorID.ContainsKey(player.GetActorNumber()))
                {
                    PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
                    PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
                }
                else
                {
                    PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
                }

            }

            private static void OnPlayerLeave(VRC.Player player)
            {
                if (player == null)
                {
                    return;
                }
                for (int i = 0; i < Main.Instance.OnPlayerLeaveEventArray.Length; i++)
                    Main.Instance.OnPlayerLeaveEventArray[i].PlayerLeave(player);
                PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
            }
        }
    }