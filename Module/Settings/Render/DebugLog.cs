using Trinity.Utilities;
using Trinity.SDK.ButtonAPI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.Settings.Render
{
    class DebugLog : BaseModule
    {
        public static QMLable debugLog;
        public DebugLog() : base("LogList", "Debug on the side", Main.Instance.SettingsButtonrender, null, true, true)
        {
        }

        public override void OnEnable()
        {
            debugLog.lable.SetActive(true);
            debugLog.lable.transform.localPosition = new Vector3(609.902f, 457.9203f, 0);
            debugLog.text.enableWordWrapping = false;
            debugLog.text.fontSizeMin = 30;
            debugLog.text.fontSizeMax = 30;
            debugLog.text.alignment = TMPro.TextAlignmentOptions.Left;
            debugLog.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            debugLog.text.color = Color.white;  

        }

        public override void OnDisable()
        {
            debugLog.lable.SetActive(false);
            
        }

        public override void OnUIInit()
        {
            debugLog = new QMLable(UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Button"), 609.902f, 457.9203f, "Trinity | Console - Debug Log");
            base.OnUIInit();
        }
    }
}
