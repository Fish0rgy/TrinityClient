using System;
using System.IO;
using UnityEngine;
using System.Reflection;
using System.Diagnostics;
using UnhollowerRuntimeLib;
using System.Threading.Tasks;
using System.Collections.Generic; 
using MelonLoader;
using Trinity.Events;
using Trinity.Module;
using Trinity.Module.World; 
using Trinity.SDK;
using Trinity.SDK.Patching;
using Trinity.SDK.Security;
using Trinity.SDK.ButtonAPI;
using System.Net;
using VRC.Core; 
using Newtonsoft.Json;
using Trinity.SDK.ButtonAPI.AVI_FAV;
using VRC.SDKBase;
using Trinity.Utilities;
using Trinity.Module.Exploit.MiscExploits;
using System.Linq;

namespace Trinity
{
    public class Main : MelonMod
    {
        public static Main Instance { get; set; }
        public static bool CompLayer { get; set; }
        public Config Config { get; set; } = new Config();
        public Serpent QuickMenuStuff { get; set; }
        public QMNestedButton PlayerButton { get; set; }
        public QMNestedButton DynamicBonesButton { get; set; }
        public QMNestedButton MiscButton { get; set; }
        public QMNestedButton GhostButton { get; set; }
        public QMNestedButton STDButton { get; set; }
        public QMNestedButton GhostTargetButton { get; set; }
        public QMNestedButton JustHButton { get; set; }
        public QMNestedButton MoveAndChillSettings { get; set; }
        public QMNestedButton MovieAndChillButton { get; set; }
        public QMNestedButton SafetyTargetButton { get; set; }
        public QMNestedButton MovementButton { get; set; }
        public QMNestedButton ExploistButton { get; set; }
        public QMNestedButton Eventexploitbutton { get; set; }
        public QMNestedButton Avatarexploitbutton { get; set; }
        public QMNestedButton SpoofersButton { get; set; }
        public QMNestedButton SafetyButton { get; set; }
        public QMNestedButton WorldButton { get; set; }
        public QMNestedButton WorldhacksButton { get; set; }
        public QMNestedButton Murderbutton { get; set; }
        public QMNestedButton Justbbutton { get; set; }
        public QMNestedButton BotButton { get; set; }
        public QMNestedButton Networkbutton { get; set; }
        public QMNestedButton Avatarbutton { get; set; }
        public QMNestedButton SettingsButton { get; set; }
        public QMNestedButton SettingsButtonLoggging { get; set; }
        public QMNestedButton SettingsButtonDumping { get; set; }
        public QMNestedButton SettingsButtonpreformance { get; set; }
        public QMNestedButton SettingsButtonrender { get; set; }
        public QMNestedButton SettingsButtonspoofer { get; set; }
        public QMNestedButton SettingsButtonTheme { get; set; }
        public QMNestedButton Targetbutton { get; set; }
        public QMNestedButton AvatarSettings { get; set; }
        public QMNestedButton MurderSettings { get; set; }
        public QMNestedButton Reuploader { get; set; }
        public QMNestedButton Amongusbutton { get; set; }
        public QMNestedButton Preformancebutton { get; set; }
        public QMNestedButton Privatebotbutton { get; set; }
        public QMNestedButton Publicbotbutton { get; set; }
        public QMNestedButton Zombiebutton { get; set; }
        public QMNestedButton EventQuickMenu { get; set; }
        public QMNestedButton WorldhacksTargetButton { get; set; }
        public QMNestedButton MunchenAdminButton { get; set; }
        public QMNestedButton Magictagbutton { get; set; }
        public QMNestedButton AmongUsSettings { get; set; }
        public QMNestedButton AudioButton { get; set; }
        public QMNestedButton MidnightButton { get; set; }
        public QMNestedButton JubstBSettings { get; set; }
        public QMNestedButton MagicTagSettings { get; set; }
        public QMNestedButton udonexploitbutton { get; set; } 

        public List<BaseModule> Modules { get; set; } = new List<BaseModule>();
        public BaseModule FlyModule = null;
        public BaseModule SpeedModule = null;
        public BaseModule EspModule = null;
        public BaseModule LoudMicModule = null;
        //Item Orbit
        private static VRC_Pickup[] cachedItemPickups;
        private static GameObject centerItemOrbit;
        private static PI selectedPlayerToOrbit;

