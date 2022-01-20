using Area51.Events;
using Area51.Module;
using Area51.Module.Bot.Local;
using Area51.Module.Exploit;
using Area51.Module.Movement;
using Area51.Module.Player;
using Area51.Module.Safety;
using Area51.Module.Safety.Photon;
using Area51.Module.Settings.DumpEvents;
using Area51.Module.Settings.Logging;
using Area51.Module.Settings.Preformance;
using Area51.Module.Settings.Render;
using Area51.Module.Settings.Spoofer;
using Area51.Module.Settings.Theme;
using Area51.Module.World;
using Area51.Module.World.World_Hacks.Among_Us;
using Area51.Module.World.World_Hacks.Murder_4;
using Area51.SDK;
using Area51.SDK.ButtonAPI;
using Area51.SDK.Security;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC.Core;

namespace Area51
{
    public class Main
    {
        public static Main Instance { get; set; }
        public Config Config { get; set; } = new Config();
        public QuickMenuStuff QuickMenuStuff { get; set; }
        public QMNestedButton PlayerButton { get; set; }
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
        public QMNestedButton SettingsButtonpreformance { get; set; }
        public QMNestedButton SettingsButtonrender { get; set; }
        public QMNestedButton SettingsButtonspoofer { get; set; }
        public QMNestedButton SettingsButtonTheme { get; set; }
        public QMNestedButton Targetbutton { get; set; }
        public QMNestedButton Amongusbutton { get; set; }
        public QMNestedButton Preformancebutton { get; set; }
        public QMNestedButton Privatebotbutton { get; set; }
        public QMNestedButton Publicbotbutton { get; set; }
        public QMNestedButton Zombiebutton { get; set; }

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
        public OnPlayerJoinEvent[] OnPlayerJoinEventArray { get; set; } = new OnPlayerJoinEvent[0];
        public OnAssetBundleLoadEvent[] OnAssetBundleLoadEventArray { get; set; } = new OnAssetBundleLoadEvent[0];
        public OnPlayerLeaveEvent[] OnPlayerLeaveEventArray { get; set; } = new OnPlayerLeaveEvent[0];
        public OnUpdateEvent[] OnUpdateEventArray { get; set; } = new OnUpdateEvent[0];
        public OnUdonEvent[] OnUdonEventArray { get; set; } = new OnUdonEvent[0];
        public OnEventEvent[] OnEventEventArray { get; set; } = new OnEventEvent[0];
        public OnAvatarLoadedEvent[] OnAvatarLoadEventArray { get; set; } = new OnAvatarLoadedEvent[0];
        public OnRPCEvent[] OnRPCEventArray { get; set; } = new OnRPCEvent[0];
        public OnSendOPEvent[] OnSendOPEventArray { get; set; } = new OnSendOPEvent[0];
        public OnSceneLoadedEvent[] OnSceneLoadedEventArray { get; set; } = new OnSceneLoadedEvent[0];
        public OnWorldInitEvent[] OnWorldInitEventArray { get; set; } = new OnWorldInitEvent[0];
        public OnNetworkSanityEvent[] OnNetworkSanityArray { get; set; } = new OnNetworkSanityEvent[0];

        public static void OnApplicationStart()
        {
            Instance = new Main();
            ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
            Logg.DisplayLogo();
            Task.Run(() => { Patches.Init(); });
        }


