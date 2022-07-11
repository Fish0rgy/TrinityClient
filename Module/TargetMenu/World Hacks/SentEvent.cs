using Trinity.Utilities;
using System;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;

using Trinity.SDK;
using Trinity.Utilities;
using Trinity.SDK.ButtonAPI;

namespace Trinity.Module.Player
{
    class SendEvent : BaseModule
    {
        public SendEvent() : base("Send Event", "Sends Custom udon event from clipboard", Main.Instance.WorldhacksTargetButton, QMButtonIcons.LoadSpriteFromFile(Serpent.udonManagerPath), false, false) { }

        public override void OnEnable()
        {

            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) => 
            {
                for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
                {
                    UW.udonsend(str, EventTarget.Targeted);
                    MenuUI.Log($"UDON: <color=green>Sent Custom Event {str}</color>");
                    LogHandler.Log(LogHandler.Colors.Green, $"[Custom Udon Event] Event Name: {str} | Object Name: {WorldWrapper.udonBehaviours[j].gameObject.name}", false, false);
                }
            });

            UIU.OpenKeyboardPopup("Send Udon Event", "Enter Event Name...", keyboardAction);
            
        }
    }
}
