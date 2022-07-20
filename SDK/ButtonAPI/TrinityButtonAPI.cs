using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace SoulMod.API
{
    internal class TrinityButtonAPI
    {
        public static VRC.UI.Elements.QuickMenu quickMenu = Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().First();
        public static MenuStateController menuStateController = quickMenu.gameObject.GetComponent<MenuStateController>();
        public static bool Complayer = false;
        //button api anyone can take from client and use
        public class QMNestedButton
        {
            public QMMenu menu;
            public Transform menuTransform;

            public QMSingleButton button;

            public QMNestedButton(Transform perant, string name, Sprite icon = null)
            {
                menu = new QMMenu(name, name, false, true);
                menuTransform = menu.menuContents;

                button = new QMSingleButton(perant, name, name, icon, delegate
                {
                    menu.OpenMenu();
                });
            }
        }
        class VrConsoleLog
        {
            public static List<TextMeshProUGUI> LogText { get; set; } = new List<TextMeshProUGUI>();
            public VrConsoleLog(Transform parent, Sprite background, float x, float y, float z)
            {
                GameObject Console = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Background"), parent);
                Console.name = "Trinity_ConsoleLog";
                Console.transform.parent = parent;
                Console.GetComponent<Image>().overrideSprite = background;
                Console.transform.localPosition = new Vector3(x, y, z); //-3.0204f, -72.5408f, 0.0908f
                Console.transform.localScale = new Vector3(4.88f, 1.98f, 1);
                Console.transform.TransformPoint(x, y, z);
                Console.AddComponent<RectMask2D>();
            }
        }
        public class QMSingleButton
        {

            public Button mainButton;

            private TMP_Text textCom;
            private Image badge;
            private Image icon;

            public QMSingleButton(Transform parent, string text, string toolTip, Sprite Icon, Action action)
            {
                GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);
                singleButton.transform.parent = parent;
                singleButton.name = text + "_Trinity_Button";
                singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
                textCom = singleButton.transform.Find("Text_H4").GetComponent<TMP_Text>();
                badge = singleButton.transform.Find("Badge_MMJump").GetComponent<Image>();
                icon = singleButton.transform.Find("Icon").GetComponent<Image>();
                singleButton.transform.Find("Background").GetComponent<Image>().color = Color.black;
                singleButton.transform.Find("Badge_MMJump").gameObject.active = true;
                if (Icon != null)
                    icon.sprite = Icon;
                else
                    UnityEngine.Object.Destroy(singleButton.transform.Find("Icon").GetComponent<Image>());
                singleButton.GetComponentsInChildren<VRC.UI.Elements.Tooltips.UiTooltip>(true).ToList().ForEach(s => s.field_Public_String_0 = toolTip);
                mainButton = singleButton.GetComponent<Button>();
                mainButton.onClick = new Button.ButtonClickedEvent();
                mainButton.onClick.AddListener(action);
                if (Complayer)
                {
                    badge.color = new(1f, 0.8234f, 0f, 1f);
                    icon.color = new(1f, 0.8234f, 0f, 1f);
                    textCom.color = new(0.7189f, 0.5634f, 0f, 1f);
                    badge.gameObject.GetComponent<StyleElement>().enabled = false;
                    icon.gameObject.GetComponent<StyleElement>().enabled = false;
                    textCom.gameObject.GetComponent<StyleElement>().enabled = false;
                }
                singleButton.SetActive(true);

            }
             
            //public static bool ModCheck(string mod)
            //{
            //    if (System.AppDomain.CurrentDomain.GetAssemblies().Any(x => x.GetName().Name == mod))
            //        return true;
            //    return false;
            //}
            //if(ModCheck("WorldClient")) concept on how this works btw
            //    TrinityButtonAPI.Complayer = true;
        }
        public class QMSpacer
        {
            public QMSpacer(Transform parent)
            {
                try
                {
                    GameObject Spacer = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);
                    Spacer.transform.parent = parent;
                    Spacer.name = "_Spacer";
                    Spacer.transform.Find("Text_H4").gameObject.active = false;
                    Spacer.transform.Find("Background").gameObject.active = false;
                    Spacer.transform.Find("Badge_MMJump").gameObject.active = false;
                    Spacer.transform.Find("Icon").gameObject.active = false;
                    Spacer.SetActive(true);
                }
                catch (Exception e) { 
                }

            }
            public static void Spacer(Transform parrent)
            {
                new QMSpacer(parrent);
                new QMSpacer(parrent);
                new QMSpacer(parrent);
                new QMSpacer(parrent);
                new QMSpacer(parrent);
                new QMSpacer(parrent);
                new QMSpacer(parrent);
                new QMSpacer(parrent);
            }
        }
        class QMTab
        {
            public QMMenu menu;
            public Transform menuTransform;
            public GameObject tab = UnityEngine.Object.Instantiate(quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools").gameObject, quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));

            public QMTab(string menuName, string pagetitle, string tooltip, Sprite Icon = null)
            {
                try
                {
                    menu = new QMMenu(menuName, pagetitle, true, false);
                    menuTransform = menu.menuContents;

                    GameObject tab =
                        UnityEngine.Object.Instantiate(quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_AudioSettings").gameObject,
                        quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));

                    tab.name = menuName + "Tab";
                    MenuTab menuTab = tab.GetComponent<MenuTab>();
                    menuTab.field_Private_MenuStateController_0 = menuStateController;
                    menuTab.field_Public_String_0 = menuName;

                    tab.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
                    if (Icon != null) { tab.transform.Find("Icon").GetComponent<Image>().sprite = Icon; } else { UnityEngine.Object.Destroy(tab.transform.Find("Icon").GetComponent<Image>()); }
                    tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
                    tab.GetComponent<Button>().onClick.AddListener(new Action(() => { tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>(); }));
                    tab.SetActive(true);
                }
                catch (Exception e) {
                }
            }
        }
        public class QMToggleButton
        {
            public Toggle toggleButton;
            public TMP_Text textCom;
            Action<bool> toggleAction;
            public Sprite offIcon = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo/Icon_Off").GetComponent<UnityEngine.UI.Image>().sprite;
            public Sprite onIcon = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<UnityEngine.UI.Image>().sprite;


            public QMToggleButton(Transform parent, string text, string toolTip, Action<bool> action)
            {
                GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject, parent);
                singleButton.transform.parent = parent;
                singleButton.name = text + "_Trinity_ToggleButton";

                singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
                textCom = singleButton.transform.Find("Text_H4").GetComponent<TMP_Text>();

                textCom.color = new(0.7189f, 0.5634f, 0f, 1f);
                textCom.gameObject.GetComponent<StyleElement>().enabled = false;

                singleButton.transform.Find("Background").GetComponent<Image>().color = Color.black;
                singleButton.transform.Find("Icon_On").GetComponent<Image>().sprite = onIcon;
                singleButton.transform.Find("Icon_Off").GetComponent<Image>().sprite = offIcon;
                singleButton.GetComponentInChildren<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = toolTip;
                toggleButton = singleButton.GetComponent<Toggle>();
                toggleAction = action;
                toggleButton.isOn = false;
                toggleButton.onValueChanged = new Toggle.ToggleEvent();
                toggleButton.onValueChanged.AddListener(toggleAction);
                singleButton.SetActive(true);

            }
            public void Toggle(bool state)
            {
                toggleButton.isOn = state;
            }
        }
        public class QMMenu
        {
            public UIPage page;
            public Transform menuContents;
            internal GameObject menuObj;

            public QMMenu(string menuName, string pageTitle, bool root = true, bool backButton = true)
            {
                try
                {
                    GameObject menu = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools").gameObject, quickMenu.transform.Find("Container/Window/QMParent"));
                    menu.name = "Menu_" + menuName;
                    menu.transform.SetSiblingIndex(5);
                    menu.SetActive(false);

                    UnityEngine.Object.Destroy(menu.GetComponent<DevMenu>());

                    page = menu.AddComponent<UIPage>();
                    page.field_Public_String_0 = menuName;
                    page.field_Private_Boolean_1 = true;
                    page.field_Protected_MenuStateController_0 = menuStateController;
                    page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
                    page.field_Private_List_1_UIPage_0.Add(page);
                    if (!root)
                    {
                        page.field_Public_Boolean_0 = true;
                        try
                        {
                            menu.transform.Find("Scrollrect/Scrollbar").gameObject.SetActive(true);
                            menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().enabled = true;
                            menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbar = menu.transform.Find("Scrollrect/Scrollbar").GetComponent<Scrollbar>();
                            menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
                        }
                        catch { }
                    }
                    menuStateController.field_Private_Dictionary_2_String_UIPage_0.Add(menuName, page);
                    if (root)
                    {
                        List<UIPage> list = menuStateController.field_Public_ArrayOf_UIPage_0.ToList<UIPage>();
                        list.Add(page);
                        menuStateController.field_Public_ArrayOf_UIPage_0 = list.ToArray();
                    }
                    TextMeshProUGUI pageTitleText = menu.GetComponentInChildren<TextMeshProUGUI>(true);
                    pageTitleText.text = pageTitle;
                    menuContents = menu.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
                    for (int i = 0; i < menuContents.transform.childCount; i++)
                        GameObject.Destroy(menuContents.transform.GetChild(i).gameObject);
                    if (backButton)
                    {
                        GameObject backButtonGameObject = menu.transform.Find("Header_DevTools/LeftItemContainer/Button_Back").gameObject;
                        backButtonGameObject.SetActive(true);
                        backButtonGameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                        backButtonGameObject.GetComponent<Button>().onClick.AddListener(new Action(() =>
                        {
                            page.Method_Protected_Virtual_New_Void_0();
                        }));
                    }

                }
                catch (Exception e) {  
                }
            }

            public void OpenMenu()
            {
                menuStateController.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(page.field_Public_String_0, null, false);
            }

            public void CloseMenu()
            {
                page.Method_Public_Virtual_New_Void_0();
            }
        }

        internal class QMLable
        {
            public TextMeshProUGUI text;
            public GameObject lable;
            public QMLable(Transform menu, float x, float y, string contents)
            {

                lable = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
                lable.name = "Lable_" + contents;

                lable.transform.localPosition = new Vector3(x, y, 0);
                text = lable.GetComponentInChildren<TextMeshProUGUI>();
                text.text = contents;
                text.enableAutoSizing = true;
                text.color = Color.white;
                text.m_fontColor = Color.white;
                lable.gameObject.SetActive(false);
            }
        }
        internal class QMHeader
        {
            public QMHeader(Transform menu, string contents)
            {
                GameObject header = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
                header.transform.parent = menu;
                header.name = "Header_" + contents;
                header.SetActive(true);
                header.GetComponentInChildren<TextMeshProUGUI>().text = contents;
            }
        }
        internal class QMCreateButton
        {
            public static GameObject Createbutton(string text, GameObject parent, Action action)
            {
                var toinst = GameObject.Find("UserInterface").transform.Find("MenuContent/Backdrop/Header/Tabs/ViewPort/Content/SafetyPageTab");
                var instanciated = GameObject.Instantiate(toinst, parent.transform);
                reset(instanciated.gameObject);
                instanciated.transform.rotation = new Quaternion(0, 0, 0, 0);
                instanciated.name = $"BTN_{text}";
                Component.Destroy(instanciated.GetComponent<VRCUiPageTab>());
                var button = instanciated.transform.Find("Button").GetComponent<UnityEngine.UI.Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(new Action(() => { action.Invoke(); })); // add listener
                instanciated.transform.Find("Button/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = text;
                return instanciated.gameObject;
            }
            public static void reset(GameObject gameobject)
            {
                gameobject.transform.localPosition = new Vector3(0, 0, 0);
                gameobject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                gameobject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        public class QMSocialButton
        {
            private GameObject socialButton;
            private Text buttonText;
            private Button buttonHandler;
            private Action onButtonClick;
            public QMSocialButton(string text, Action action)
            {
                socialButton = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").transform.Find("Buttons/RightSideButtons/RightUpperButtonColumn/PlaylistsButton").gameObject, GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").transform.Find("Buttons/RightSideButtons/RightUpperButtonColumn").gameObject.transform);
                UnityEngine.Object.Destroy(socialButton.GetComponent<VRCUiButton>());
                buttonText = socialButton.GetComponentInChildren<Text>();
                buttonHandler = socialButton.GetComponent<Button>();
                socialButton.name = text;
                buttonText.text = text;
                socialButton.GetComponent<Button>().onClick.AddListener(action);
                buttonHandler.interactable = true;
                socialButton.gameObject.SetActive(true);
            }
        }
        internal class QMButtonIcons
        {
            public static Sprite LoadSpriteFromFile(string path)
            {
                byte[] array = File.ReadAllBytes($"{Directory.GetCurrentDirectory()}/SoulMod/Icons/{path}");
                Texture2D texture2D = new Texture2D(256, 256);
                Il2CppImageConversionManager.LoadImage(texture2D, array);
                Rect rect = new Rect(0.0f, 0.0f, texture2D.width, texture2D.height);
                Vector2 vector = new Vector2(0.5f, 0.5f);
                Vector4 zero = Vector4.zero;
                return Sprite.CreateSprite_Injected(texture2D, ref rect, ref vector, 100f, 0, SpriteMeshType.Tight, ref zero, false);
            }
        }
        public class QMButtonGroup
        {
            public GameObject buttonGroup;
            public int buttonamount = 0;
            public QMButtonGroup(Transform parent, string name)
            {
                buttonGroup = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks").gameObject, parent);
                buttonGroup.name = "Buttons_" + name;
                buttonGroup.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
                for (int i = 0; i < buttonGroup.transform.childCount; i++)
                    GameObject.Destroy(buttonGroup.transform.GetChild(i).gameObject);
            }
        }
    }

}
