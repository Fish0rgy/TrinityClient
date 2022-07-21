using Trinity.Utilities;
using HarmonyLib;
using System;
using System.Reflection;
using VRC.Networking;

namespace Trinity.SDK.Patching.Patches
{
    public static class _Udon 
    {
        public static void InitUdon()
        {
            try
            {
                SerpentPatch.Instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), new HarmonyMethod(AccessTools.Method(typeof(_Udon), nameof(OnUdon)))); 
                LogHandler.Log(LogHandler.Colors.Green, "[Patch] Udon", false, false);
            }
            catch (Exception ex)
            {
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] Could not patch Udon failed\n" + ex, false, false);
            }
        } 
        [Obfuscation(Exclude = true)]
        private static bool OnUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            //bool gay = true;
            //Main.Instance.OnUdonEvents.ForEach(udon => {
            //    if (!udon.OnUdon(__0, __1, __instance))
            //        gay = false;
            //});
            //return gay;
            for (int i = 0; i < Main.Instance.OnUdonEvents.Count; i++)
                if (!Main.Instance.OnUdonEvents[i].OnUdon(__0, __1, __instance))
                    return false;
            return true;
        }
    }
}
