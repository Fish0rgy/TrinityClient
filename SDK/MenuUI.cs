using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Trinity.Module;
using Trinity.Module.Bot.Local;
using Trinity.Module.Exploit;
using Trinity.Module.Exploit.Photon_Exploit;
using Trinity.Module.Exploit.UdonExploits;
using Trinity.Module.Movement;
using Trinity.Module.Player;
using Trinity.Module.Player.Audio;
using Trinity.Module.Safety;
using Trinity.Module.Safety.Avatar;
using Trinity.Module.Safety.Photon;
using Trinity.Module.Settings.Logging;
using Trinity.Module.Settings.Preformance;
using Trinity.Module.Settings.Render;
using Trinity.Module.Settings.Theme;
using Trinity.Module.TargetMenu;
using Trinity.Module.TargetMenu.Murder4_Settings;
using Trinity.Module.TargetMenu.SafetySettings;
using Trinity.Module.TargetMenu.World_Hacks.AmongUs_Settings;
using Trinity.Module.TargetMenu.World_Hacks.JustB;
using Trinity.Module.TargetMenu.World_Hacks.MagicTag;
using Trinity.Module.TargetMenu.World_Hacks.MovieAndChill;
using Trinity.Module.World;
using Trinity.Module.World.World_Hacks;
using Trinity.Module.World.World_Hacks.Among_Us;
using Trinity.Module.World.World_Hacks.Just_B;
using Trinity.Module.World.World_Hacks.Just_H;
using Trinity.Module.World.World_Hacks.MagicFreezeTag;
using Trinity.Module.World.World_Hacks.MovieAndChill;
using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.Module.World.World_Hacks.TrinityEngine;
using Trinity.Modules.Exploits;
using Trinity.SDK.ButtonAPI;
using Trinity.SDK.Security;
using Trinity.Utilities;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;

namespace Trinity.SDK
{
    class MenuUI
    {

