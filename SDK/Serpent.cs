using Trinity.Utilities;
using Trinity.SDK.ButtonAPI;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace Trinity.SDK
{
    public class Serpent
    {
        //Button IMAGES
        public const string NameplatePath = "Trinity\\Icons\\Namplate.png";
        public const string BGPath = "Trinity\\Icons\\Background.png";
        public const string clientLogoPath = "Trinity\\Icons\\Logo.png";
        public const string AvatarPath = "Trinity\\Icons\\Avatar.png";
        public const string AudioPath = "Trinity\\Icons\\Audio.png";
        public const string bot1 = "Trinity\\Icons\\Start1.png";
        public const string bot2 = "Trinity\\Icons\\Start2.png";
        public const string QMBGPath = "Trinity\\Icons\\Background.png";
        public const string SettingsIconPath = "Trinity\\Icons\\Settings.png";
        public const string SafetyIconPath = "Trinity\\Icons\\Safety.png";
        public const string PlayerIconPath = "Trinity\\Icons\\Player.png";
        public const string LocalHandlerIconPath = "Trinity\\Icons\\Exploit Manager.png";
        public const string APIHandlerIconPath = "Trinity\\Icons\\";
        public const string WorldHacksIconPath = "Trinity\\Icons\\World.png"; 
        public const string TabAlienPath = "Trinity\\Icons\\Logo.png";
        public const string SpaceShipPath = "Trinity\\Icons\\Logo.png";
        public const string raygunPath = "Trinity\\Icons\\TargetCrash.png";
        public const string satltePath = "Trinity\\Icons\\Network.png";
        public const string TheBotsPath = "Trinity\\Icons\\Bot.png";
        public const string drippedoutPath = "Trinity\\Icons\\Logo.png";
        public const string renderPath = "Trinity\\Icons\\Render.png";
        public const string rocketPath = "Trinity\\Icons\\Rocket.png";
        public const string preformancePath = "Trinity\\Icons\\Preformance.png";
        public const string ToggleOnPath = "Trinity\\Icons\\On.png";
        public const string ToggleOffPath = "Trinity\\Icons\\Off.png";
        public const string loggingPath = "Trinity\\Icons\\Logs.png";
        public const string earthPath = "Trinity\\Icons\\World.png";
        public const string SavePath = "Trinity\\Icons\\Reuploader.png";
        public const string refreshPath = "Trinity\\Icons\\Refreash.png";
        public const string powerbuttonPath = "Trinity\\Icons\\Power.png";
        public const string skipPath = "Trinity\\Icons\\Skip.png";
        public const string stopbots = "Trinity\\Icons\\Stop.png";
        public const string copyPath = "Trinity\\Icons\\Copy.png";
        public const string CustomPath = "Trinity\\Icons\\Custom.png";
        public const string ClonePath = "Trinity\\Icons\\Clone.png";
        public const string MovmentPath = "Trinity\\Icons\\Force.png";
        public const string AvatarExploitPath = "Trinity\\Icons\\Exploits.png";
        public const string RetroPath = "Trinity\\Icons\\customimg.png";
        public const string MimicPath = "Trinity\\Icons\\Mimic.png";
        public const string ThemePath = "Trinity\\Icons\\Theme.png";
        public const string TeleportPath = "Trinity\\Icons\\Teleport.png";
        public const string ReJoinPath = "Trinity\\Icons\\ReJoin.png";
        public const string BonesPath = "Trinity\\Icons\\Bones.png";

        public const string zombiePath = "Trinity\\Icons\\Zombie.png";
        public const string udonManagerPath = "Trinity\\Icons\\Exploit Manager.png";
        public const string amogusPath = "Trinity\\Icons\\Games.png";
        public const string murder4Path = "Trinity\\Icons\\Murder.png";
        public const string Games = "Trinity\\Icons\\Games.png";
        public const string justbPath = "Trinity\\Icons\\Club Hax.png";
        public const string movieandchill = "Trinity\\Icons\\Movie.png";

        public const string FinderPath = "Trinity\\Icons\\Finder.png";
        public const string EventTablePath = "Trinity\\Icons\\Table.png";


        public Image Button_WorldsIcon;
        public Image Button_AvatarsIcon;
        public Image Button_SocialIcon;
        public Image Button_SafetyIcon;
        public Image Panel_NoNotifications_MessageIcon;
        public Image Button_NameplateVisibleIcon;
        public Image Button_GoHomeIcon;
        public Image Button_RespawnIcon;
        public Image StandIcon;
 
        public Transform tabMenuTemplat;
        public Transform Menu_DevTools;
        public Transform Menu_Dashboard;
        public Transform QMParent;
        public Transform page_Buttons_QM;
        public VRC.UI.Elements.QuickMenu quickMenu;
        public static VRC.UI.Elements.QuickMenu Qmenu;
        public SelectedUserMenuQM selectedUserMenuQM;
        public MenuStateController menuStateController;

        public static bool UIUpdated = false;

        public static object Instance { get; internal set; }

        //vrchat + removal
        public static bool Carousel_Banners(bool state) => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK").gameObject.active = state;
        public static bool VRCP_PageTab(bool state) => UIU.UserInterface.transform.Find("MenuContent/Backdrop/Header/Tabs/ViewPort/Content/VRC+PageTab").gameObject.active = state;
        public static bool QM_Badge_Icon(bool state) => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand/Icon").gameObject.active = state;
        //Header
        public static string QM_Text(string Text_title) => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().text = Text_title;
        public static float QM_Text_Size(float _Size) => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().fontSize = _Size;
        public static bool QM_Text_Wrapping(bool state) => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().enableWordWrapping = state;
 
        public Serpent()
        {
            quickMenu = Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().First();
            menuStateController = quickMenu.gameObject.GetComponent<MenuStateController>();

            Button_WorldsIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Icon").GetComponent<Image>();
            Button_AvatarsIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Icon").GetComponent<Image>();
            Button_SocialIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Icon").GetComponent<Image>();
            Button_SafetyIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Icon").GetComponent<Image>();

            Button_GoHomeIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Icon").GetComponent<Image>();
            Button_RespawnIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Icon").GetComponent<Image>();
            StandIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Icon_Off").GetComponent<Image>();
            Panel_NoNotifications_MessageIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").gameObject.GetComponent<Image>();

            Button_NameplateVisibleIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_NameplateControls/Buttons/Button A/Icon").GetComponent<Image>();

            selectedUserMenuQM = quickMenu.transform.Find("Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
            tabMenuTemplat = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools");
            Menu_DevTools = quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools");
            QMParent = quickMenu.transform.Find("Container/Window/QMParent");
            page_Buttons_QM = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");
            Menu_Dashboard = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings");
        }

     
        public static void Spacer(Transform parrent)
        {
            new QMTEST(parrent);
            new QMTEST(parrent);
            new QMTEST(parrent);
            new QMTEST(parrent);
            new QMTEST(parrent);
            new QMTEST(parrent);
            new QMTEST(parrent);
            new QMTEST(parrent);
        }

        public static void ThemeUI()
        {
            try
            {
                if (UIUpdated == false)
                {                 
                    Carousel_Banners(false);        
                    QM_Text("");
                    QM_Text_Size(44f);
                    QM_Text_Wrapping(false);
                    UIUpdated = true;
                    LogHandler.Log(LogHandler.Colors.Green, "[THEME] Set", false, false);
                }
            }
            catch{ }

        }

    }
}
