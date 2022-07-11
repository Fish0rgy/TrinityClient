using Trinity.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;

namespace Trinity.SDK.ButtonAPI
{
    class QMOuterButton
    {
        public QMMenu menu;
        public Transform menuTransform; 

        public QMOuterButton(string menuName, string pagetitle, string tooltip, Sprite Icon = null)
        { 
            try
            {
                GameObject tab = UnityEngine.Object.Instantiate(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand").gameObject, Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer"));
                menu = new QMMenu(menuName, pagetitle, true, false);
                menuTransform = menu.menuContents;
                tab.name = menuName + "Tab";
                //MenuTab menuTab = tab.GetComponent<MenuTab>();
                //menuTab.field_Private_MenuStateController_0 = Main.Instance.QuickMenuStuff.menuStateController;
                //menuTab.field_Public_String_0 = menuName;

                tab.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
                tab.transform.Find("Icon").GetComponent<Image>().sprite = Icon;
                tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
                tab.GetComponent<Button>().onClick.AddListener(new Action(() => { tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>(); }));
                tab.SetActive(true);
                tab.transform.localPosition = new Vector3(-95.0667f, 0f, 0f);
            }
            catch (Exception Ex) { LogHandler.Log(LogHandler.Colors.Red, $"[QMOuterButton.cs] \n {Ex.Message}", false, false); }
        }
    }
}
