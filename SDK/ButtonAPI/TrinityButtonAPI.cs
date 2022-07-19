using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace Trinity.SDK.ButtonAPI
{
    internal class TrinityButtonAPI
    {
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
                GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);
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
                if (!Main.CompLayer)
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
        }
        public class QMSpacer
        {
            public QMSpacer(Transform parent)
            {
                try
                {
                    GameObject Spacer = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);
                    Spacer.transform.parent = parent;
                    Spacer.name = "_Spacer";
                    Spacer.transform.Find("Text_H4").gameObject.active = false;
                    Spacer.transform.Find("Background").gameObject.active = false;
                    Spacer.transform.Find("Badge_MMJump").gameObject.active = false;
                    Spacer.transform.Find("Icon").gameObject.active = false;
                    Spacer.SetActive(true);
                }
                catch (Exception e) { LogHandler.Error(e); }

            }
        }
        class QMTab
        {
            public QMMenu menu;
            public Transform menuTransform;
            public GameObject tab = UnityEngine.Object.Instantiate(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools").gameObject, Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));

            public QMTab(string menuName, string pagetitle, string tooltip, Sprite Icon = null)
            {
                try
                {
                    menu = new QMMenu(menuName, pagetitle, true, false);
                    menuTransform = menu.menuContents;

                    GameObject tab =
                        UnityEngine.Object.Instantiate(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_AudioSettings").gameObject,
                        Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));

                    tab.name = menuName + "Tab";
                    MenuTab menuTab = tab.GetComponent<MenuTab>();
                    menuTab.field_Private_MenuStateController_0 = Main.Instance.QuickMenuStuff.menuStateController;
                    menuTab.field_Public_String_0 = menuName;

                    tab.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
                    if (Icon != null) { tab.transform.Find("Icon").GetComponent<Image>().sprite = Icon; } else { UnityEngine.Object.Destroy(tab.transform.Find("Icon").GetComponent<Image>()); }
                    tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
                    tab.GetComponent<Button>().onClick.AddListener(new Action(() => { tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>(); }));
                    tab.SetActive(true);
                }
                catch (Exception Ex) { LogHandler.Log(LogHandler.Colors.Red, $"[QMTab.cs] \n {Ex.Message}", false, false); }
            }
        }
        public class QMToggleButton
        {
            public Toggle toggleButton;
            public TMP_Text textCom;
            Action<bool> toggleAction;

            public QMToggleButton(Transform parent, string text, string toolTip, Action<bool> action)
            {
                GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject, parent);
                singleButton.transform.parent = parent;
                singleButton.name = text + "_Trinity_ToggleButton";

                singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
                textCom = singleButton.transform.Find("Text_H4").GetComponent<TMP_Text>();

                textCom.color = new(0.7189f, 0.5634f, 0f, 1f);
                textCom.gameObject.GetComponent<StyleElement>().enabled = false;

                singleButton.transform.Find("Background").GetComponent<Image>().color = Color.black;
                singleButton.transform.Find("Icon_On").GetComponent<Image>().sprite = SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.ToggleOnPath);
                singleButton.transform.Find("Icon_Off").GetComponent<Image>().sprite = SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.ToggleOffPath);
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
                    GameObject menu = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools").gameObject, Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent"));
                    menu.name = "Menu_" + menuName;
                    menu.transform.SetSiblingIndex(5);
                    menu.SetActive(false);

                    UnityEngine.Object.Destroy(menu.GetComponent<DevMenu>());

                    page = menu.AddComponent<UIPage>();
                    page.field_Public_String_0 = menuName;
                    page.field_Private_Boolean_1 = true;
                    page.field_Protected_MenuStateController_0 = Main.Instance.QuickMenuStuff.menuStateController;
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
                    Main.Instance.QuickMenuStuff.menuStateController.field_Private_Dictionary_2_String_UIPage_0.Add(menuName, page);
                    if (root)
                    {
                        List<UIPage> list = Main.Instance.QuickMenuStuff.menuStateController.field_Public_ArrayOf_UIPage_0.ToList<UIPage>();
                        list.Add(page);
                        Main.Instance.QuickMenuStuff.menuStateController.field_Public_ArrayOf_UIPage_0 = list.ToArray();
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
                catch (Exception Ex)
                {
                    LogHandler.Log(LogHandler.Colors.Green, Ex.Message, true, false);
                }
            }

            public void OpenMenu()
            {
                Main.Instance.QuickMenuStuff.menuStateController.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(page.field_Public_String_0, null, false);
            }

            public void CloseMenu()
            {
                page.Method_Public_Virtual_New_Void_0();
            }
        }
        internal class QMLoadingScreen
        {

            public static float HRed = 0;
            public static float HGreen = 0.438850343f;
            public static float HBlue = 0.712937f;
            public static GameObject partsystem;

            public static IEnumerator LoadingScreen()
            {
                try
                {
                    //var ovcolor = new Color( HRed, HGreen, HBlue);
                    var ovcolor = Color.magenta;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/Rectangle").GetComponent<Image>().color = Color.clear;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/MidRing").GetComponent<Image>().color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/InnerDashRing").GetComponent<Image>().color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ButtonMiddle").GetComponent<Button>().image.color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/RingGlow").GetComponent<Image>().color = ovcolor;
                    GameObject.Find("/UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked").SetActive(false);
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ArrowRight").GetComponent<Image>().color = Color.clear;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ArrowLeft").GetComponent<Image>().color = Color.clear;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/TitleText").GetComponent<Text>().color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ProgressLine").GetComponent<Image>().color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/TitleText").GetComponent<UnityEngine.UI.Outline>().enabled = false;
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel").SetActive(false);
                    GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").SetActive(false);
                    GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles").SetActive(false);
                    //GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<Button>().image.color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = Color.clear;
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = Color.clear;
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = Color.clear;
                    GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = Color.green; //filling
                    GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = Color.red; //Not Filled
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Button>().image.color = ovcolor;
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = Color.clear;
                    MelonCoroutines.Start(loadingscreen());
                    if (GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle") == null)
                    {
                        LogHandler.Log(LogHandler.Colors.Red, "failed to customize home button", false, false);
                    }
                    else
                    {
                        GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Button>().image.color = ovcolor;
                    }
                }
                catch
                {
                    LogHandler.Log(LogHandler.Colors.Red, "The custom loading screen failed to load", false, false);
                }

                yield return null;
            }

            private static bool isvideo = true;
            private static RenderTexture rendert;

            public static IEnumerator loadingscreen()
            {
                var isbackround = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Image>();
                var isbackround1 = GameObject.Instantiate(isbackround, isbackround.transform.parent);
                isbackround1.gameObject.name = "Video";
                var delettext = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/Video/Text");

                GameObject.Destroy(delettext);
                isbackround1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100); //0 100 425
                isbackround1.GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 425);
                isbackround1.GetComponent<RectTransform>().sizeDelta /= new Vector2(0.9f, 0.3f);
                isbackround1.GetComponent<UnityEngine.UI.Button>().enabled = false;
                isbackround1.GetComponent<RectTransform>().sizeDelta /= new Vector2(0.9f, 0.9f);

                var isbackround2 = GameObject.Instantiate(isbackround1, isbackround1.transform);
                isbackround2.name = "Backround";
                isbackround2.transform.localPosition = new Vector3(0, 0, 0);
                isbackround2.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                var bg2img = isbackround2.GetComponent<Image>();
                bg2img.color = Color.cyan;
                MelonCoroutines.Start(loadspriterest(bg2img, "http://nocturnal-client.xyz/cl/Download/Media/just%20border.png"));




                var objb = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/Video");
                Component.Destroy(objb.GetComponent<Button>());
                Component.Destroy(objb.transform.Find("Backround").gameObject.GetComponent<Button>());

                objb.GetComponent<Image>().sprite = null;
                objb.AddComponent<UnityEngine.Video.VideoPlayer>();
                var vidcomp = objb.GetComponent<UnityEngine.Video.VideoPlayer>();
                vidcomp.isLooping = true;
                rendert = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
                rendert.Create();
                Material mat = new Material(Shader.Find("Standard"));
                mat.color = default;
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.white);

                mat.SetTexture("_EmissionMap", rendert);
                objb.GetComponent<Image>().material = mat;
                vidcomp.targetTexture = rendert;
                vidcomp.url = $"{MelonUtils.GameDirectory}\\Trinity\\Misc\\LoadingVid.mp4";
                while (GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>() == null)
                    yield return null;

                //var getauds = GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>();
                //getauds.clip = null;

                vidcomp.audioOutputMode = VideoAudioOutputMode.AudioSource;
                vidcomp.EnableAudioTrack(0, false);
                isvideo = false;
                yield return null;
            }
            public static IEnumerator loadparticles()
            {
                byte[] loadingparticles = File.ReadAllBytes($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\\\loadingscreen");
                var myLoadedAssetBundle = AssetBundle.LoadFromMemory(loadingparticles);
                if (myLoadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    yield break;
                }
                partsystem = myLoadedAssetBundle.LoadAsset<GameObject>("ParticleLoader");
                var gmj = GameObject.Instantiate(partsystem, GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup").transform);
                gmj.transform.localPosition = new Vector3(0, 0, 8000);
                gmj.transform.Find("finished").gameObject.transform.localPosition = new Vector3(0, 0, 10000);
                gmj.transform.Find("finished/Other").gameObject.transform.localPosition = new Vector3(0, 0, 3000);
                gmj.transform.Find("middle").gameObject.transform.localPosition = new Vector3(-50, 0f, 10000);
                gmj.transform.Find("cirlce mid").gameObject.transform.localPosition = new Vector3(-673.8608f, 0, 4000);
                gmj.transform.Find("spawn").gameObject.transform.localPosition = new Vector3(800, 0, -8500f);

                foreach (var gmjs in gmj.GetComponentsInChildren<ParticleSystem>(true))
                {
                    gmjs.startColor = new Color(HRed, HGreen, HBlue);
                    gmjs.trails.colorOverTrail = new Color(HRed, HGreen, HBlue);
                }
                GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/3DElements").gameObject.SetActive(false);

                while (GameObject.Find("/UserInterface").transform.Find("DesktopUImanager") == null)
                    yield return null;


                var toload = myLoadedAssetBundle.LoadAsset<GameObject>("Holder");

                myLoadedAssetBundle.Unload(false);
                var gmjsa = GameObject.Instantiate(toload, GameObject.Find("/UserInterface").transform.Find("DesktopUImanager").transform);
                gmjsa.transform.localPosition = new Vector3(0, 360.621f, 700);
                gmjsa.transform.localRotation = new Quaternion(0, 0, 0, 0);
                gmjsa.transform.localScale = new Vector3(1, 1, 1);
                var p1 = gmjsa.transform.Find("Particle System").transform;
                p1.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                p1.localPosition = new Vector3(0, 64.16f, 7.2f);
                var p2 = gmjsa.transform.Find("Particle System (1)").transform;
                p2.localScale = new Vector3(0.06f, 0.06f, 0.06f);
                p2.localPosition = new Vector3(-30.78f, -321.5403f, 8.54f);
                yield return null;
            }
            public static IEnumerator loadspriterest(Image Instance, string url)
            {

                var www = UnityWebRequestTexture.GetTexture(url);
                _ = www.downloadHandler;
                var asyncOperation = www.SendWebRequest();
                Func<bool> func = () => asyncOperation.isDone;
                yield return new WaitUntil(func);
                if (www.isHttpError || www.isNetworkError)
                {
                    LogHandler.Log(LogHandler.Colors.Red, www.error, false, false);
                    yield break;
                }

                var content = DownloadHandlerTexture.GetContent(www);
                var sprite2 = Instance.sprite = Sprite.CreateSprite(content,
                    new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                    SpriteMeshType.FullRect, Vector4.zero, false);

                if (sprite2 != null) Instance.sprite = sprite2;
            }
        }
        internal class QMLable
        {
            public TextMeshProUGUI text;
            public GameObject lable;
            public QMLable(Transform menu, float x, float y, string contents)
            {

                lable = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
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
                GameObject header = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
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
        internal class QMButtonIcons
        {
            public static Sprite LoadSpriteFromFile(string path)
            {
                byte[] data = File.ReadAllBytes(path);
                Texture2D tex = new(256, 256);
                ImageConversion.LoadImage(tex, data);

                Rect rect = new Rect(0.0f, 0.0f, tex.width, tex.height);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                Vector4 border = Vector4.zero;

                Sprite sprite = Sprite.CreateSprite_Injected(tex, ref rect, ref pivot, 100, 0, SpriteMeshType.Tight, ref border, false);
                return sprite;
            }
        }
        public class QMButtonGroup
        {
            public GameObject buttonGroup;
            public int buttonamount = 0;
            public QMButtonGroup(Transform parent, string name)
            {
                buttonGroup = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks").gameObject, parent);
                buttonGroup.name = "Buttons_" + name;
                buttonGroup.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
                for (int i = 0; i < buttonGroup.transform.childCount; i++)
                    GameObject.Destroy(buttonGroup.transform.GetChild(i).gameObject);
            }
        }
    }
     
}
