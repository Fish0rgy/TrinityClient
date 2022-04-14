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
            Main.Instance.udonexploitbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Trinity Engine", QMButtonIcons.LoadSpriteFromFile(Serpent.udonManagerPath));
            Main.Instance.Zombiebutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Zombie Tag", QMButtonIcons.LoadSpriteFromFile(Serpent.zombiePath));
            Main.Instance.Amongusbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Among Us", QMButtonIcons.LoadSpriteFromFile(Serpent.amogusPath));
            Main.Instance.Murderbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Murder 4", QMButtonIcons.LoadSpriteFromFile(Serpent.murder4Path));
            Main.Instance.Justbbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Just B", QMButtonIcons.LoadSpriteFromFile(Serpent.justbPath));
            Main.Instance.JustHButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Just H", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.Magictagbutton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Magic Tag", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.MovieAndChillButton = new QMNestedButton(Main.Instance.WorldhacksButton.menuTransform, "Movie & Chill", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.PlayerButton = new QMNestedButton(mainTab.menuTransform, "Player", QMButtonIcons.LoadSpriteFromFile(Serpent.PlayerIconPath));
            Main.Instance.AudioButton = new QMNestedButton(Main.Instance.PlayerButton.menuTransform, "Audio Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.AudioPath));
            Main.Instance.MovementButton = new QMNestedButton(mainTab.menuTransform, "Movement", QMButtonIcons.LoadSpriteFromFile(Serpent.MovmentPath));
            Main.Instance.ExploistButton = new QMNestedButton(mainTab.menuTransform, "Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarExploitPath));
            Main.Instance.Eventexploitbutton = new QMNestedButton(Main.Instance.ExploistButton.menuTransform, "Event Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.Avatarexploitbutton = new QMNestedButton(Main.Instance.ExploistButton.menuTransform, "Avatar Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarPath));
            Main.Instance.SafetyButton = new QMNestedButton(mainTab.menuTransform, "Safety", QMButtonIcons.LoadSpriteFromFile(Serpent.SafetyIconPath));
            Main.Instance.Networkbutton = new QMNestedButton(Main.Instance.SafetyButton.menuTransform, "Network Saftey", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.Avatarbutton = new QMNestedButton(Main.Instance.SafetyButton.menuTransform, "Avatar Saftey", QMButtonIcons.LoadSpriteFromFile(Serpent.SafetyIconPath));
            Main.Instance.BotButton = new QMNestedButton(mainTab.menuTransform, "Bot", QMButtonIcons.LoadSpriteFromFile(Serpent.TheBotsPath));
            Main.Instance.Privatebotbutton = new QMNestedButton(Main.Instance.BotButton.menuTransform, "Local handler", QMButtonIcons.LoadSpriteFromFile(Serpent.PlayerIconPath));
            Main.Instance.SettingsButton = new QMNestedButton(mainTab.menuTransform, "Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.SettingsIconPath));
            Main.Instance.SettingsButtonpreformance = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Preformance", QMButtonIcons.LoadSpriteFromFile(Serpent.preformancePath));
            Main.Instance.SettingsButtonrender = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Render", QMButtonIcons.LoadSpriteFromFile(Serpent.renderPath));
            Main.Instance.SettingsButtonLoggging = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Logging", QMButtonIcons.LoadSpriteFromFile(Serpent.loggingPath));
            Main.Instance.SettingsButtonTheme = new QMNestedButton(Main.Instance.SettingsButton.menuTransform, "Theme", QMButtonIcons.LoadSpriteFromFile(Serpent.ThemePath));
            //targetmenu
            Main.Instance.Targetbutton = new QMNestedButton(Main.Instance.QuickMenuStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/").transform, "Trinity", QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath));
            Main.Instance.AvatarSettings = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "Avatar", QMButtonIcons.LoadSpriteFromFile(Serpent.AvatarPath));
            Main.Instance.SafetyTargetButton = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "Safety Settings", QMButtonIcons.LoadSpriteFromFile(Serpent.satltePath));
            Main.Instance.WorldhacksTargetButton = new QMNestedButton(Main.Instance.Targetbutton.menuTransform, "World Exploits", QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath));
            Main.Instance.MurderSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Murder 4 Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.murder4Path));
            Main.Instance.AmongUsSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Among Us Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.MagicTagSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Magic Tag Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.Games));
            Main.Instance.JubstBSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Just B Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.justbPath));
            Main.Instance.MoveAndChillSettings = new QMNestedButton(Main.Instance.WorldhacksTargetButton.menuTransform, "Movie&Chill+ Menu", QMButtonIcons.LoadSpriteFromFile(Serpent.movieandchill)); 

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
