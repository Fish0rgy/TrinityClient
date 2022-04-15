using Trinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace Trinity.SDK.ButtonAPI
{
    public class QMMenu
    {
        public UIPage page;
        public Transform menuContents;

        public GameObject menuObj;

        public QMMenu(string menuName, string pageTitle, bool root = true, bool backButton = true)
        {
            VRC.UI.Elements.QuickMenu qm = Main.Instance.QuickMenuStuff.quickMenu;
            GameObject origObj = qm.transform.Find("Container/Window/QMParent/Menu_DevTools").gameObject;

            menuObj = GameObject.Instantiate(origObj, origObj.transform.parent);
            menuObj.name = "Menu_" + menuName;
            menuObj.transform.SetSiblingIndex(5);
            menuObj.SetActive(false);

            UnityEngine.Object.Destroy(menuObj.GetComponent<DevMenu>());

            page = menuObj.AddComponent<UIPage>();
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
                    menuObj.transform.Find("Scrollrect/Scrollbar").gameObject.SetActive(true);
                    menuObj.transform.Find("Scrollrect").GetComponent<ScrollRect>().enabled = true;
                    menuObj.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbar = menuObj.transform.Find("Scrollrect/Scrollbar").GetComponent<Scrollbar>();
                    menuObj.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
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
            TextMeshProUGUI pageTitleText = menuObj.GetComponentInChildren<TextMeshProUGUI>(true);
            pageTitleText.text = pageTitle;
            menuContents = menuObj.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
            for (int i = 0; i < menuContents.transform.childCount; i++)
                GameObject.Destroy(menuContents.transform.GetChild(i).gameObject);
            if (backButton)
            {
                GameObject backButtonGameObject = menuObj.transform.Find("Header_DevTools/LeftItemContainer/Button_Back").gameObject;
                backButtonGameObject.SetActive(true);
                backButtonGameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                backButtonGameObject.GetComponent<Button>().onClick.AddListener(new Action(() =>
                {
                    page.Method_Protected_Virtual_New_Void_0();
                }));
            }
        }

        public void OpenMenu()
        {
            Main.Instance.QuickMenuStuff.menuStateController.Method_Public_Void_String_UIContext_Boolean_0(this.page.field_Public_String_0, null, false);
        }

        public void CloseMenu()
        {
            this.page.Method_Public_Virtual_New_Void_0();
        }
    }
}
