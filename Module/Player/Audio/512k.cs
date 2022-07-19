using Trinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.Player.Audio
{
    class _512k : BaseModule
    {
        public _512k() : base("512k BitRate", "512k BitRate", Main.Instance.AudioButton, null, true, true) { }
        public override void OnEnable()
        {
            MenuUI.Log("AUDIO: <color=green>Set Bit Rate To 512k</color>");
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_512k;
        }

        public override void OnDisable()
        {
            MenuUI.Log("AUDIO: <color=red>Reset Audio Bit Rate</color>");
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }
    }
}
