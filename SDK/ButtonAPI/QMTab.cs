using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;

namespace Trinity.SDK.ButtonAPI
{
    class QMTab
    {
        public QMMenu menu;
        public Transform menuTransform;

        public QMTab(string menuName, string pagetitle, string tooltip, Sprite Icon = null)
        {
            menu = new QMMenu(menuName, pagetitle, true, false);
            menuTransform = menu.menuContents;
            GameObject tab = UnityEngine.Object.Instantiate( Main.Instance.QuickMenuStuff.quickMenu.transform.Find(              
                "Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_AudioSettings").gameObject, 
                Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"               
                ));
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
    }
}
