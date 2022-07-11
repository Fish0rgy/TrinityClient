using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Safety.Photon
{
    internal class AntiEvent1 : BaseModule 
    {
        public AntiEvent1() : base("Anti LouddMic", "Destroy Bad Event Data From Event 1", Main.Instance.Networkbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Trinity.SDK.Config.AntiE1 = true;
        }

        public override void OnDisable()
        {
            Trinity.SDK.Config.AntiE1 = false;
        }
    }
}
