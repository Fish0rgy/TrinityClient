using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks
{
    class CustomTriggerEvent : BaseModule
    {
        public CustomTriggerEvent() : base("Send\n Trigger Event", "Sends Custom udon event from clipboard", Main.Instance.udonexploitbutton, QMButtonIcons.CreateSpriteFromBase64(Extra_Icons.Finder), false, false) { }

        public override void OnEnable()
        {
            string payload = Misc.GetClipboard();
            UdonExploitManager.trigersend(payload);
        }
    }
}
