using Trinity.Utilities;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Threading;
using VRC.Core;
using Trinity.Utilities;
using VRC;

namespace Trinity.SDK.Patching.Patches
{
    public static class _OnUInit
    {
        public static void OnUIInit()
        {
            try
            { 
                SerpentPatch.Instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod("Start"), null, new HarmonyMethod(AccessTools.Method(typeof(Main), "InitMenu"))); 
                //while (NetworkManager.field_Internal_Static_NetworkManager_0 == null){
                //    Thread.Sleep(25);
                //}
                //VRCEventDelegate<Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
                //VRCEventDelegate<Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
                //field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(_OnUInit.OnPlayerJoin));
                //field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(_OnUInit.OnPlayerLeave));

                //thanks hacker 
                SerpentPatch.Instance.Patch(typeof(NetworkManager).GetMethod("Method_Public_Void_Player_1"), null, new HarmonyMethod(AccessTools.Method(typeof(_OnUInit), nameof(OnPlayerJoin)))); 
                SerpentPatch.Instance.Patch(typeof(NetworkManager).GetMethod("Method_Public_Void_Player_0"), null, new HarmonyMethod(AccessTools.Method(typeof(_OnUInit),nameof(OnPlayerLeave)))); 
                LogHandler.Log(LogHandler.Colors.Green, "[Patch] All Patching Procedures Are Complete, Now Starting Client", false, false);
            }
            catch  { LogHandler.Log(LogHandler.Colors.Red, "[Patch] [Error] OnUInit", false, false); }
        }

        private static void OnPlayerJoin(VRC.Player __0)
        {
            if (__0 == PU.GetPlayer()) { WorldWrapper.Init(); }
            for (int i = 0; i < Main.Instance.OnPlayerJoinEvents.Count; i++)
                Main.Instance.OnPlayerJoinEvents[i].OnPlayerJoin(__0);
            if (PU.PlayersActorID.ContainsKey(__0.GetActorNumber()))
            {
                PU.PlayersActorID.Remove(__0.GetActorNumber());
                PU.PlayersActorID.Add(__0.GetActorNumber(), __0);
                return;
            }
            PU.PlayersActorID.Add(__0.GetActorNumber(), __0);
        }

        private static void OnPlayerLeave(VRC.Player __0)
        {
            if(__0 == PU.GetPlayer()) { Misc.ClearVD(); Module.Safety.Photon.PhotonProtection.E1BlockedPlayers.Clear(); }
            for (int i = 0; i < Main.Instance.OnPlayerLeaveEvents.Count; i++)
                Main.Instance.OnPlayerLeaveEvents[i].PlayerLeave(__0);
            PU.PlayersActorID.Remove(__0.GetActorNumber());
        }
    }
}