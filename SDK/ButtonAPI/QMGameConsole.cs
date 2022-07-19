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
using Trinity.SDK.Security;
using Trinity.Utilities;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core; 

namespace Trinity.SDK.ButtonAPI
{
    class QMGameConsole
    {
        public static List<ConsoleEntry> logs = new List<ConsoleEntry>();
        public static GameObject consoleObj;

		public static void Init()
        {
            try { CreateConsole();
            }
            catch (Exception ex) { LogHandler.Log(LogHandler.Colors.Red, $"[Failed To Create Console] \n{ex.ToString}", false, false); }
        }
        public static void CreateConsole()
		{
            //Transform buttonContainer = mainTab.menu.menuContents;
            //GameObject menuObj = mainTab.menu.menuObj; 

            //Transform vLG = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_TrinityClient").transform.Find("Scrollrect/Viewport/VerticalLayoutGroup");
            ////LogHandler.Log(LogHandler.Colors.Green,$"{vLG.name}",false,false);
            //GameObject btnObj = GameObject.Instantiate(buttonContainer.GetComponentInChildren<Button>().gameObject, vLG);

            //foreach (Image i in btnObj.GetComponentsInChildren<Image>())
            //{
            //    GameObject.Destroy(i.gameObject);
            //}
            //UnityEngine.Object.Destroy(btnObj.GetComponent<Button>());

            //btnObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


            //consoleObj = GameObject.Instantiate(buttonContainer.GetComponentInChildren<Button>().gameObject, btnObj.transform);

            //foreach (Image i in consoleObj.GetComponentsInChildren<Image>())
            //{
            //    GameObject.Destroy(i.gameObject);
            //}
            //UnityEngine.Object.Destroy(consoleObj.GetComponent<Button>());
            //UnityEngine.Object.Destroy(consoleObj.GetComponent<LayoutElement>());
            //UnityEngine.Object.Destroy(consoleObj.GetComponent<CanvasGroup>());
            //UnityEngine.Object.Destroy(consoleObj.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>());
            //GameObject.Destroy(consoleObj.transform.Find("Text_H4").gameObject);

            //Image img = consoleObj.AddComponent<Image>();
            //img.sprite = QMButtonIcons.LoadSpriteFromFile("Trinity\\Icons\\ConsoleBack.png");

            //RectTransform trans = consoleObj.GetComponent<RectTransform>();
            //trans.anchoredPosition = new(512, 35);
            //trans.sizeDelta = new(900, 450);

            //VerticalLayoutGroup conVLG = consoleObj.AddComponent<VerticalLayoutGroup>();
            //conVLG.childControlHeight = false;
            //conVLG.childScaleHeight = false;
            //conVLG.childForceExpandHeight = false;

            //conVLG.childControlWidth = true;
            //conVLG.childScaleWidth = false;
            //conVLG.childForceExpandWidth = true;

            //conVLG.spacing = 0;
            //conVLG.padding = new(10, 0, 5, 0);

            //for (int c = 0; c < consoleObj.transform.GetChildCount(); ++c)
            //{
            //    GameObject.Destroy(consoleObj.transform.GetChild(c).gameObject);
            //}

            //for (int i = 0; i < 13; ++i)
            //{
            //    ConsoleEntry newEntry = new("");
            //    newEntry.mainObj.transform.SetParent(consoleObj.transform, false);
            //    logs.Add(newEntry);
            //}
            //Log($"", true);
            //Log($"", true);
            //Log($"<color=#cf9700>        Welcome to Trinity               </color>", true);
            //Log($"", true);
            //Log($"<color=#cf9700>           Made By Fish                </color>", true);
            //Log($"", true);
            //Log($"", true);
            //Log($"", true);
            //Log($"", true);
            //Log($"", true);
            //Log($"", true);
            //Log($"", true);
            //Log($"", true);
        }
	}
}


























































//Transform buttonContainer = MenuUI.MenuTabJustBetter.menu.menuContents;
//GameObject gay = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_TrinityClient/Scrollrect");

//Transform vLG = gay.transform.Find("Viewport/VerticalLayoutGroupScrollrect/Viewport/VerticalLayoutGroup");

//GameObject btnObj = GameObject.Instantiate(buttonContainer.GetComponentInChildren<Button>().gameObject, vLG);

//foreach (Image i in btnObj.GetComponentsInChildren<Image>())
//{
//    GameObject.Destroy(i.gameObject);
//}
//UnityEngine.Object.Destroy(btnObj.GetComponent<Button>()); 
//btnObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


//consoleObj = GameObject.Instantiate(buttonContainer.GetComponentInChildren<Button>().gameObject, btnObj.transform); 

//foreach (Image i in consoleObj.GetComponentsInChildren<Image>())
//{
//    GameObject.Destroy(i.gameObject);
//}
//UnityEngine.Object.Destroy(consoleObj.GetComponent<Button>());
//UnityEngine.Object.Destroy(consoleObj.GetComponent<LayoutElement>());
//UnityEngine.Object.Destroy(consoleObj.GetComponent<CanvasGroup>());
//UnityEngine.Object.Destroy(consoleObj.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>());
//GameObject.Destroy(consoleObj.transform.Find("Text_H4").gameObject);
//Image img = consoleObj.AddComponent<Image>();
//img.sprite = QMButtonIcons.LoadSpriteFromFile("Trinity\\Icons\\ConsoleBack.png");
//RectTransform trans = consoleObj.GetComponent<RectTransform>();
//trans.anchoredPosition = new(512, 35);
//trans.sizeDelta = new(900, 450);

//VerticalLayoutGroup conVLG = consoleObj.AddComponent<VerticalLayoutGroup>();
//conVLG.childControlHeight = false;
//conVLG.childScaleHeight = false;
//conVLG.childForceExpandHeight = false;

//conVLG.childControlWidth = true;
//conVLG.childScaleWidth = false;
//conVLG.childForceExpandWidth = true;

//conVLG.spacing = 0;
//conVLG.padding = new(10, 0, 5, 0);

//for (int c = 0; c < consoleObj.transform.GetChildCount(); ++c)
//{
//    GameObject.Destroy(consoleObj.transform.GetChild(c).gameObject);
//}

//for (int i = 0; i < 13; ++i)
//{
//    ConsoleEntry newEntry = new("");
//    newEntry.mainObj.transform.SetParent(consoleObj.transform, false);
//    logs.Add(newEntry);
//}
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"<color=#cf9700>        Welcome to Trinity               </color>", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"<color=#cf9700>           Made By Fish                </color>", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);
//MenuUI.Log($"", true);