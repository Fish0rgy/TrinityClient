using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Audio
{
    class _512k : BaseModule
    {
        public _512k() : base("512k BitRate", "512k BitRate", Main.Instance.AudioButton, null, true, false) { }
        public override void OnEnable()
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_512k;
        }

        public override void OnDisable()
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
        }
    }
}