        public static void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    Process.Start("VRChat.exe", Environment.CommandLine);
                    OnApplicationQuit();
                }
            }
            for (int i = 0; i < Instance.OnUpdateEventArray.Length; i++)
                Instance.OnUpdateEventArray[i].OnUpdate();

            if (Input.GetKey(KeyCode.K))
            {    
                MelonCoroutines.Start(Keybinds.SendNineByKeybind());
                Logg.Log(Logg.Colors.Green, "[Exploit] DDosing Ur Fat Sister Wheelchair!", false, false);
                Logg.LogDebug("[Exploit] Sending -> HowAboutYouEventSomeBitches v4!");
            }

            if (Input.GetKey(KeyCode.P))
            {
                MelonCoroutines.Start(Keybinds.LogInfo());
            }

        }


        public static void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            for (int i = 0; i < Instance.OnSceneLoadedEventArray.Length; i++)
                Instance.OnSceneLoadedEventArray[i].OnSceneWasLoadedEvent(buildIndex, sceneName);
        }


        [Obfuscation(Exclude = true)]
        private static void OnUIInit()
        {

            Instance.QuickMenuStuff = new QuickMenuStuff();
           
            QMTab mainTab = new QMTab("Area 51 Client", "Area 51 Client", "Im Tupper Show Me ur Titties!", Instance.QuickMenuStuff.Button_NameplateVisibleIcon.sprite);
            Instance.WorldButton = new QMNestedButton(mainTab.menuTransform, "World", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.WorldhacksButton = new QMNestedButton(Instance.WorldButton.menuTransform, "World Hacks", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.Amongusbutton = new QMNestedButton(Instance.WorldhacksButton.menuTransform, "Among Us", Instance.QuickMenuStuff.Button_SocialIcon.sprite);
            Instance.Murderbutton = new QMNestedButton(Instance.WorldhacksButton.menuTransform, "Murder 4", Instance.QuickMenuStuff.Button_SocialIcon.sprite);
            Instance.PlayerButton = new QMNestedButton(mainTab.menuTransform, "Player", Instance.QuickMenuStuff.Button_SocialIcon.sprite);
            Instance.MovementButton = new QMNestedButton(mainTab.menuTransform, "Movement", Instance.QuickMenuStuff.StandIcon.sprite);
            Instance.ExploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits", Instance.QuickMenuStuff.Button_SafetyIcon.sprite);
            Instance.Eventexploitbutton = new QMNestedButton(Instance.ExploistButton.menuTransform, "Event Exploits", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.Avatarexploitbutton = new QMNestedButton(Instance.ExploistButton.menuTransform, "Avatar Exploits", Instance.QuickMenuStuff.Button_AvatarsIcon.sprite);
            Instance.SafetyButton = new QMNestedButton(mainTab.menuTransform, "Safety", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.Networkbutton = new QMNestedButton(Instance.SafetyButton.menuTransform, "Network Saftey", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.Avatarbutton = new QMNestedButton(Instance.SafetyButton.menuTransform, "Avatar Saftey", Instance.QuickMenuStuff.Button_AvatarsIcon.sprite);
            Instance.BotButton = new QMNestedButton(mainTab.menuTransform, "Bot", Instance.QuickMenuStuff.Button_NameplateVisibleIcon.sprite);
            Instance.Privatebotbutton = new QMNestedButton(Instance.BotButton.menuTransform, "Local handler", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.Publicbotbutton = new QMNestedButton(Instance.BotButton.menuTransform, "API Handler", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.SettingsButton = new QMNestedButton(mainTab.menuTransform, "Settings", Instance.QuickMenuStuff.Button_RespawnIcon.sprite);
            Instance.SettingsButtonpreformance = new QMNestedButton(Instance.SettingsButton.menuTransform, "Preformance", Instance.QuickMenuStuff.Button_WorldsIcon.sprite);
            Instance.SettingsButtonrender = new QMNestedButton(Instance.SettingsButton.menuTransform, "Render", Instance.QuickMenuStuff.Button_SocialIcon.sprite);
            Instance.SettingsButtonLoggging = new QMNestedButton(Instance.SettingsButton.menuTransform, "Loggging", Instance.QuickMenuStuff.Button_RespawnIcon.sprite);
            Instance.SettingsButtonspoofer = new QMNestedButton(Instance.SettingsButton.menuTransform, "Spoofers", Instance.QuickMenuStuff.Button_RespawnIcon.sprite);
            Instance.SettingsButtonTheme = new QMNestedButton(Instance.SettingsButton.menuTransform, "Theme", Instance.QuickMenuStuff.Button_RespawnIcon.sprite);
            Instance.Modules.Add(new JoinByID());
            Instance.Modules.Add(new Rejoin());
            Instance.Modules.Add(new CopyWID());
            Instance.Modules.Add(new InstanceLock());
            Instance.Modules.Add(new FreezeItems());
            Instance.Modules.Add(new StartGame());
            Instance.Modules.Add(new AbortGame());
            Instance.Modules.Add(new BystanderWin());
            Instance.Modules.Add(new MurderWin());
            Instance.Modules.Add(new KillAll());
            Instance.Modules.Add(new BlindAll());
            Instance.Modules.Add(new ForcePickup());
            Instance.Modules.Add(new BringItems());
            Instance.Modules.Add(new BringKnife());
            Instance.Modules.Add(new BringRevolver());
            Instance.Modules.Add(new BringSmoke());

            Instance.Modules.Add(new A_StartGame());
            Instance.Modules.Add(new A_AbortGame());
            Instance.Modules.Add(new A_MurderWin());
            Instance.Modules.Add(new A_BystandersWin());
            Instance.Modules.Add(new A_CallMeeting());
            Instance.Modules.Add(new A_ReportBody());
            Instance.Modules.Add(new A_CloseVote());
            Instance.Modules.Add(new A_SkipVote());
            Instance.Modules.Add(new A_KillAll());
            Instance.Modules.Add(new A_TaskDone());

            Instance.Modules.Add(new AviToID());
            Instance.Modules.Add(new FPSUnlocker());
            Instance.Modules.Add(new XBoxMic());
            Instance.Modules.Add(new LoudMic());
            Instance.Modules.Add(new HeadFlipper());
            Instance.Modules.Add(new CopyUserID());
            Instance.Modules.Add(new HideSelf());

            Instance.Modules.Add(new Speed());
            Instance.Modules.Add(new Fly());

            //photon
            Instance.Modules.Add(new USpeakEarRape());
            Instance.Modules.Add(new Event9V3());
            //rpc
            //udon
            Instance.Modules.Add(new UdonSpam());
            //serialization
            Instance.Modules.Add(new FreezePlayer());
         //   Instance.Modules.Add(new ());
            //Avatar
            Instance.Modules.Add(new VRCA());
            Instance.Modules.Add(new VRCW());
            Instance.Modules.Add(new AudioCrash());
            Instance.Modules.Add(new VoidBypass());
            Instance.Modules.Add(new EthosBypass());
            Instance.Modules.Add(new CorreuptPC());
            Instance.Modules.Add(new AssetBundleCrash());
            Instance.Modules.Add(new GameClose());
            Instance.Modules.Add(new QuestCrash());

            //Photonbots
            Instance.Modules.Add(new ExploitLogs());
            //photonevents
            Instance.Modules.Add(new RateLimit());
            Instance.Modules.Add(new PhotonProtection());

            //avatar
            Instance.Modules.Add(new AntiAviCrash());


            //Mimic
            Instance.Modules.Add(new Join());
            Instance.Modules.Add(new KIllBots());

            //logging 
            Instance.Modules.Add(new OPSendLogger());
            Instance.Modules.Add(new AssetBundleLogger());
            Instance.Modules.Add(new AvatarLogger());
            Instance.Modules.Add(new UdonLogger());
            Instance.Modules.Add(new EventLogger());
            Instance.Modules.Add(new RpcLogger());
            Instance.Modules.Add(new UnityLogger());
            Instance.Modules.Add(new JoinLogger());
            Instance.Modules.Add(new Dump1());
            Instance.Modules.Add(new Dump6());
            Instance.Modules.Add(new Dump7());
            Instance.Modules.Add(new Dump209());
            //Preformace
            Instance.Modules.Add(new HideVideoPlayers());
            Instance.Modules.Add(new HideChairs());
            Instance.Modules.Add(new HidePickUps());

            Instance.Modules.Add(new ConsoleClear());
            Instance.Modules.Add(new QuickRestart());
            Instance.Modules.Add(new RestartAndRejoin());

            //render 
            Instance.Modules.Add(new PlayerList());
            Instance.Modules.Add(new DebugLog());
            Instance.Modules.Add(new TriggerESP());
            Instance.Modules.Add(new ObjectESP());
            Instance.Modules.Add(new CapsuleEsp());
            Instance.Modules.Add(new CustomNameplates());
            //ping spoofer
            Instance.Modules.Add(new FPSSpoofer());
            Instance.Modules.Add(new PingSpoofer());
            //ping Theme
            Instance.Modules.Add(new Area51Theme());
            Instance.Modules.Add(new munchen());

            foreach (BaseModule module in Instance.Modules)
                module.OnUIInit();

            //targetmenu
            new QMSingleButton(Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "VRCA", "Download Users VRCA", null, delegate
            {

                using (var wc = new WebClient { Headers = { "User-Agent: Other" } })
                {
                    ApiAvatar avatar = PlayerWrapper.GetByUsrID(Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                    wc.DownloadFileAsync(new Uri(avatar.assetUrl), "Area51/VRCA/" + avatar.name);
                    Logg.Log(Logg.Colors.Grey, "Downloaded Selected User VRCA Completed", false, false);
                    Logg.LogDebug("[Ripper] -> Downloaded Selected User VRCA Completed!");
                }

            });

            new QMSingleButton(Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "ForceClone", "ForceClone", null, delegate
            {
                ApiAvatar avatar = PlayerWrapper.GetByUsrID(Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                if (avatar.releaseStatus == "public")
                    PlayerWrapper.ChangeAvatar(avatar.id);
                Logg.LogDebug("[Info] -> ForceClone Completed!");
            });


            new QMSingleButton(Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "UserID", "UserID", null, delegate
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                if (SelectedPlayer.id != "")
                    Misc.SetClipboard(SelectedPlayer.id);
                Logg.LogDebug("[Info] -> Coppied UserID to clipboard.");
            });

            new QMSingleButton(Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "AvatarID", "AvatarID", null, delegate
            {
                ApiAvatar SelectedPlayer = PlayerWrapper.GetByUsrID(Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                if (SelectedPlayer.id != "")
                    Misc.SetClipboard(SelectedPlayer.id);
                Logg.LogDebug("[Info] -> Coppied AvatarID to clipboard.");
            });

            new QMSingleButton(Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "Teleport", "Teleport to selected user.", null, delegate
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                PlayerWrapper.Teleport(PlayerWrapper.GetByUsrID(Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0));
                Logg.LogDebug($"[Info] -> Teleported To: {SelectedPlayer.displayName}");
            });
          
            SecurityCheck.LoginLog();
            APIUser currentUser = APIUser.CurrentUser;
            Logg.Log(Logg.Colors.Green, "UI Setup Done!", false, false);
            QuickMenuStuff.ThemeUI();
            PlayerWrapper.ChangeAvatar("avtr_c4961195-1980-4a98-bb95-3cbe0e063463");
            Console.Title = $"Area 51 Private Client | User: {currentUser.displayName}";
        }

        public static void OnGUI()
        {
            
        }

        public static void OnApplicationQuit()
        {
            foreach (BaseModule module in Instance.Modules)
            {
                if (module.save)
                {
                    Instance.Config.setConfigBool(module.name, module.toggled);
                }
            }

            Process.GetCurrentProcess().Kill();
        }

    }
}
