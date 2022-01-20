using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;

namespace Area51.SDK.ButtonAPI
{
    class QMTab
    {
        public QMMenu menu;
        public Transform menuTransform;
        public Image _MASK;

        //void test()
        //{

           
        //    GameObject carl = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK").gameObject, Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));
        //    MenuTab menuTab = carl.GetComponent<MenuTab>();
        //    menuTab.field_Private_MenuStateController_0 = Main.Instance.quickMenuStuff.menuStateController;
        //    menuTab.field_Public_String_0 = menuName;
        //}
        public QMTab(string menuName, string pagetitle, string tooltip, Sprite icon = null)
        {
            menu = new QMMenu(menuName, pagetitle, true, false);
            menuTransform = menu.menuContents;

            GameObject tab = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools").gameObject, Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));
            tab.name = menuName + "Tab";
            MenuTab menuTab = tab.GetComponent<MenuTab>();
            menuTab.field_Private_MenuStateController_0 = Main.Instance.QuickMenuStuff.menuStateController;
            menuTab.field_Public_String_0 = menuName;

            Image tabImage = tab.transform.Find("Icon").GetComponent<Image>();
            tab.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
            tabImage.sprite = icon;
            tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
            tab.GetComponent<Button>().onClick.AddListener(new Action(() =>
           {
               tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
           }));
            tab.SetActive(true);
        }
    }
}