        public static IEnumerator StartUI()
        {
            var pos = new Vector3(272, 964, 0);
            Console.Title = $"Trinity Private Client | User: {APIUser.CurrentUser.displayName}";
            Main.Instance.QuickMenuStuff = new Serpent();
            QMTab mainTab = new QMTab("Trinity Client", "", "What's a client!", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Serpent.Spacer(mainTab.menuTransform);

            Main.Instance.WorldButton = new QMNestedButton(mainTab.menuTransform, "World", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            Main.Instance.WorldhacksButton = new QMNestedButton(Main.Instance.WorldButton.menuTransform, "World Hacks", QMButtonIcons.LoadSpriteFromFile(Serpent.WorldHacksIconPath));
            Main.Instance.udonexploitbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Area-51 Engine", QMButtonIcons.LoadSpriteFromFile(Serpent.udonManagerPath));
            Main.Instance.Zombiebutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Zombie Tag", QMButtonIcons.LoadSpriteFromFile(Serpent.zombiePath));
            Main.Instance.Amongusbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Among Us", QMButtonIcons.LoadSpriteFromFile(Serpent.amogusPath));
            Main.Instance.Murderbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Murder 4", QMButtonIcons.LoadSpriteFromFile(Serpent.murder4Path));
            Main.Instance.Justbbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Just B", QMButtonIcons.LoadSpriteFromFile(Serpent.justbPath));
            Main.Instance.JustHButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Just H", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            Main.Instance.Magictagbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Magic Tag", QMButtonIcons.LoadSpriteFromFile(Serpent.MGKPath));
            Main.Instance.MovieAndChillButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Movie & Chill", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            Main.Instance.PlayerButton = new QMNestedButton(mainTab.menuTransform, "Player", QMButtonIcons.LoadSpriteFromFile(Serpent.PlayerIconPath));
            Main.Instance.AudioButton = new QMNestedButton(Main.Instance.PlayerButton.menuTransform, "Audio Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Main.Instance.MovementButton = new QMNestedButton(mainTab.menuTransform, "Movement", QMButtonIcons.LoadSpriteFromFile(Serpent.MovmentPath));
            Main.Instance.ExploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarExploitPath));
            Main.Instance.Eventexploitbutton = new QMNestedButton(Main.Instance.ExploistButton.menuTransform, "Event Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.Avatarexploitbutton = new QMNestedButton(Main.Instance.ExploistButton.menuTransform, "Avatar Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Main.Instance.SafetyButton = new QMNestedButton(mainTab.menuTransform, "Safety", QMButtonIcons.LoadSpriteFromFile(Serpent.SafetyIconPath));
            Main.Instance.Networkbutton = new QMNestedButton(Main.Instance.SafetyButton.menuTransform, "Network Saftey", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.Avatarbutton = new QMNestedButton(Main.Instance.SafetyButton.menuTransform, "Avatar Saftey", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarExploitPath));
            Main.Instance.BotButton = new QMNestedButton(mainTab.menuTransform, "Bot", QMButtonIcons.LoadSpriteFromFile(Serpent.TheBotsPath));
            Main.Instance.Privatebotbutton = new QMNestedButton(Main.Instance.BotButton.menuTransform, "Local handler", QMButtonIcons.LoadSpriteFromFile(Serpent.PlayerIconPath));
            Main.Instance.SettingsButton = new QMNestedButton(mainTab.menuTransform, "Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.SettingsIconPath));
            Main.Instance.SettingsButtonpreformance = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Preformance", QMButtonIcons.LoadSpriteFromFile(Serpent.rocketPath));
            Main.Instance.SettingsButtonrender = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Render", QMButtonIcons.LoadSpriteFromFile(Serpent.renderPath));
            Main.Instance.SettingsButtonLoggging = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Logging", QMButtonIcons.LoadSpriteFromFile(Serpent.loggingPath));
            //Main.Instance.SettingsButtonDumping = new QMNestedButton(Main.Instance.SettingsButtonLoggging.menuTransform, "Event\nDumping",  QMButtonIcons.LoadSpriteFromFile(Serpent.SavePath));
            Main.Instance.SettingsButtonTheme = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Theme", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            //targetmenu
            Main.Instance.Targetbutton = new QMNestedButton(Main.Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "Trinity", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Main.Instance.AvatarSettings = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "Avatar", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Main.Instance.SafetyTargetButton = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "Safety Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.WorldhacksTargetButton = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "World Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            Main.Instance.MurderSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Murder 4 Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.murder4Path));
            Main.Instance.AmongUsSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Among Us Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.amogusPath));
            Main.Instance.MagicTagSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Magic Tag Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.MGKPath));
            Main.Instance.JubstBSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Just B Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.justbPath));
            Main.Instance.MoveAndChillSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Movie&Chill+ Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));

            ////world
            //Main.Instance.Modules.Add(new JoinByID());
            //Main.Instance.Modules.Add(new Rejoin());
            //Main.Instance.Modules.Add(new CopyWID());
            //Main.Instance.Modules.Add(new InstanceLock());
            //Main.Instance.Modules.Add(new FreezeItems());
            ////just b  
            //Main.Instance.Modules.Add(new BlindPeople());
            //Main.Instance.Modules.Add(new VIPSpoofer());
            //Main.Instance.Modules.Add(new Room1());
            //Main.Instance.Modules.Add(new Room2());
            //Main.Instance.Modules.Add(new Room3());
            //Main.Instance.Modules.Add(new Room4());
            //Main.Instance.Modules.Add(new Room5());
            //Main.Instance.Modules.Add(new Room6());
            //Main.Instance.Modules.Add(new Room7());

            ////Just H
            //Main.Instance.Modules.Add(new BypassVROnly());

            ////Movie & Chill +
            //Main.Instance.Modules.Add(new Teleport_Everyone());
            ////murder4
            //Main.Instance.Modules.Add(new MurderAssign());
            //Main.Instance.Modules.Add(new DetectiveAssign());
            //Main.Instance.Modules.Add(new StartGame());
            //Main.Instance.Modules.Add(new AbortGame());
            //Main.Instance.Modules.Add(new BystanderWin());
            //Main.Instance.Modules.Add(new MurderWin());
            //Main.Instance.Modules.Add(new KillAll());
            //Main.Instance.Modules.Add(new KillLoop());
            //Main.Instance.Modules.Add(new BlindAll());
            //Main.Instance.Modules.Add(new FlashLoop());
            //Main.Instance.Modules.Add(new ForcePickup());
            ////Main.Instance.Modules.Add(new BringItems());
            //Main.Instance.Modules.Add(new BringKnife());
            //Main.Instance.Modules.Add(new BringRevolver());
            ////Main.Instance.Modules.Add(new BringSmoke());
            ////ain.Instance.Modules.Add(new CloseDoors());
            //Main.Instance.Modules.Add(new LockDoors());
            ////Main.Instance.Modules.Add(new ShoveDoors());
            //Main.Instance.Modules.Add(new OpenDoors());

            ////MagicTag hax
            //Main.Instance.Modules.Add(new GiveTagger());
            //Main.Instance.Modules.Add(new GiveRunner());
            //Main.Instance.Modules.Add(new Unfreeze());

            ////amongus
            //Main.Instance.Modules.Add(new A_StartGame());
            //Main.Instance.Modules.Add(new A_AbortGame());
            //Main.Instance.Modules.Add(new A_MurderWin());
            //Main.Instance.Modules.Add(new A_BystandersWin());
            //Main.Instance.Modules.Add(new A_CallMeeting());
            //Main.Instance.Modules.Add(new A_ReportBody());
            //Main.Instance.Modules.Add(new A_CloseVote());
            //Main.Instance.Modules.Add(new A_SkipVote());
            //Main.Instance.Modules.Add(new A_KillAll());
            //Main.Instance.Modules.Add(new A_TaskDone());
            //Main.Instance.Modules.Add(new A_AssignImposter());
            //Main.Instance.Modules.Add(new A_AssignCrew());

            ////Trinity CheatEngine
            //Main.Instance.Modules.Add(new GetTriggerList());
            //Main.Instance.Modules.Add(new GetUdonEventList());
            //Main.Instance.Modules.Add(new CustomTriggerEvent());
            //Main.Instance.Modules.Add(new CustomUdonEvent());
            //Main.Instance.Modules.Add(new TriggerExploitFinder());
            //Main.Instance.Modules.Add(new UdonExploitFinder());
            //Main.Instance.Modules.Add(new TriggerNuke());
            //Main.Instance.Modules.Add(new UdonNuke());

            ////Player
            //Main.Instance.Modules.Add(new AvatarID());
            //Main.Instance.Modules.Add(new FPSUnlocker());
            //Main.Instance.Modules.Add(new LoudMic());
            //Main.Instance.Modules.Add(new HeadFlipper());
            //Main.Instance.Modules.Add(new CopyUserID());
            //Main.Instance.Modules.Add(new HideSelf());
            //Main.Instance.Modules.Add(new DrippedOut());
            ////player>Audio 
            //Main.Instance.Modules.Add(new XBoxMicd());
            //Main.Instance.Modules.Add(new _16k());
            //Main.Instance.Modules.Add(new _18k());
            //Main.Instance.Modules.Add(new _24k());
            //Main.Instance.Modules.Add(new _512k());

            ////Movement
            //Main.Instance.Modules.Add(new Speed());
            //Main.Instance.Modules.Add(new Fly());
            //Main.Instance.Modules.Add(new InfinityJump());
            ////Photon Exploit
            //Main.Instance.Modules.Add(new USpeakEarRape());
            //Main.Instance.Modules.Add(new Freecam());
            //Main.Instance.Modules.Add(new SendRPCDebug());
            //Main.Instance.Modules.Add(new ItemLag());
            //Main.Instance.Modules.Add(new FakeLag());
            //Main.Instance.Modules.Add(new FakeLagMic());
            //Main.Instance.Modules.Add(new FreezePlayers());



            ////Avatar
            //Main.Instance.Modules.Add(new AudioCrash());
            //Main.Instance.Modules.Add(new VoidBypass());
            //// Main.Instance.Modules.Add(new EthosBypass());
            //Main.Instance.Modules.Add(new CorreuptPC());
            //Main.Instance.Modules.Add(new AssetBundleCrash());
            //Main.Instance.Modules.Add(new GameClose());
            //Main.Instance.Modules.Add(new QuestCrash());
            //Main.Instance.Modules.Add(new VRCA());
            //Main.Instance.Modules.Add(new VRCW());
            ////photon protection 
            ////Main.Instance.Modules.Add(new ExploitLogs());
            //Main.Instance.Modules.Add(new PhotonProtection());
            //Main.Instance.Modules.Add(new AntiUdon());
            //Main.Instance.Modules.Add(new AntiRPC());
            ////avatar protection 
            //Main.Instance.Modules.Add(new AntiMesh());
            //Main.Instance.Modules.Add(new AntiShader());
            //Main.Instance.Modules.Add(new AntiCloth());
            //Main.Instance.Modules.Add(new AntiAudio());
            //Main.Instance.Modules.Add(new AntiBones());
            //Main.Instance.Modules.Add(new AntiAnimators());
            //Main.Instance.Modules.Add(new AntiColliders());
            //Main.Instance.Modules.Add(new AntiLight());
            //Main.Instance.Modules.Add(new AntiParticles());
            //Main.Instance.Modules.Add(new AntiMatirials());

            ////Mimic - Photonbots
            //Main.Instance.Modules.Add(new CuddleBot1());
            //Main.Instance.Modules.Add(new CuddleBot2());
            //Main.Instance.Modules.Add(new KIllBots());

            ////logging 
            //Main.Instance.Modules.Add(new OPSendLogger());
            //Main.Instance.Modules.Add(new AssetBundleLogger());
            //Main.Instance.Modules.Add(new AvatarLogger());
            //Main.Instance.Modules.Add(new VCRALogger());
            //Main.Instance.Modules.Add(new UdonLogger());
            //Main.Instance.Modules.Add(new EventLogger());
            //Main.Instance.Modules.Add(new RpcLogger());
            //Main.Instance.Modules.Add(new UnityLogger());
            //Main.Instance.Modules.Add(new JoinLogger());

            ////Performance 
            //Main.Instance.Modules.Add(new ConsoleClear());
            //Main.Instance.Modules.Add(new ReLogin());
            //Main.Instance.Modules.Add(new Logout());
            //Main.Instance.Modules.Add(new HideVideoPlayers());
            //Main.Instance.Modules.Add(new HideChairs());
            //Main.Instance.Modules.Add(new HidePickUps());
            //Main.Instance.Modules.Add(new RestartAndRejoin());
            //Main.Instance.Modules.Add(new QuickRestart());
            ////render 
            //Main.Instance.Modules.Add(new PlayerList());
            //Main.Instance.Modules.Add(new DebugLog());
            //Main.Instance.Modules.Add(new CustomNameplates());
            //Main.Instance.Modules.Add(new TriggerESP());
            //Main.Instance.Modules.Add(new ObjectESP());
            //Main.Instance.Modules.Add(new CapsuleEsp());
            ////Theme
            //Main.Instance.Modules.Add(new SerpentTheme());
            //Main.Instance.Modules.Add(new RetroTheme());
            //Main.Instance.Modules.Add(new munchen());
            //Main.Instance.Modules.Add(new CustomBGImage());
            //Main.Instance.Modules.Add(new ButtonsGreen());
            //Main.Instance.Modules.Add(new ButtonsBlue());
            //Main.Instance.Modules.Add(new ButtonsRed());
            //Main.Instance.Modules.Add(new ButtonsMagenta());

            ////targetmenu
            //Main.Instance.Modules.Add(new AvatSelected());
            //Main.Instance.Modules.Add(new DownloadVRCSelected());
            //Main.Instance.Modules.Add(new ForceClone());
            //Main.Instance.Modules.Add(new Tel2User());
            //Main.Instance.Modules.Add(new UserIDSelected());
            //Main.Instance.Modules.Add(new TargetOrbitch());
            //Main.Instance.Modules.Add(new VoiceIM());

            ////targetmenu>Movie&Chill+
            //Main.Instance.Modules.Add(new TargetMCLag());
            //Main.Instance.Modules.Add(new TargetMCTeleport());

            ////targetmenu>SafetySettings
            //Main.Instance.Modules.Add(new TargetAntiPhoton());
            //Main.Instance.Modules.Add(new TargetAntiUdon());
            //Main.Instance.Modules.Add(new TargetAntiRPC());

            ////targetmenu>Battle Discs
            //Main.Instance.Modules.Add(new SendEvent());

            ////targetmenu>Just B
            //Main.Instance.Modules.Add(new LockPlayerMovement());
            //Main.Instance.Modules.Add(new ForceRespawn());


            ////target>Magic Tag
            //Main.Instance.Modules.Add(new FreezePlayer());
            //Main.Instance.Modules.Add(new MakeTagger());
            //Main.Instance.Modules.Add(new MakeRunner());

            ////targetmenu>Among Us
            //Main.Instance.Modules.Add(new AssignImposter());
            //Main.Instance.Modules.Add(new AssignCrew());
            //Main.Instance.Modules.Add(new AForceSpawn());
            //Main.Instance.Modules.Add(new AmongUsKill());

            ////targetmenu>Murder 4
            //Main.Instance.Modules.Add(new MurderAssignTarget());
            //Main.Instance.Modules.Add(new DetectiveAssignTarget());
            //Main.Instance.Modules.Add(new BystanderAssignTarget());
            //Main.Instance.Modules.Add(new KillLoopTarget());
            //Main.Instance.Modules.Add(new MurderKillTarget());
            //Main.Instance.Modules.Add(new BlindTarget());
            //Main.Instance.Modules.Add(new BlindLoopTarget());
            //Main.Instance.Modules.Add(new BringItemsTarget());
            //Main.Instance.Modules.Add(new BringKnifeTarget());
            //Main.Instance.Modules.Add(new BringRevolverTarget());
            //Main.Instance.Modules.Add(new ForceSpawn());

            ////targetmenu>Avatar
            //Main.Instance.Modules.Add(new ReUploadTUT());
            //Main.Instance.Modules.Add(new ReUploadAvatar());
            //Main.Instance.Modules.Add(new CopyData());

            Assembly assm = Assembly.GetExecutingAssembly();

            foreach (Type t in assm.GetTypes())
            {
                if (!moduleArray.Contains(t.Name)) continue;

                object newInst = Activator.CreateInstance(t, new object[0]);
                object newObj = Convert.ChangeType(newInst, t);
                Main.Instance.Modules.Add((BaseModule)newObj);
                yield return null;
            }

            //Loads "Modules" and Creates UI/Buttions
            foreach (BaseModule module in Main.Instance.Modules)
            {
                module.OnUIInit();
                yield return null;
            }

            Transform rightKeyButton = UIU.keyboardPopup.transform.Find("ButtonRight");
            GameObject pasteButtonObj = GameObject.Instantiate(rightKeyButton.gameObject, rightKeyButton.parent);

            RectTransform pasteButtonTrans = pasteButtonObj.GetComponent<RectTransform>();
            pasteButtonTrans.anchoredPosition = new(450, 160);
            pasteButtonTrans.sizeDelta = new(165, 98.8f);

            pasteButtonObj.GetComponentInChildren<Text>().text = "<<< Paste";

            Button pasteButton = pasteButtonObj.GetComponent<Button>();

            Action pasteAction = new(() =>
            {
                UIU.keyboardPopup.GetComponentInChildren<InputField>().text = System.Windows.Forms.Clipboard.GetText();
            });

            pasteButton.onClick = new Button.ButtonClickedEvent();
            pasteButton.onClick.AddListener(pasteAction);


            //logout
            new QMSingleButton(mainTab.menuTransform, "Logout", "Serpent Logout Button", QMButtonIcons.LoadSpriteFromFile(Serpent.powerbuttonPath), delegate ()
            {
                try
                {
                    if (File.Exists(SecurityCheck.key) && SecurityCheck.CleanOnExit(File.ReadAllText(SecurityCheck.key)))
                    {
                        LogHandler.Log(LogHandler.Colors.Yellow, "[Trinity] Logged Out", false, false);
                    }
                    else
                    {
                        LogHandler.Log(LogHandler.Colors.Red, "[Trinity] Failed To Logged Out!", false, false);
                    }
                }
                catch (Exception EX) { }
            });
            try { Serpent.Carousel_Banners(false); } catch { }
            Serpent.QM_Text("Trinity");
            Console.Clear();
            LogHandler.DisplayLogo();
            //auth
            try
            {
                if (File.Exists(SecurityCheck.key) && SecurityCheck.GetServerInfo(File.ReadAllText(SecurityCheck.key)))
                {
                    //200
                    LogHandler.Log(LogHandler.Colors.Green, "[Trinity] Successful Relogin, Have Fun :)", false, false);
                }
                else { LogHandler.Log(LogHandler.Colors.Red, "[Trinity] Unsuccessful Relogin, please contact owner!", false, false); }
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }

        private static List<string> moduleArray = new()
        {
            "JoinByID",
"Rejoin",
"CopyWID",
"InstanceLock",
"FreezeItems",
"BlindPeople",
"VIPSpoofer",
"Room1",
"Room2",
"Room3",
"Room4",
"Room5",
"Room6",
"Room7",
"BypassVROnly",
"Teleport_Everyone",
"MurderAssign",
"DetectiveAssign",
"StartGame",
"AbortGame",
"BystanderWin",
"MurderWin",
"KillAll",
"KillLoop",
"BlindAll",
"FlashLoop",
"ForcePickup",
"w BringItems",
"BringKnife",
"BringRevolver",
"w BringSmoke",
" CloseDoors",
"LockDoors",
"w ShoveDoors",
"OpenDoors",
"GiveTagger",
"GiveRunner",
"Unfreeze",
"A_StartGame",
"A_AbortGame",
"A_MurderWin",
"A_BystandersWin",
"A_CallMeeting",
"A_ReportBody",
"A_CloseVote",
"A_SkipVote",
"A_KillAll",
"A_TaskDone",
"A_AssignImposter",
"A_AssignCrew",
"GetTriggerList",
"GetUdonEventList",
"CustomTriggerEvent",
"CustomUdonEvent",
"TriggerExploitFinder",
"UdonExploitFinder",
"TriggerNuke",
"UdonNuke",
"AvatarID",
"FPSUnlocker",
"LoudMic",
"HeadFlipper",
"CopyUserID",
"HideSelf",
"DrippedOut",
"XBoxMicd",
"_16k",
"_18k",
"_24k",
"_512k",
"Speed",
"Fly",
"InfinityJump",
"USpeakEarRape",
"Freecam",
"SendRPCDebug",
"ItemLag",
"FakeLag",
"FakeLagMic",
"FreezePlayers",
"AudioCrash",
"VoidBypass",
"ew EthosBypass",
"CorreuptPC",
"AssetBundleCrash",
"GameClose",
"QuestCrash",
"VRCA",
"VRCW",
"w ExploitLogs",
"PhotonProtection",
"AntiUdon",
"AntiRPC",
"AntiMesh",
"AntiShader",
"AntiCloth",
"AntiAudio",
"AntiBones",
"AntiAnimators",
"AntiColliders",
"AntiLight",
"AntiParticles",
"AntiMatirials",
"CuddleBot1",
"CuddleBot2",
"KIllBots",
"OPSendLogger",
"AssetBundleLogger",
"AvatarLogger",
"VCRALogger",
"UdonLogger",
"EventLogger",
"RpcLogger",
"UnityLogger",
"JoinLogger",
"ConsoleClear",
"ReLogin",
"Logout",
"HideVideoPlayers",
"HideChairs",
"HidePickUps",
"RestartAndRejoin(",
"QuickRestart(",
"PlayerList",
"DebugLog",
"CustomNameplates",
"TriggerESP",
"ObjectESP",
"CapsuleEsp",
"SerpentTheme",
"RetroTheme",
"munchen",
"CustomBGImage",
"ButtonsGreen",
"ButtonsBlue",
"ButtonsRed",
"ButtonsMagenta",
"AvatSelected",
"DownloadVRCSelected",
"ForceClone",
"Tel2User",
"UserIDSelected",
"TargetOrbitch",
"VoiceIM",
"TargetMCLag",
"TargetMCTeleport",
"TargetAntiPhoton",
"TargetAntiUdon",
"TargetAntiRPC",
"SendEvent",
"LockPlayerMovement",
"ForceRespawn",
"FreezePlayer",
"MakeTagger",
"MakeRunner",
"AssignImposter",
"AssignCrew",
"AForceSpawn",
"AmongUsKill",
"MurderAssignTarget",
"DetectiveAssignTarget",
"BystanderAssignTarget",
"KillLoopTarget",
"MurderKillTarget",
"BlindTarget",
"BlindLoopTarget",
"BringItemsTarget",
"BringKnifeTarget",
"BringRevolverTarget",
"ForceSpawn",
"ReUploadTUT",
"ReUploadAvatar",
"CopyData"
        };

    }
}
