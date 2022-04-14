using Trinity.Utilities;
using HarmonyLib;
using System;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.Core;

namespace Trinity.SDK.Patching.Patches
{
    public static class _AvatarAssetBundleLoad 
    {
        public static void InitAOnAssetBundleLoad()
        {
            try
            {
                SerpentPatch.Instance.Patch(typeof(AssetManagement).GetMethod("Method_Public_Static_Object_Object_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(_AvatarAssetBundleLoad), nameof(OnAvatarAssetBundleLoad))));
                //SerpentPatch.Instance.Patch(typeof(AssetManagement).GetMethod("Method_Public_Static_Object_Object_Vector3_Quaternion_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(_AvatarAssetBundleLoad), nameof(OnAssetRequestDownload))));

                SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] AssetBundle", false, false);
            }
            catch
            {
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [Error] AssetBundle", false, false);
            }
        }


        //[Obfuscation(Exclude = true)]
        //public static bool OnAssetRequestDownload(IntPtr AssetPointer, Vector3 Position, Quaternion Rotation, byte AllowCustomShader, byte IsUI, byte Validate, IntPtr NativeMethodPointer)
        //{
        //    return true;
        //}

        [Obfuscation(Exclude = true)]
        private static bool OnAvatarAssetBundleLoad(ref UnityEngine.Object __0)
        {
            GameObject gameObject = __0.TryCast<GameObject>();
            if (gameObject == null)
            {
                return true;
            }
            if (!gameObject.name.ToLower().Contains("avatar"))
            {
                return true;
            }
            string avatarId = gameObject.GetComponent<PipelineManager>().blueprintId;
            for (int i = 0; i < Main.Instance.OnAssetBundleLoadEvents.Count; i++)
                if (!Main.Instance.OnAssetBundleLoadEvents[i].OnAvatarAssetBundleLoad(gameObject, avatarId))
                    return false;

            return true;
        }

    }
}
