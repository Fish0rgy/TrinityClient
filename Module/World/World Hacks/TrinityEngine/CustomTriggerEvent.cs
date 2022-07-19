using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Trinity.Utilities;

namespace Trinity.Module.World.World_Hacks
{ 
    class CustomTriggerEvent : BaseModule
    {
        public CustomTriggerEvent() : base("Send\n Trigger Event", "Sends Custom udon event from clipboard", Main.Instance.udonexploitbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.FinderPath), false, false) { }

        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {
                if (string.IsNullOrWhiteSpace(str)) return;

                for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
                {
                    UW.trigersend(str);
                    LogHandler.Log(LogHandler.Colors.Green, $"[Custom Udon Event] Event Name: {str} | Object Name: {WorldWrapper.udonBehaviours[j].gameObject.name}", false, false);
                }
            });

            UIU.OpenKeyboardPopup("Send Trigger Event", "Enter Object Name...", keyboardAction);
        }
    }
}
