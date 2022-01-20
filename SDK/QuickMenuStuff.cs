using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace Area51.SDK
{
    public class QuickMenuStuff
    {
        public Image Button_WorldsIcon;
        public Image Button_AvatarsIcon;
        public Image Button_SocialIcon;
        public Image Button_SafetyIcon;
        public Image Panel_NoNotifications_MessageIcon;

        public Image Button_NameplateVisibleIcon;

        public Image Button_GoHomeIcon;
        public Image Button_RespawnIcon;
        public Image StandIcon;

        public VRC.UI.Elements.QuickMenu quickMenu;
        public SelectedUserMenuQM selectedUserMenuQM;
        public MenuStateController menuStateController;

        public Transform tabMenuTemplat;
        public Transform Menu_DevTools;
        public Transform Menu_Dashboard;
        public Transform QMParent;
        public Transform page_Buttons_QM;

        //vrchat + removal
        public static bool Carousel_Banners(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK").active = state;
        public static bool VRCP_PageTab(bool state) => GameObject.Find("UserInterface/MenuContent/Backdrop/Header/Tabs/ViewPort/Content/VRC+PageTab").active = state;
       
        //backwindow
        public static bool Back_window(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Back Window/").active = state;
        public static bool Back_window_background(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Back Window/Background").active = state;
        public static bool Back_window_Logo(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Back Window/Logo").active = state;
        //color BW
        public static Color Back_window_color(UnityEngine.Color color) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Back Window/Background").GetComponent<Image>().color = color;
        public static Color Back_window_material_color(UnityEngine.Color color) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Back Window/Background").GetComponent<Image>().material.color = color;
        //QM Layers
        public static UnityEngine.Color QMLayer1_BackColor(UnityEngine.Color color) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().color = color;
        public static bool QMLayer1(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").active = state;
        public static bool QMLayer2(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer02").active = state;
        //Badge Removal
        public static bool QM_Badge_Icon(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand/Icon").active = state;


        //Header
        public static string QM_Text(string Text_title) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().text = Text_title;
        public static float QM_Text_Size(float _Size) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().fontSize = _Size;
        public static bool QM_Text_Wrapping(bool state) => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().enableWordWrapping = state;
        //Color notworking
        public static Color QM_H1Text_Color() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TMPro.TextMeshProUGUI>().m_fontColor = Color.black;
        //QuickLinks
        public static Color QM_QLText_Color() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks/LeftItemContainer/Text_Title").GetComponent<TMPro.TextMeshProUGUI>().color = Color.black;
        //QuickActions
        public static Color QM_QAText_Color() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions/LeftItemContainer/Text_Title").GetComponent<TMPro.TextMeshProUGUI>().m_Color = Color.black;
   


       
        
        public QuickMenuStuff()
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



        public static bool UIUpdated = false;
        public static void ThemeUI()
        {  
            try
            {
                if(UIUpdated == false)
                {
                    //ill fix this as i i find everything
                    Color white = new Color(255f, 255f, 255f, 255f);
                    Color whiteRGBA = new Color(1, 1, 1, 1);
                    Color RGBA_Red = new Color(0.5676f, 0f, 0f, 1f);
                    Color red = new Color(1, 0, 1, 1);
                    //VRC+ Functions
                    SDK.QuickMenuStuff.Carousel_Banners(false);
                    SDK.QuickMenuStuff.VRCP_PageTab(false);
                    //QM
                    //Back_window_Logo(false);
                    //QMLayer1(false);
                    //QMLayer2(false);

                    //Back_window(true);
                    //Back_window_background(true);
                    //color

                    //QMLayer1_BackColor(red);
                    //Back_window_color(RGBA_Red);
                    //Back_window_material_color(Color.red);

                    //QM Label
                    QM_Text("Area 51 Client - Quick Menu");
                    QM_Text_Size(44f);
                    QM_Text_Wrapping(false);

                    // QM_H1Text_Color();
                    //  QM_QLText_Color();
                    //  QM_QAText_Color();

                    //QM Badge
                    QM_Badge_Icon(false);
                    //menu background
                  
                    UIUpdated = true;
                    Logg.Log(Logg.Colors.Green, "[THEME] Set, Thank You(TooOverRated)!", false, false);
                }
            }
            catch
            {

            }

        }

    }
}
