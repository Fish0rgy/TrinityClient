using Trinity.Utilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using System.Linq;
using Object = UnityEngine.Object;

namespace Trinity.SDK.ButtonAPI
{
    public class QMSocialButton
    {
        private GameObject socialButton;
        private Text buttonText;
        private Button buttonHandler;
        private Action onButtonClick;
        public QMSocialButton(string text, Action action, float x,float y)
        { 
            socialButton = Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").transform.Find("Buttons/RightSideButtons/RightUpperButtonColumn/PlaylistsButton").gameObject, GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").transform.Find("Buttons/RightSideButtons/RightUpperButtonColumn").gameObject.transform);
            Object.Destroy(socialButton.GetComponent<VRCUiButton>());
            buttonText = socialButton.GetComponentInChildren<Text>();
            buttonHandler = socialButton.GetComponent<Button>();
            socialButton.name = text;
            buttonText.text = text;
            socialButton.GetComponent<Button>().onClick.AddListener(action);
            buttonHandler.interactable = true;
            socialButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            RectTransform trans = socialButton.GetComponent<RectTransform>();
            trans.anchoredPosition = new(x, y);
            socialButton.gameObject.SetActive(true);
        }
    }
}
