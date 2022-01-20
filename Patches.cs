using AmplitudeSDKWrapper;
using Area51.Module.Safety.Photon.NetworkSanity.Core;
using Area51.Module.Safety.Photon.Sanitizers;
using Area51.Module.Settings.Logging;
using Area51.SDK;
using ExitGames.Client.Photon;
using HarmonyLib;
using MelonLoader;
using MoPhoGames.USpeak.Core;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Networking;
using VRC.SDKBase;

namespace Area51
{
    class Patches
    {

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int LoadFrameDelegate(USpeakFrameContainer container, Il2CppStructArray<byte> source, int sourceOffset);
        private delegate void EventDelegate(IntPtr thisPtr, IntPtr eventDataPtr, IntPtr nativeMethodInfo);
        private static readonly List<object> _ourPinnedDelegates = new List<object>();
        private static readonly List<ISanitizer> Sanitizers = new List<ISanitizer>();
        private static IntPtr _ourAvatarPlayableDecode;
        private static IntPtr _ourAvatarPlayableDecodeMethodInfo;

        private static IntPtr _ourSyncPhysicsDecode;
        private static IntPtr _ourSyncPhysicsDecodeMethodInfo;

        private static IntPtr _ourPoseRecorderDecode;
        private static IntPtr _ourPoseRecorderDecodeMethodInfo;

        private static IntPtr _ourPoseRecorderDispatchedUpdate;
        private static IntPtr _ourPoseRecorderDispatchedUpdateMethodInfo;

