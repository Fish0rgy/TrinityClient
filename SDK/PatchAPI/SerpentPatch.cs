using Trinity.Utilities;
using Trinity.SDK.PatchAPI.Patches;
using Trinity.SDK.Patching.Patches; 
using System;
using System.Reflection;
using Harmony;

namespace Trinity.SDK.Patching
{
    class SerpentPatch
    {
        public new static HarmonyLib.Harmony Harmony { get; set; }
        public static HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Trinity");

        public static HarmonyMethod GetLocalPatch(Type type, string methodName)
        {
            return new HarmonyMethod(type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
        }

        public static void InitPatches()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "[Patch] Starting Patches....", false, false); 
                _AssetManagement.InitObjectInstantiatedPatch();
                _Spoofers.InitSpoofs();
                _Udon.InitUdon();
                _RoundTrip.RoundTripInit();
                _OnEvent.InitOnEvent();
                _AvatarAssetBundleLoad.InitAOnAssetBundleLoad();
                _Logging.InitLogging();
                _OnUInit.OnUIInit(); 


            }
            catch (Exception ERR) { LogHandler.Log(LogHandler.Colors.Green, ERR.Message, false, false); }
        }
    } 
}
