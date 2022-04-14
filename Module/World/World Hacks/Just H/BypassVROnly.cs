using Trinity.Utilities;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Just_H
{
    class BypassVROnly : BaseModule
    {
        public BypassVROnly() : base("Bypass VROnly", "Enables desktop users to use vr only floors", Main.Instance.JustHButton, null, false)
        {
        }
        public override void OnEnable()
        {
            JustHMisc.EnableVROnlyBtn(true);
            LogHandler.Log(LogHandler.Colors.Green, "Stopped VR Check, Your Now Allowed To Go Where Ever You Want!", false, false);
        }
    }
}
