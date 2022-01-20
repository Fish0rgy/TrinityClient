using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Area51.SDK.ButtonAPI
{
    public class QMSingleButtonTM
    {
        public QMSingleButtonTM(Transform parent, string text, string toolTip, Sprite Icon, Action action)
        {
            GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.selectedUserMenuQM.transform.Find("/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").gameObject, parent);
            singleButton.transform.parent = parent;
            singleButton.name = text + "_51_Button";

            singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
            if (Icon != null)
            {
                singleButton.transform.Find("Icon").GetComponent<Image>().sprite = Icon;
            }
            else
            {
                UnityEngine.Object.Destroy(singleButton.transform.Find("Icon").GetComponent<Image>());
            }
            singleButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = toolTip;
            Button button = singleButton.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(action);
            singleButton.SetActive(true);
        }
    }
}
