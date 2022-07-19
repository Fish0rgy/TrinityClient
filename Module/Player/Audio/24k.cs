using Trinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.Player.Audio
{
    class _24k : BaseModule
    {
        public _24k() : base("Default mic", "24k BitRate", Main.Instance.AudioButton, null, true, true) { }

        public override void OnEnable()
        {
            MenuUI.Log("AUDIO: <color=green>Set Bit Rate To 24k</color>");
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }

        public override void OnDisable()
        {
            MenuUI.Log("AUDIO: <color=red>Reset Audio Bit Rate</color>");
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }
    }
}
