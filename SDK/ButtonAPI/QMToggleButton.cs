using Trinity.Utilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;

namespace Trinity.SDK.ButtonAPI
{
    public class QMToggleButton
    {
        public Toggle toggleButton;

        public TMP_Text textCom;

        public QMToggleButton(Transform parent, string text, string toolTip, Action<bool> action)
        {
            GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>( Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject, parent);
            singleButton.transform.parent = parent;
            singleButton.name = text + "_Trinity_ToggleButton";

            singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
            textCom = singleButton.transform.Find("Text_H4").GetComponent<TMP_Text>();
            textCom.color = new(0.7189f, 0.5634f, 0f, 1f);
            textCom.gameObject.GetComponent<StyleElement>().enabled = false;

            singleButton.transform.Find("Background").GetComponent<Image>().color = Color.black;
            singleButton.transform.Find("Icon_On").GetComponent<Image>().sprite = SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.ToggleOnPath);
            singleButton.transform.Find("Icon_Off").GetComponent<Image>().sprite = SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.ToggleOffPath);
            singleButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = toolTip;
            toggleButton = singleButton.GetComponent<Toggle>();
            toggleButton.onValueChanged = new Toggle.ToggleEvent();
            toggleButton.onValueChanged.AddListener(action);
            singleButton.SetActive(true);
        }
    }
}