        //Attach To Head
        private static PI selectedPlayerToAttach;
        private static HumanBodyBones attachmentPoint;
        private static bool attachmentPointOffset;

        public List<OnPlayerJoinEvent> OnPlayerJoinEvents { get; set; } = new List<OnPlayerJoinEvent>();
        public List<OnAssetBundleLoadEvent> OnAssetBundleLoadEvents { get; set; } = new List<OnAssetBundleLoadEvent>();
        public List<OnPlayerLeaveEvent> OnPlayerLeaveEvents { get; set; } = new List<OnPlayerLeaveEvent>();
        public List<OnUpdateEvent> OnUpdateEvents { get; set; } = new List<OnUpdateEvent>();
        public List<OnUdonEvent> OnUdonEvents { get; set; } = new List<OnUdonEvent>();
        public List<OnEventEvent> OnEventEvents { get; set; } = new List<OnEventEvent>();
        public List<OnRPCEvent> OnRPCEvents { get; set; } = new List<OnRPCEvent>();
        public List<OnAvatarLoadedEvent> OnAvatarLoadEvents { get; set; } = new List<OnAvatarLoadedEvent>();
        public List<OnSendOPEvent> OnSendOPEvents { get; set; } = new List<OnSendOPEvent>();
        public List<OnSceneLoadedEvent> OnSceneLoadedEvents { get; set; } = new List<OnSceneLoadedEvent>();
        public List<OnWorldInitEvent> OnWorldInitEvents { get; set; } = new List<OnWorldInitEvent>();
        public List<OnNetworkSanityEvent> OnNetworkSanityEvents { get; set; } = new List<OnNetworkSanityEvent>();
        public List<OnObjectInstantiatedEvent> OnObjectInstantiatedEvents { get; set; } = new List<OnObjectInstantiatedEvent>();
        public List<OnPhotonPeerEvent> OnPhotonPeerEvents { get; set; } = new List<OnPhotonPeerEvent>();
        public List<OnRoundTripEvent> OnRoundTripEvents { get; set; } = new List<OnRoundTripEvent>();

        public static float VarianceFPS = 0f;
        public static int VariancePing = 0;
        public static string fileVersion = "1.8.4.0";
        public override void OnGUI() { }
         
         
        public override void OnApplicationStart()
        {
            SecurityCheck.CheckFile();
            SecurityCheck.CheckSteam();
            Instance = new Main(); 
            ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
            LogHandler.DisplayLogo();
            SecurityCheck.CheckUpdate();
            Directory.CreateDirectory("Trinity\\LoadingScreenMusic");
            MelonCoroutines.Start(Misc.LoadingMusic());
            PU.AddClientUsers(); 
            if (Misc.ModCheck("WorldClient") || Misc.ModCheck("Notorious"))
                CompLayer = true;
            //MelonCoroutines.Start(SpectateMode.VRChat_OnUiManagerInit());
            Task.Run(() =>
            {
                SerpentPatch.InitPatches(); 
            });
        }
        public override void OnUpdate()
        {
            for (int i = 0; i < Instance.OnUpdateEvents.Count; i++) Instance.OnUpdateEvents[i].OnUpdate();

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Process.Start("VRChat.exe", Environment.CommandLine);
                    OnApplicationQuit();
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
            {
                MelonCoroutines.Start(OldKeyBinds.udonNukeKeyBind());
                LogHandler.Log(LogHandler.Colors.Green, "[Keybind] Nuking World.....", true, false);
            }
            
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            for (int i = 0; i < Instance.OnSceneLoadedEvents.Count; i++)
            {
                Instance.OnSceneLoadedEvents[i].OnSceneWasLoadedEvent(buildIndex, sceneName);
            }
        }
        [Obfuscation(Exclude = true)]
        public static void InitMenu()
        {
            try
            {
                //UIU.WaitBitch();
                MelonCoroutines.Start(MenuUI.StartUI());
                Config.Instance = Config.Load(); 
                LogHandler.Log(LogHandler.Colors.Green, "Client UI Initialized!", true, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.Message, true, false);
            }
        }
        public override void OnApplicationQuit()
        {
            Instance.Modules.ToList().ForEach(module =>
            {
                if (module.save)
                    Instance.Config.setConfigBool(module.name, module.toggled);
            });
        }
    }
}