using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.Player
{
    class SendEvent : BaseModule
    {
        public SendEvent() : base("Send Event", "Sends Custom udon event from clipboard", Main.Instance.WorldhacksTargetButton, QMButtonIcons.CreateSpriteFromBase64(Extra_Icons.udonManager), false, false) { }

        public override void OnEnable()
        {
            string payload = Misc.GetClipboard();
            for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
            {
                UdonExploitManager.udonsend(payload, "target");
                LogHandler.Log(LogHandler.Colors.Green, $"[Custom Udon Event] Event Name: {payload} | Object Name: {WorldWrapper.udonBehaviours[j].gameObject.name}", false, false);
            }
        }
    }
}