        private static IntPtr _ourSyncPhysicsDispatchedUpdate;
        private static IntPtr _ourSyncPhysicsDispatchedUpdateMethodInfo;
        public new static HarmonyLib.Harmony Harmony { get; private set; }
        private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Area 51");
        private static string newHWID = "";
        public static List<int> blacklistedPlayers = new List<int>();
        public static readonly RateLimiter _rateLimiter = new RateLimiter();
        private static readonly List<object> OurPinnedDelegates = new List<object>();
        private static readonly Dictionary<string, (int, int)> _ratelimitValues = new Dictionary<string, (int, int)>()
        {
            { "Generic", (500, 500) },
            { "ReceiveVoiceStatsSyncRPC", (348, 64) },
            { "InformOfBadConnection", (64, 6) },
            { "initUSpeakSenderRPC", (256, 6) },
            { "InteractWithStationRPC", (128, 32) },
            { "SpawnEmojiRPC", (128, 6) },
            { "SanityCheck", (256, 32) },
            { "PlayEmoteRPC", (256, 6) },
            { "TeleportRPC", (256, 16) },
            { "CancelRPC", (256, 32) },
            { "SetTimerRPC", (256, 64) },
            { "_DestroyObject", (512, 128) },
            { "_InstantiateObject", (512, 128) },
            { "_SendOnSpawn", (512, 128) },
            { "ConfigurePortal", (512, 128) },
            { "UdonSyncRunProgramAsRPC", (512, 128) }, // <--- Udon is gay
            { "ChangeVisibility", (128, 12) },
            { "PhotoCapture", (128, 32) },
            { "TimerBloop", (128, 16) },
            { "ReloadAvatarNetworkedRPC", (128, 12) },
            { "InternalApplyOverrideRPC", (512, 128) },
            { "AddURL", (64, 6) },
            { "Play", (64, 6) },
            { "Pause", (64, 6) },
            { "SendVoiceSetupToPlayerRPC", (512, 6) },
            { "SendStrokeRPC", (512, 32) }
        };
        public static void Init()
        {

            try
            {
                Instance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(FakeHWID))));
                SafetyPatch();
                Instance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(VoidPatch))));
                Instance.Patch(typeof(AmplitudeWrapper).GetMethods().First((MethodInfo x) => x.Name == "LogEvent" && x.GetParameters().Length == 4), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(VoidPatch))));
                Logg.Log(Logg.Colors.Green, "[Patch] Analystics", false, false);
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, "[Patch] Could not patch Analystics failed\n" + ex, false, false);
            }
            try
            {
                Instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnUdon))));
                Logg.Log(Logg.Colors.Green, "[Patch] Udon", false, false);
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, "[Patch] Could not patch Udon failed\n" + ex, false, false);
            }
            try
            {
               
                Instance.Patch(typeof(LoadBalancingClient).GetMethod("OnEvent"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnEvent))));
                Instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnRPC))));
                Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0", null, null), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OpRaiseEvent))));
             //  try { networksanity(); } catch (Exception ex) { Logg.Log(Logg.Colors.Red, $"[Patch] [Error] NetworkSanity {ex.ToString()}", false, false); }
                Logg.Log(Logg.Colors.Green, "[Patch] Networking", false, false);
            }
            catch
            {
                Logg.Log(Logg.Colors.Red, "[Patch] [Error] Networking", false, false);
            }

            try
            {
                Instance.Patch(typeof(VRC.Core.AssetManagement).GetMethod("Method_Public_Static_Object_Object_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(OnAvatarAssetBundleLoad))));

                Logg.Log(Logg.Colors.Green, "[Patch] AssetBundle", false, false);
            }
            catch
            {
                Logg.Log(Logg.Colors.Red, "[Patch] [Error] AssetBundle", false, false);
            }

            try
            {
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "Log" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Debug))));
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogError" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(DebugError))));
                Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogWarning" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(DebugWarning))));

                Logg.Log(Logg.Colors.Green, "[Patch] Logger", false, false);
            }
            catch
            {
                Logg.Log(Logg.Colors.Red, "[Patch] [Error] Logger", false, false);
            }
            try
            {
                Instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod("Awake"), null, new HarmonyMethod(AccessTools.Method(typeof(Main), ("OnUIInit"))));
            }
            catch
            {

            }
            while (NetworkManager.field_Internal_Static_NetworkManager_0 == null)
            {
                Thread.Sleep(25);
            }

            VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
            VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
            field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(Patches.OnPlayerJoin));
            field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(Patches.OnPlayerLeave));

            Logg.Log(Logg.Colors.Green, "[Patch] All Patching Procedures Are Complete, Now Starting Client", false, false);
        }
        private static HarmonyMethod GetLocalPatch(Type type, string methodName)
        {
            return new HarmonyLib.HarmonyMethod(type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
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
        [Obfuscation(Exclude = true)]
        private static bool DebugError(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                Logg.Log(Logg.Colors.Red, "[UnityError] " + Il2CppSystem.Convert.ToString(__0), false, false);
            return true;
        }
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
            for (int i = 0; i < Main.Instance.OnAssetBundleLoadEventArray.Length; i++)
                if (!Main.Instance.OnAssetBundleLoadEventArray[i].OnAvatarAssetBundleLoad(gameObject, avatarId))
                    return false;

            return true;
        }
        [Obfuscation(Exclude = true)]
        private static bool networksantiytoggle()
        {
            for (int i = 0; i < Main.Instance.OnNetworkSanityArray.Length; i++)
                if (!Main.Instance.OnNetworkSanityArray[i].networksantiytoggle())
                    return false;
            return true;
        }
        [Obfuscation(Exclude = true)]
        private static bool DebugWarning(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                Logg.Log(Logg.Colors.Yellow, "[UnityWarning] " + Il2CppSystem.Convert.ToString(__0), false, false);
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool Debug(ref Il2CppSystem.Object __0)
        {
            if (UnityLogger.Instance == null)
                return true;

            if (UnityLogger.Instance.toggled)
                Logg.Log(Logg.Colors.Green, "[Unity] " + Il2CppSystem.Convert.ToString(__0), false, false);
            return true;
        }
        [Obfuscation(Exclude = true)]
        private static bool OnUdon(string __0, VRC.Player __1)
        {
            for (int i = 0; i < Main.Instance.OnUdonEventArray.Length; i++)
                if (!Main.Instance.OnUdonEventArray[i].OnUdon(__0, __1))
                    return false;
            return true;
        }
        private static bool OnEventPatch(LoadBalancingClient loadBalancingClient, EventData eventData)
        {
            foreach (var sanitizer in Sanitizers)
            {
                if (sanitizer.OnPhotonEvent(loadBalancingClient, eventData))
                    return false;
            }
            return true;
        }
        public unsafe static void networksanity()
        {
            IEnumerable<Type> types;
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null);
            }

            foreach (var t in types)
            {
                if (t.IsAbstract)
                    continue;
                if (!typeof(ISanitizer).IsAssignableFrom(t))
                    continue;

                var sanitizer = Activator.CreateInstance(t) as ISanitizer;
                Sanitizers.Add(sanitizer);
                MelonLogger.Msg($"Added new Sanitizer: {t.Name}");
            }

            unsafe
            {
                var originalMethodPtr = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(LoadBalancingClient).GetMethod(nameof(LoadBalancingClient.OnEvent))).GetValue(null);

                EventDelegate originalDelegate = null;

                void OnEventDelegate(IntPtr thisPtr, IntPtr eventDataPtr, IntPtr nativeMethodInfo)
                {
                    if (eventDataPtr == IntPtr.Zero)
                    {
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                        return;
                    }

                    try
                    {
                        var eventData = new EventData(eventDataPtr);
                        if (OnEventPatch(new LoadBalancingClient(thisPtr), eventData))
                            originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                    }
                    catch (Exception ex)
                    {
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                        MelonLogger.Error(ex.Message);
                    }
                }

                var patchDelegate = new EventDelegate(OnEventDelegate);
                _ourPinnedDelegates.Add(patchDelegate);

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), Marshal.GetFunctionPointerForDelegate(patchDelegate));
                originalDelegate = Marshal.GetDelegateForFunctionPointer<EventDelegate>(originalMethodPtr);
            }

            unsafe
            {
                var originalMethodPtr = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(VRCNetworkingClient).GetMethod(nameof(VRCNetworkingClient.OnEvent))).GetValue(null);

                EventDelegate originalDelegate = null;

                void OnEventDelegate(IntPtr thisPtr, IntPtr eventDataPtr, IntPtr nativeMethodInfo)
                {
                    if (eventDataPtr == IntPtr.Zero)
                    {
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                        return;
                    }

                    var eventData = new EventData(eventDataPtr);
                    if (VRCNetworkingClientOnPhotonEvent(eventData))
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                }

                var patchDelegate = new EventDelegate(OnEventDelegate);
                _ourPinnedDelegates.Add(patchDelegate);

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), Marshal.GetFunctionPointerForDelegate(patchDelegate));
                originalDelegate = Marshal.GetDelegateForFunctionPointer<EventDelegate>(originalMethodPtr);
            }
        }
        private static bool VRCNetworkingClientOnPhotonEvent(EventData eventData)
        {
            foreach (var sanitizer in Sanitizers)
            {
                if (sanitizer.VRCNetworkingClientOnPhotonEvent(eventData))
                    return false;
            }
            return true;
        }

        private static bool VRC_EventLogOnPhotonEvent(EventData eventData)
        {
            return eventData.Code == 6 && _rateLimiter.IsRateLimited(eventData.Sender);
        }

        [Obfuscation(Exclude = true)]
        private static bool FakeHWID(ref string __result)
        {
            if (Patches.newHWID == "")
            {
                Patches.newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9)
                }))).Select(delegate (byte x)
                {
                    byte b = x;
                    return b.ToString("x2");
                }).Aggregate((string x, string y) => x + y);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] HWID New: {Patches.newHWID}", false, false);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] PC Name New: {SystemInfo.deviceName}", false, false);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] Model New: {SystemInfo.deviceModel}", false, false);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] PBU New: {SystemInfo.graphicsDeviceName}", false, false);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] CPU New: {SystemInfo.processorType}", false, false);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] PBU ID New: {SystemInfo.graphicsDeviceID.ToString()}", false, false);
                Logg.Log(Logg.Colors.Green, $"[Spoofer] OS New:{SystemInfo.operatingSystem}", false, false);
            }
            __result = Patches.newHWID;
            return false;
        }
        public unsafe static void SafetyPatch()
        {
            IntPtr intPtr2 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceModel");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr2)), AccessTools.Method(typeof(Patches), "FakeModel", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr3 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceName");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr3)), AccessTools.Method(typeof(Patches), "FakeName", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr4 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceName");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr4)), AccessTools.Method(typeof(Patches), "FakeGBU", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr5 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceID");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr5)), AccessTools.Method(typeof(Patches), "FakeGBUID", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr6 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetProcessorType");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr6)), AccessTools.Method(typeof(Patches), "FakeProcessor", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr7 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetOperatingSystem");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr7)), AccessTools.Method(typeof(Patches), "FakeOS", null, null).MethodHandle.GetFunctionPointer());
        }
        public static IntPtr FakeModel() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Patches.Motherboards[new System.Random().Next(0, Patches.Motherboards.Length)])).Pointer;
        public static IntPtr FakeName() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp("DESKTOP-" + Misc.RandomString(7))).Pointer;
        public static IntPtr FakeGBU() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Patches.PBU[new System.Random().Next(0, Patches.PBU.Length)])).Pointer;
        public static IntPtr FakeGBUID() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Misc.RandomString(12))).Pointer;
        public static IntPtr FakeProcessor() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Patches.CPU[new System.Random().Next(0, Patches.CPU.Length)])).Pointer;
        public static IntPtr FakeOS() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Patches.OS[new System.Random().Next(0, Patches.OS.Length)])).Pointer;
        [Obfuscation(Exclude = true)]
        private static bool OnEvent(EventData __0)
        {
            if (__0 == null)
                return false;
            if (blacklistedPlayers.Contains(__0.Sender))
                return false;

            for (int i = 0; i < Main.Instance.OnEventEventArray.Length; i++)
                if (!Main.Instance.OnEventEventArray[i].OnEvent(__0))
                    return false;
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool OpRaiseEvent(byte __0, Il2CppSystem.Object __1, RaiseEventOptions __2)
        {
            for (int i = 0; i < Main.Instance.OnSendOPEventArray.Length; i++)
                if (!Main.Instance.OnSendOPEventArray[i].OnSendOP(__0, ref __1, ref __2))
                    return false;

            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool OnRPC(VRC.Player __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
        {
            for (int i = 0; i < Main.Instance.OnRPCEventArray.Length; i++)
                if (!Main.Instance.OnRPCEventArray[i].OnRPC(__0, __1, __2, __3, __4))
                    return false;

            return true;
        }

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

        public unsafe static void networksanitypatch()
        {
            IEnumerable<Type> types;
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null);
            }
            var decodeMethodName = "";

            foreach (var methodInfo in typeof(UdonSync).GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!string.IsNullOrEmpty(decodeMethodName))
                    break;

                if (methodInfo.IsAbstract)
                    continue;

                if (!methodInfo.Name.StartsWith("Method_Public_Virtual_Final_New_Void_ValueTypePublicSealed"))
                    continue;

                foreach (var xi in XrefScanner.XrefScan(methodInfo))
                {
                    if (xi.Type != XrefType.Method)
                        continue;

                    var resolvedMethod = xi.TryResolve();

                    if (resolvedMethod == null)
                        continue;

                    if (resolvedMethod.Name == "get_SyncMethod")
                    {
                        decodeMethodName = methodInfo.Name;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(decodeMethodName))
            {
                MelonLogger.Error("Unable to determine target method name, you'll be unprotected against photon exploits.");
                return;
            }

            unsafe
            {
                var originalMethod = (Il2CppMethodInfo*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(AvatarPlayableController).GetMethod(decodeMethodName)).GetValue(null);
                var originalMethodPtr = *(IntPtr*)originalMethod;

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), typeof(FlatBufferSanitizer).GetMethod(nameof(Area51.Module.Safety.Photon.Sanitizers.FlatBufferSanitizer.AvatarPlayableControllerDecodePatch), BindingFlags.Static | BindingFlags.NonPublic).MethodHandle.GetFunctionPointer());

                var methodInfoCopy = (Il2CppMethodInfo*)Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppMethodInfo>());
                *methodInfoCopy = *originalMethod;

                _ourAvatarPlayableDecodeMethodInfo = (IntPtr)methodInfoCopy;
                _ourAvatarPlayableDecode = originalMethodPtr;
            }

            unsafe
            {
                var originalMethod = (Il2CppMethodInfo*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(SyncPhysics).GetMethod(decodeMethodName)).GetValue(null);
                var originalMethodPtr = *(IntPtr*)originalMethod;

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), typeof(FlatBufferSanitizer).GetMethod(nameof(Area51.Module.Safety.Photon.Sanitizers.FlatBufferSanitizer.SyncPhysicsDecodePatch), BindingFlags.Static | BindingFlags.NonPublic).MethodHandle.GetFunctionPointer());

                var methodInfoCopy = (Il2CppMethodInfo*)Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppMethodInfo>());
                *methodInfoCopy = *originalMethod;

                _ourSyncPhysicsDecodeMethodInfo = (IntPtr)methodInfoCopy;
                _ourSyncPhysicsDecode = originalMethodPtr;
            }

            unsafe
            {
                var originalMethod = (Il2CppMethodInfo*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(PoseRecorder).GetMethod(decodeMethodName)).GetValue(null);
                var originalMethodPtr = *(IntPtr*)originalMethod;

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), typeof(FlatBufferSanitizer).GetMethod(nameof(Area51.Module.Safety.Photon.Sanitizers.FlatBufferSanitizer.PoseRecorderDecodePatch), BindingFlags.Static | BindingFlags.NonPublic).MethodHandle.GetFunctionPointer());

                var methodInfoCopy = (Il2CppMethodInfo*)Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppMethodInfo>());
                *methodInfoCopy = *originalMethod;

                _ourPoseRecorderDecodeMethodInfo = (IntPtr)methodInfoCopy;
                _ourPoseRecorderDecode = originalMethodPtr;
            }

            unsafe
            {
                var originalMethod = (Il2CppMethodInfo*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(PoseRecorder).GetMethod(nameof(PoseRecorder.Method_Public_Virtual_Final_New_Void_Single_Single_0))).GetValue(null);
                var originalMethodPtr = *(IntPtr*)originalMethod;

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), typeof(FlatBufferSanitizer).GetMethod(nameof(Area51.Module.Safety.Photon.Sanitizers.FlatBufferSanitizer.PoseRecorderDispatchedUpdatePatch), BindingFlags.Static | BindingFlags.NonPublic).MethodHandle.GetFunctionPointer());

                var methodInfoCopy = (Il2CppMethodInfo*)Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppMethodInfo>());
                *methodInfoCopy = *originalMethod;

                _ourPoseRecorderDispatchedUpdateMethodInfo = (IntPtr)methodInfoCopy;
                _ourPoseRecorderDispatchedUpdate = originalMethodPtr;
            }

            unsafe
            {
                var originalMethod = (Il2CppMethodInfo*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(SyncPhysics).GetMethod(nameof(SyncPhysics.Method_Public_Virtual_Final_New_Void_Single_Single_0))).GetValue(null);
                var originalMethodPtr = *(IntPtr*)originalMethod;

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), typeof(FlatBufferSanitizer).GetMethod(nameof(Area51.Module.Safety.Photon.Sanitizers.FlatBufferSanitizer.SyncPhysicsDispatchedUpdatePatch), BindingFlags.Static | BindingFlags.NonPublic).MethodHandle.GetFunctionPointer());

                var methodInfoCopy = (Il2CppMethodInfo*)Marshal.AllocHGlobal(Marshal.SizeOf<Il2CppMethodInfo>());
                *methodInfoCopy = *originalMethod;

                _ourSyncPhysicsDispatchedUpdateMethodInfo = (IntPtr)methodInfoCopy;
                _ourSyncPhysicsDispatchedUpdate = originalMethodPtr;
            }
            unsafe
            {
                LoadFrameDelegate _loadFrame;
                _loadFrame = (LoadFrameDelegate)Delegate.CreateDelegate(typeof(LoadFrameDelegate), typeof(USpeakFrameContainer).GetMethods().Single(x =>
                {
                    if (!x.Name.StartsWith("Method_Public_Int32_ArrayOf_Byte_Int32_") || x.Name.Contains("_PDM_"))
                        return false;

                    return XrefScanner.XrefScan(x).Count(y => y.Type == XrefType.Method && y.TryResolve() != null) == 4;
                }));
            }
            unsafe
            {
                var originalMethodPtr = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(LoadBalancingClient).GetMethod(nameof(LoadBalancingClient.OnEvent))).GetValue(null);

                EventDelegate originalDelegate = null;

                void OnEventDelegate(IntPtr thisPtr, IntPtr eventDataPtr, IntPtr nativeMethodInfo)
                {
                    if (eventDataPtr == IntPtr.Zero)
                    {
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                        return;
                    }

                    try
                    {
                        var eventData = new EventData(eventDataPtr);
                        if (OnEventPatch(new LoadBalancingClient(thisPtr), eventData))
                            originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                    }
                    catch (Exception ex)
                    {
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                        MelonLogger.Error(ex.Message);
                    }
                }

                var patchDelegate = new EventDelegate(OnEventDelegate);
                _ourPinnedDelegates.Add(patchDelegate);

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), Marshal.GetFunctionPointerForDelegate(patchDelegate));
                originalDelegate = Marshal.GetDelegateForFunctionPointer<EventDelegate>(originalMethodPtr);
            }

            unsafe
            {
                var originalMethodPtr = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(typeof(VRCNetworkingClient).GetMethod(nameof(VRCNetworkingClient.OnEvent))).GetValue(null);

                EventDelegate originalDelegate = null;

                void OnEventDelegate(IntPtr thisPtr, IntPtr eventDataPtr, IntPtr nativeMethodInfo)
                {
                    if (eventDataPtr == IntPtr.Zero)
                    {
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                        return;
                    }

                    var eventData = new EventData(eventDataPtr);
                    if (VRCNetworkingClientOnPhotonEvent(eventData))
                        originalDelegate(thisPtr, eventDataPtr, nativeMethodInfo);
                }

                var patchDelegate = new EventDelegate(OnEventDelegate);
                _ourPinnedDelegates.Add(patchDelegate);

                MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPtr), Marshal.GetFunctionPointerForDelegate(patchDelegate));
                originalDelegate = Marshal.GetDelegateForFunctionPointer<EventDelegate>(originalMethodPtr);
            }
        }

        //private static bool VRCNetworkingClientOnPhotonEvent(EventData eventData)
        //{
        //    for (int i = 0; i < Main.Instance.onVrcEventArray.Length; i++)
        //        if (!Main.Instance.onVrcEventArray[i].VRCNetworkingClientOnPhotonEvent(eventData))
        //            return false;
        //    return true;
        //}

        //private static bool OnEventPatch(LoadBalancingClient loadBalancingClient, EventData eventData)
        //{
        //    for (int i = 0; i < Main.Instance.onPhotonEventArray.Length; i++)
        //        if (!Main.Instance.onPhotonEventArray[i].OnEventPatch(loadBalancingClient, eventData))
        //            return false;
        //    return true;
        //}
    }
}