using Trinity.Utilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using System.Linq;

namespace Trinity.SDK.ButtonAPI
{
    public class QMSingleButton
    {

        public Button mainButton;

        private TMP_Text textCom;
        private Image badge;
        private Image icon;

        public QMSingleButton(Transform parent, string text, string toolTip, Sprite Icon, Action action)
        {
            GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>( Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);     
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
}
