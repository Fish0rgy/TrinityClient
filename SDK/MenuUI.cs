using MelonLoader;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using TMPro;
using Trinity.Module;
using Trinity.Module.Bot; 
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
using Trinity.SDK.ButtonAPI.AVI_FAV;
using Trinity.SDK.ButtonAPI.PopUp;
using Trinity.SDK.Patching.Patches;
using Trinity.SDK.Security;
using Trinity.Utilities;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.UI;

namespace Trinity.SDK
{
    class MenuUI
    {
        public static List<ConsoleEntry> logs = new List<ConsoleEntry>();
        public static GameObject consoleObj;
        public static bool steampatch = false;

        public static IEnumerator StartUI()
        {
            var pos = new Vector3(272, 964, 0);
            Console.Title = $"Trinity | User: {APIUser.CurrentUser.displayName}";
            try { Serpent.Carousel_Banners(false); } catch { }
            Serpent.QM_Text("Trinity");
            Main.Instance.QuickMenuStuff = new Serpent();
            QMTab mainTab = new QMTab("TrinityClient", "", "Made By Fish.#0002", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Serpent.Spacer(mainTab.menuTransform);


            Main.Instance.WorldButton = new QMNestedButton(mainTab.menuTransform, "World", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            HackedGames();
            Main.Instance.PlayerButton = new QMNestedButton(mainTab.menuTransform, "Player", QMButtonIcons.LoadSpriteFromFile(Serpent.PlayerIconPath));
            PlayerMenu();
            Main.Instance.MovementButton = new QMNestedButton(mainTab.menuTransform, "Movement", QMButtonIcons.LoadSpriteFromFile(Serpent.MovmentPath));
            Main.Instance.ExploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarExploitPath));
            ExploitMenu();
            Main.Instance.SafetyButton = new QMNestedButton(mainTab.menuTransform, "Safety", QMButtonIcons.LoadSpriteFromFile(Serpent.SafetyIconPath));
            SafetyMenu();
            Main.Instance.BotButton = new QMNestedButton(mainTab.menuTransform, "Bot", QMButtonIcons.LoadSpriteFromFile(Serpent.TheBotsPath));
            BotMenu();
            Main.Instance.SettingsButton = new QMNestedButton(mainTab.menuTransform, "Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.SettingsIconPath));
            SettingsMenu();
            TargetMenu();
            new QMSingleButton(mainTab.menuTransform, "Clear Console", "Serpent Logout Button", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), delegate ()
            {
                Console.Clear();
                LogHandler.DisplayLogo();
                Log($"", true);
                Log($"<color=#cf9700>        Welcome to Trinity               </color>", true);
                Log($"", true);
                Log($"<color=#cf9700>           Made By Fish                </color>", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                Log($"", true);
                MenuUI.Log("CONSOLE: <color=green>Cleard Melonloader Console</color>");
            });
            new QMSocialButton("Req Inv +10", delegate ()
            {
                Misc.SpamInvites(PU.SocialInfo().field_Private_APIUser_0.id);
            });
             
            Transform buttonContainer = mainTab.menu.menuContents;
            GameObject menuObj = mainTab.menu.menuObj;

            Transform vLG = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_TrinityClient").transform.Find("Scrollrect/Viewport/VerticalLayoutGroup");
            //LogHandler.Log(LogHandler.Colors.Green,$"{vLG.name}",false,false);
            GameObject btnObj = GameObject.Instantiate(buttonContainer.GetComponentInChildren<Button>().gameObject, vLG);

            foreach (Image i in btnObj.GetComponentsInChildren<Image>())
            {
                GameObject.Destroy(i.gameObject);
            }
            UnityEngine.Object.Destroy(btnObj.GetComponent<Button>());

            btnObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


            consoleObj = GameObject.Instantiate(buttonContainer.GetComponentInChildren<Button>().gameObject, btnObj.transform);

            foreach (Image i in consoleObj.GetComponentsInChildren<Image>())
            {
                GameObject.Destroy(i.gameObject);
            }
            UnityEngine.Object.Destroy(consoleObj.GetComponent<Button>());
            UnityEngine.Object.Destroy(consoleObj.GetComponent<LayoutElement>());
            UnityEngine.Object.Destroy(consoleObj.GetComponent<CanvasGroup>());
            UnityEngine.Object.Destroy(consoleObj.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>());
            GameObject.Destroy(consoleObj.transform.Find("Text_H4").gameObject);

            Image img = consoleObj.AddComponent<Image>();
            img.sprite = QMButtonIcons.LoadSpriteFromFile("Trinity\\Icons\\ConsoleBack.png");

            RectTransform trans = consoleObj.GetComponent<RectTransform>();
            trans.anchoredPosition = new(512, 35);
            trans.sizeDelta = new(900, 450);

            VerticalLayoutGroup conVLG = consoleObj.AddComponent<VerticalLayoutGroup>();
            conVLG.childControlHeight = false;
            conVLG.childScaleHeight = false;
            conVLG.childForceExpandHeight = false;

            conVLG.childControlWidth = true;
            conVLG.childScaleWidth = false;
            conVLG.childForceExpandWidth = true;

            conVLG.spacing = 0;
            conVLG.padding = new(10, 0, 5, 0);

            for (int c = 0; c < consoleObj.transform.GetChildCount(); ++c)
            {
                GameObject.Destroy(consoleObj.transform.GetChild(c).gameObject);
            }

            for (int i = 0; i < 13; ++i)
            {
                ConsoleEntry newEntry = new("");
                newEntry.mainObj.transform.SetParent(consoleObj.transform, false);
                logs.Add(newEntry);
            }
            Log($"", true);
            Log($"<color=#cf9700>        Welcome to Trinity               </color>", true);
            Log($"", true);
            Log($"<color=#cf9700>           Made By Fish                </color>", true);
            Log($"", true);
            Log($"<color=#cf9700>[Spoofer] Steam Spoofed: {steampatch} </color>", true);
            Log($"<color=#cf9700>[Spoofer] PC Name: {SystemInfo.deviceName} </color>", true);
            Log($"<color=#cf9700>[Spoofer] Model: {SystemInfo.deviceModel}</color>", true);
            Log($"<color=#cf9700>[Spoofer] PBU: {SystemInfo.graphicsDeviceName}</color>", true);
            Log($"<color=#cf9700>[Spoofer] CPU: {SystemInfo.processorType}</color>", true);
            Log($"<color=#cf9700>[Spoofer] PBU ID: {SystemInfo.graphicsDeviceID.ToString()}</color>", true);
            Log($"<color=#cf9700>[Spoofer] OS:{SystemInfo.operatingSystem}</color>", true);
            Log($"<color=#cf9700>[Spoofer] HWID: {_Spoofers.newHWID}</color>", true);




            Assembly assm = Assembly.GetExecutingAssembly();

            foreach (Type t in assm.GetTypes())
            {
                if (!moduleArray.Contains(t.Name)) continue;

                object newInst = Activator.CreateInstance(t, new object[0]);
                object newObj = Convert.ChangeType(newInst, t);
                Main.Instance.Modules.Add((BaseModule)newObj);
                yield return null;
            }
            Main.Instance.Modules.ToList().ForEach(module =>
            {
                if (module.name == "Fly") Main.Instance.FlyModule = module;
                if (module.name == "Mesh ESP") Main.Instance.EspModule = module;
                if (module.name == "Speed") Main.Instance.SpeedModule = module;
                if (module.name == "LoudMic") Main.Instance.LoudMicModule = module;
                module.OnUIInit();
            });
            Transform rightKeyButton = UIU.keyboardPopup.transform.Find("ButtonRight");
            GameObject pasteButtonObj = GameObject.Instantiate(rightKeyButton.gameObject, rightKeyButton.parent);

            RectTransform pasteButtonTrans = pasteButtonObj.GetComponent<RectTransform>();
            pasteButtonTrans.anchoredPosition = new(450, 160);
            pasteButtonTrans.sizeDelta = new(165, 98.8f);

            pasteButtonObj.GetComponentInChildren<Text>().text = "Paste";

            Button pasteButton = pasteButtonObj.GetComponent<Button>();

            Action pasteAction = new(() =>
            {
                UIU.keyboardPopup.GetComponentInChildren<InputField>().text = System.Windows.Forms.Clipboard.GetText();
            });

            pasteButton.onClick = new Button.ButtonClickedEvent();
            pasteButton.onClick.AddListener(pasteAction);


            Console.Clear();
            LogHandler.DisplayLogo();
            //QMCustomNoti.SetUp();
            //MelonCoroutines.Start(Misc.nameplates());
            //LogHandler.Popup("Trinity", "Client Done Creating Buttons!");
            try
            {

                if (Misc.ModCheck("TeoLoader") == false)
                    avatarFav.UI(); 
            }
            catch (Exception e)
            {
                LogHandler.Error(e);
            }
            if(PU.GetPlayer().prop_APIUser_0.id == "usr_4a0fba6d-9e0d-4cbc-b43e-7d31ed645a03")
            {
                GameObject.Find("UserInterface/MenuContent/Screens/Worlds/Current Room/RefreshWorlds").gameObject.SetActive(false);
                //GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Add By ID").gameObject.SetActive(false);
                //GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Avatar").gameObject.SetActive(false); 
            }
             
            yield return new WaitForSecondsRealtime(0.1f);
        }
        private static List<string> moduleArray = new()
        {
            "PingSpoofer",
            "Copydata",
            "FPSSpoofer",
            "JoinByID", 
            "GlobalBones",
            "Bfriends",
            "Bhead",
            "Bchest",
            "Bhand",
            "Bhip",
            "Bfeet",
            "Breload",
            "MeshESP",
            "PickupSteal",
            "Rejoin",
            "TKill",
            "TGiveMoney", 
            "ResetClues",
            "CopyWID",
            "InstanceLock",
            "FreezeItems",
            "BlindPeople",
            "VIPSpoofer",
            "STDMoney",
            "STDHealth",
            "A_TeleportAll",
            "AmongUsTeleport",
            "MurderGoBoomBoom",
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
            "NoReload",
            "KillAll",
            "KillLoop", 
            "NoCollidersForDoors", 
            "NoSteam",
            "BlindAll",
            "FlashLoop",
            "ForcePickup",    
            "CloseDoors",
            "LockDoors",
            "NoReload",
            "ShoveDoors",
            "OpenDoors",
            "SetFPSpoof",
            "SetPingSpoof",
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
            "AddClientList",
            "RemoveClientList",
            "ReflectDamage",
            "ShoveDoors",
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
            "ModerationLogger",
            "AntiEvent1",
            "Fly",
            "InfinityJump",
            "AntiLockInstance",
            "NaziItems",
            "Freecam",
            "SendRPCDebug",
            "ItemLag",
            "FakeLag",
            "FakeLagMic",
            "FreezePlayers",  
            "CorreuptPC",
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
            //"ItemOrbit",
            "AntiMatirials", 
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
            //"ReLogin",
            //"Logout",
            "HideVideoPlayers",
            "HideChairs",
            "HidePickUps",
            "RestartAndRejoin",
            "QuickRestart",
            "PlayerList",
            "DebugLog",
            "CustomNameplates",
            "TriggerESP",
            "ObjectESP", 
            //ghost
            "AvatSelected",
            "GiveCurrency", 
            "KillHumans",
            "GodMode",
            "UnlockRoom",
            "GhostWin",
            "HumansWin",
            "ClaimClues",
            "StartMatch",
            "CraftAllGuns",
            "munchen",
            "CustomBGImage",
            "ButtonColor",
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
            "DeboBootyHole",
            "DetectiveAssignTarget",
            "BystanderAssignTarget",
            "KillLoopTarget",
            "MurderKillTarget",
            "BlindTarget",
            //"SpectateMode",
            "BlindLoopTarget",
            "CoolKeyBinds",
            "BringItemsTarget",
            "BringKnifeTarget",
            "BringRevolverTarget",
            "ForceSpawn", 
            "ReUploadAvatar", 
        };
        public static void Log(string txt, bool plainText = false)
        {
            int logCount = logs.Count;

            ConsoleEntry oldEntry = logs[0];
            logs.Remove(oldEntry);
            GameObject.Destroy(oldEntry.mainObj);

            if (!plainText) txt = $"[<color=#00ff00ff>{DateTime.Now.ToString("h:mm tt")}</color>] " + txt;

            ConsoleEntry newEntry = new(txt);
            newEntry.mainObj.transform.SetParent(consoleObj.transform, false);
            logs.Add(newEntry);
        }

        public static void SafetyMenu(){
            Main.Instance.Networkbutton = new QMNestedButton(Main.Instance.SafetyButton.menuTransform, "Network Saftey", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.Avatarbutton = new QMNestedButton(Main.Instance.SafetyButton.menuTransform, "Avatar Saftey", QMButtonIcons.LoadSpriteFromFile(Serpent.SafetyIconPath));
        }
        public static void ExploitMenu(){
            Main.Instance.Eventexploitbutton = new QMNestedButton(Main.Instance.ExploistButton.menuTransform, "Event Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.Avatarexploitbutton = new QMNestedButton(Main.Instance.ExploistButton.menuTransform, "Avatar Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarPath));
        }
        public static void PlayerMenu(){
            Main.Instance.DynamicBonesButton = new QMNestedButton(Main.Instance.PlayerButton.menuTransform, "Dynamic Bones", QMButtonIcons.LoadSpriteFromFile(Serpent.BonesPath));
            Main.Instance.MiscButton = new QMNestedButton(Main.Instance.PlayerButton.menuTransform, "Misc", QMButtonIcons.LoadSpriteFromFile(Serpent.CustomPath));
            Main.Instance.AudioButton = new QMNestedButton(Main.Instance.PlayerButton.menuTransform, "Audio Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.AudioPath));
        }
        public static void HackedGames(){
            Main.Instance.WorldhacksButton = new QMNestedButton(Main.Instance.WorldButton.menuTransform, "World Hacks", QMButtonIcons.LoadSpriteFromFile(Serpent.WorldHacksIconPath));
            Main.Instance.udonexploitbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Trinity Engine", QMButtonIcons.LoadSpriteFromFile(Serpent.udonManagerPath));
            Main.Instance.Zombiebutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Zombie Tag", QMButtonIcons.LoadSpriteFromFile(Serpent.zombiePath));
            Main.Instance.Amongusbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Among Us", QMButtonIcons.LoadSpriteFromFile(Serpent.amogusPath));
            Main.Instance.Murderbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Murder 4", QMButtonIcons.LoadSpriteFromFile(Serpent.murder4Path));
            Main.Instance.Justbbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Just B", QMButtonIcons.LoadSpriteFromFile(Serpent.justbPath));
            Main.Instance.JustHButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Just H", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.Magictagbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Magic Tag", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.MovieAndChillButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Movie & Chill", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.GhostButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Ghost", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.STDButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "STD", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
        }
        public static void TargetMenu(){
            Main.Instance.Targetbutton = new QMNestedButton(Main.Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "Trinity", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Main.Instance.AvatarSettings = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "Avatar", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarPath));
            Main.Instance.SafetyTargetButton = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "Safety Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.WorldhacksTargetButton = new QMNestedButton(Main.Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "World Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            Main.Instance.MurderSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Murder 4 Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.murder4Path));
            Main.Instance.AmongUsSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Among Us Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.MagicTagSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Magic Tag Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.JubstBSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Just B Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.justbPath));
            Main.Instance.GhostTargetButton = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Ghost Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.MoveAndChillSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Movie&Chill+ Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.movieandchill));
        }
        public static void BotMenu(){
            Main.Instance.Privatebotbutton = new QMNestedButton(Main.Instance.BotButton.menuTransform, "App Bots", QMButtonIcons.LoadSpriteFromFile(Serpent.PlayerIconPath));
        }
        public static void SettingsMenu(){ 
            Main.Instance.SettingsButtonpreformance = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Performance", QMButtonIcons.LoadSpriteFromFile(Serpent.preformancePath));
            Main.Instance.SettingsButtonrender = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Render", QMButtonIcons.LoadSpriteFromFile(Serpent.renderPath));
            Main.Instance.SettingsButtonLoggging = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Logging", QMButtonIcons.LoadSpriteFromFile(Serpent.loggingPath));
            Main.Instance.SettingsButtonTheme = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Theme", QMButtonIcons.LoadSpriteFromFile(Serpent.ThemePath));
            Main.Instance.SettingsButtonspoofer = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Spoofer", QMButtonIcons.LoadSpriteFromFile(Serpent.SafetyIconPath));
        }
    }
}
