﻿using System;
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
            ButtonText.ToList().ForEach(Text =>
            {
                try
                {
                    Text.color = color;
                }
                catch { }

            }); 
		}
        public static void ChangeButtonColor(Color color)
        {
            ButtonColor.ToList().ForEach(image =>
            { 
                try
                {
                    if (image.name.Contains("HeaderBackground") || image.name.Contains("Background_QM_PagePanel")) return;
                    image.color = color;
                }
                catch { }

            });
        }
        public static void ChangeBackgroundColor(float r,float g, float b, float Opacity)
        {
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().color = new Color(r,g,b,Opacity);
        }  
		public static void WaitBitch()
		{
			new WaitForSecondsRealtime(0.8f);
		}
	}
}
