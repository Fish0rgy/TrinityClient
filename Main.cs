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


namespace Trinity
{
    public class Main
    {
        public static Main Instance { get; set; }

        public Config Config { get; set; } = new Config();
        public Serpent QuickMenuStuff { get; set; }
        public QMNestedButton PlayerButton { get; set; }
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
        public QMNestedButton Magictagbutton { get; set; }
        public QMNestedButton AmongUsSettings { get; set; }
        public QMNestedButton AudioButton { get; set; }
        public QMNestedButton MidnightButton { get; set; }
        public QMNestedButton JubstBSettings { get; set; }
        public QMNestedButton MagicTagSettings { get; set; }
        public QMNestedButton udonexploitbutton { get; set; }

        internal List<BaseModule> Modules { get; set; } = new List<BaseModule>();
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
        
        public static void OnGUI() { }

        public static void OnApplicationStart()
        {
            Instance = new Main();
            ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
            LogHandler.DisplayLogo();
            Directory.CreateDirectory("Trinity\\LoadingScreenMusic");
            MelonCoroutines.Start(Misc.LoadingMusic());
            Task.Run(() =>
            {
                SerpentPatch.InitPatches();
            });
        }
        public static void OnUpdate()
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

            // uDoneNuke.
            if (Input.GetKey(KeyCode.K))
            {
                MelonCoroutines.Start(Keybinds.udonNukeKeyBind());
                LogHandler.Log(LogHandler.Colors.Green, "[Keybind] Nuking World...", true, false);
            }
        }
        public static void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            for (int i = 0; i < Instance.OnSceneLoadedEvents.Count; i++)
            {
                Instance.OnSceneLoadedEvents[i].OnSceneWasLoadedEvent(buildIndex, sceneName);
            }
        }
        [Obfuscation(Exclude = true)]
        public static void OnUIInit()
        {
            try { MelonCoroutines.Start(MenuUI.StartUI()); LogHandler.Log(LogHandler.Colors.Green, "Client UI Initialized!", true, false); } catch (Exception ERROR) { LogHandler.Log(LogHandler.Colors.Red, ERROR.Message, true, false); }
        }
        public static void OnApplicationQuit()
        {
            try
            {
                foreach (BaseModule module in Instance.Modules) { if (module.save) { Instance.Config.setConfigBool(module.name, module.toggled); } }
                if (File.Exists(SecurityCheck.key) && SecurityCheck.CleanOnExit(File.ReadAllText(SecurityCheck.key)) == true) { LogHandler.Log(LogHandler.Colors.Yellow, "[Trinity] Shutting down, GoodBye!", false, false); Process.GetCurrentProcess().Kill(); }
                else
                {
                    Process.GetCurrentProcess().Kill();
                    LogHandler.Log(LogHandler.Colors.Red, "Failed to logout, please contact owner!", false, false);
                }
            }
            catch (Exception EX) { LogHandler.Log(LogHandler.Colors.Red, EX.StackTrace, true, false); }
        }
    }
}