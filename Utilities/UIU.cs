using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnhollowerRuntimeLib;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;

namespace Trinity.Utilities
{
    public static class UIU
	{
		public static GameObject QuickMenu = UserInterface.transform.Find("Canvas_QuickMenu(Clone)").gameObject;
		public static TextMeshProUGUI[] ButtonText = (TextMeshProUGUI[])UnityEngine.Object.FindObjectsOfType<TextMeshProUGUI>(); 
		public static Image[] ButtonColor = (Image[])UnityEngine.Object.FindObjectsOfType<Image>();
		public static GameObject UserInterface => GameObject.Find("UserInterface");
        public static VRCUiPopupInput keyboardPopup => VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.field_Public_VRCUiPopupInput_0;

        public static void CloseVRCUI() => VRCUiManager.prop_VRCUiManager_0.Method_Public_Void_Boolean_Boolean_1(true, false);
        public static Il2CppSystem.Action CloseVRCUIAction => DelegateSupport.ConvertDelegate<Il2CppSystem.Action>(new Action(() => { UIU.CloseVRCUI(); }));

        public static void OpenVRCUIPopup(string title, string body, string acceptText, Action acceptAction, string declineText, Action declineAction = null)
        {
            Il2CppSystem.Action newAcceptAction = DelegateSupport.ConvertDelegate<Il2CppSystem.Action>(acceptAction);
            if (declineAction == null)
            {
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_1(title, body, acceptText, newAcceptAction, declineText, CloseVRCUIAction);
            }
            else
            {
                Il2CppSystem.Action newDeclineAction = DelegateSupport.ConvertDelegate<Il2CppSystem.Action>(declineAction);
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_1(title, body, acceptText, newAcceptAction, declineText, newDeclineAction);
            }
        }

        public static void OpenVRCUINotifPopup(string title, string body, string okayText, Action okayAction = null)
        {
            if (okayAction == null)
            {
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_1(title, body, okayText, CloseVRCUIAction);
            }
            else
            {
                Il2CppSystem.Action newOkayAction = DelegateSupport.ConvertDelegate<Il2CppSystem.Action>(okayAction);
                VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_1(title, body, okayText, newOkayAction);
            }

        }

        public static void OpenKeyboardPopup(string title, string placeholderText, System.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> action)
        {
            Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> newAction = DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>(action);
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0(title, null, InputField.InputType.Standard, false, "Okay", newAction, CloseVRCUIAction, placeholderText);
        }
        public static void OpenKeyboardPopup2(string title, string placeholderText, System.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> action)
        {
            Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> newAction = DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>(action);
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_0(title, null, InputField.InputType.Standard, false, "Okay", newAction, CloseVRCUIAction, placeholderText);
        }
		public static void ChangeTextColor(Color color)
		{
			for (int i = 0; i < ButtonText.Length; i++)
			{
				TextMeshProUGUI text = ButtonText[i];
				text.color = color;
			}
		}
		public static void ChangeButtonColor(Color color)
        {
			for (int i = 0; i < ButtonColor.Length; i++)
			{
				Image text = ButtonColor[i];
				text.color = color;
			}
		}
		public static void ChangeBackgroundColor(float r,float g, float b, float Opacity)
        {
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().color = new Color(r,g,b,Opacity);
        }
		public static void SetColor(Color Primary, Color secondary)
		{
			GameObject QuickMenu = UserInterface.transform.Find("Canvas_QuickMenu(Clone)").gameObject;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Dashboard/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Notifications/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Here/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Camera/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_AudioSettings/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Area 51 ClientTab/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Dashboard/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Notifications/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Here/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Camera/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_AudioSettings/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Area 51 ClientTab/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/MicButton").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/MicButton/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Toggle_SafeMode").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Toggle_SafeMode/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").gameObject.active = false;
			if (QuickMenu.transform.Find("Container/Window/QMParent/BackgroundLayer02").GetComponent<Image>().color != Color.white)
			{
				QuickMenu.transform.Find("Container/Window/QMParent/BackgroundLayer02").GetComponent<Image>().color = Color.white;
				QuickMenu.transform.Find("Container/Window/QMParent/BackgroundLayer02").gameObject.active = false;
				QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Background_QM_PagePanel").GetComponent<Image>().color = Color.white;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().color = Color.white;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks/LeftItemContainer/Text_Title").gameObject.active = false;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions/LeftItemContainer/Text_Title").gameObject.active = false;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/ Button_InteractionPauseWithState/Background").GetComponent<Image>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Text_FPS").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Text_Ping").GetComponent<TextMeshProUGUI>().color = secondary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/Wing_Left/Button/Icon").GetComponent<Image>().color = secondary;
				QuickMenu.transform.Find("Container/Window/Wing_Right/Button/Icon").GetComponent<Image>().color = secondary;
			}
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Icon_On").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Icon_Off").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_CalibrateFBT/Icon").GetComponent<Image>().color = secondary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_CalibrateFBT/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_CalibrateFBT/Text_H4").GetComponent<TextMeshProUGUI>().color = secondary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_MünchenClientMünchenClient2/Icon").GetComponent<Image>().color = Color.white;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_MünchenClientMünchenClient2/Background").GetComponent<Image>().color = Primary;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Notorious_QMTabButton_7/Icon").GetComponent<Image>().color = Color.white;
			QuickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Notorious_QMTabButton_7/Background").GetComponent<Image>().color = Primary;
			if (QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().color != Color.black)
			{
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().color = Primary;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks/LeftItemContainer/Text_Title").gameObject.active = false;
				QuickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions/LeftItemContainer/Text_Title").gameObject.active = false;
			}
		}






		public static void WaitBitch()
		{
			new WaitForSecondsRealtime(0.8f);
		}
	}
}
