using AmplitudeSDKWrapper;
using MelonLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnhollowerBaseLib;
using UnityEngine;
using AccessTools = HarmonyLib.AccessTools;
using HarmonyMethod = HarmonyLib.HarmonyMethod;

namespace Trinity.SDK.Patching.Patches
{
    public static class _Spoofers
    {
        public static string newHWID = string.Empty;
        public static void InitSpoofs()
        {
            try
            {
                AlienPatch.Instance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(_Spoofers), nameof(FakeHWID))));
                AlienPatch.Instance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(_Spoofers), nameof(VoidPatch))));
                AlienPatch.Instance.Patch(typeof(AmplitudeWrapper).GetMethods().First((MethodInfo x) => x.Name == "LogEvent" && x.GetParameters().Length == 4), new HarmonyMethod(AccessTools.Method(typeof(_Spoofers), nameof(VoidPatch))));
                LogHandler.Log(LogHandler.Colors.Green, "[Patch] Analystics", false, false);
            }
            catch (Exception ERROR)
            {
                LogHandler.Log(LogHandler.Colors.Red, "[Patch] Could not patch Analystics failed\n" + ERROR, false, false);
            }
        }


        [Obfuscation(Exclude = true)]
        private static bool FakeHWID(ref string __result)
        {
            if (newHWID == string.Empty)
            {
                newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9) }))).Select(delegate (byte x)
                {
                    byte b = x;
                    return b.ToString("x2");}).Aggregate((string x, string y) => x + y);

                LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] HWID New: {newHWID}", false, false);               
            }
            __result = newHWID;
            return false;
        }


        //public unsafe static void SafetyPatch()
        //{

        //    //LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] PC Name New: {SystemInfo.deviceName}", false, false);
        //    //LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] Model New: {SystemInfo.deviceModel}", false, false);
        //    //LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] PBU New: {SystemInfo.graphicsDeviceName}", false, false);
        //    //LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] CPU New: {SystemInfo.processorType}", false, false);
        //    //LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] PBU ID New: {SystemInfo.graphicsDeviceID.ToString()}", false, false);
        //    //LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] OS New:{SystemInfo.operatingSystem}", false, false);
        //    IntPtr intPtr2 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceModel");
        //    MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr2)), AccessTools.Method(typeof(_Spoofers), "FakeModel", null, null).MethodHandle.GetFunctionPointer());
        //    IntPtr intPtr3 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceName");
        //    MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr3)), AccessTools.Method(typeof(_Spoofers), "FakeName", null, null).MethodHandle.GetFunctionPointer());
        //    IntPtr intPtr4 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceName");
        //    MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr4)), AccessTools.Method(typeof(_Spoofers), "FakeGBU", null, null).MethodHandle.GetFunctionPointer());
        //    IntPtr intPtr5 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceID");
        //    MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr5)), AccessTools.Method(typeof(_Spoofers), "FakeGBUID", null, null).MethodHandle.GetFunctionPointer());
        //    IntPtr intPtr6 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetProcessorType");
        //    MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr6)), AccessTools.Method(typeof(_Spoofers), "FakeProcessor", null, null).MethodHandle.GetFunctionPointer());
        //    IntPtr intPtr7 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetOperatingSystem");
        //    MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr7)), AccessTools.Method(typeof(_Spoofers), "FakeOS", null, null).MethodHandle.GetFunctionPointer());
        //}

        //public static IntPtr FakeModel() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Motherboards[new System.Random().Next(0, Motherboards.Length)])).Pointer;
        //public static IntPtr FakeName() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp("DESKTOP-" + Misc.RandomString(7))).Pointer;
        //public static IntPtr FakeGBU() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(PBU[new System.Random().Next(0, PBU.Length)])).Pointer;
        //public static IntPtr FakeGBUID() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Misc.RandomString(12))).Pointer;
        //public static IntPtr FakeProcessor() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(CPU[new System.Random().Next(0, CPU.Length)])).Pointer;
        //public static IntPtr FakeOS() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(OS[new System.Random().Next(0, OS.Length)])).Pointer;



        [Obfuscation(Exclude = true)]
        private static bool VoidPatch()
        {
            return false;
        }

        [Obfuscation(Exclude = true)]
        private static bool VoidPatchTrue(bool __result)
        {
            __result = true;
            return false;
        }

        [Obfuscation(Exclude = true)]
        private static bool VoidPatchFalse(bool __result)
        {
            __result = false;
            return false;
        }

        private static string[] PBU = new string[]
        {
            "MSI Radeon RX 6900 XT GAMING Z TRIO 16GB",
            "Gigabyte Radeon RX 6700 XT Gaming OC 12GB",
            "ASUS DUAL GeForce RTX 2060 6GB OC EVO",
            "Palit GeForce GTX 1050 Ti StormX 4GB",
            "MSI GeForce GTX 1650 D6 Ventus XS OCV2 12GB OC",
            "ASUS GOLD20TH GTX 980 Ti Platinum",
            "ASUS TUF GeForce RTX 3060 12GB OC Gaming",
            "NVIDIA Quadro RTX 4000 8GB",
            "NVIDIA GeForce GTX 1080 Ti",
            "NVIDIA GeForce GTX 1080",
            "NVIDIA GeForce GTX 1070",
            "NVIDIA GeForce GTX 1060 6GB",
            "NVIDIA GeForce GTX 980 Ti"
        };
        private static string[] CPU = new string[]
        {
            "AMD Ryzen 5 3600",
            "AMD Ryzen 7 3700X",
            "AMD Ryzen 7 5800X",
            "AMD Ryzen 9 5900X",
            "AMD Ryzen 9 3900X 12-Core Processor",
            "INTEL CORE I9-10900K",
            "INTEL CORE I7-10700K",
            "INTEL CORE I9-9900K",
            "Intel Core i7-8700K"
        };
        private static string[] Motherboards = new string[]
        {
            "B550 AORUS PRO (Gigabyte Technology Co., Ltd.)",
            "Gigabyte B450M DS3H",
            "Asus AM4 TUF Gaming X570-Plus",
            "ASRock Z370 Taichi"
        };
        private static string[] OS = new string[]
        {
            "Windows 10  (10.0.0) 64bit",
            "Windows 10  (10.0.0) 32bit",
            "Windows 8  (10.0.0) 64bit",
            "Windows 8  (10.0.0) 32bit",
            "Windows 7  (10.0.0) 64bit",
            "Windows 7  (10.0.0) 32bit",
            "Windows Vista 64bit",
            "Windows Vista 32bit",
            "Windows XP 64bit",
            "Windows XP 32bit"
        };

    }
}
