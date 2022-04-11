using Area51.SDK.PatchAPI.Patches;
using Area51.SDK.Patching.Patches;
using HarmonyLib;
using System;
using System.Reflection;

namespace Area51.SDK.Patching
{
    class AlienPatch
    {
        public new static HarmonyLib.Harmony Harmony { get; set; }
        public static HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Area 51");

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
                _OnEvent.InitOnEvent();
                _AvatarAssetBundleLoad.InitAOnAssetBundleLoad();
                _Logging.InitLogging();
                _OnUInit.OnUIInit();
                

            }
            catch (Exception ERR) { LogHandler.Log(LogHandler.Colors.Green, ERR.Message, false, false); }
        }
    }
}
