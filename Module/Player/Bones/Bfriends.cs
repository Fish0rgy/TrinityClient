using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Bones
{
    internal class Bfriends : BaseModule
    {
        public Bfriends() : base("Friends", "Bones For heads", Main.Instance.DynamicBonesButton, null, true, false) { }

        public override void OnEnable()
        {
            SDK.Config.Instance.GB_Friends = true;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "Added Dynamic Bones To Friends", false, false);
        }
        public override void OnDisable()
        {
            SDK.Config.Instance.GB_Friends = false;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "Removed Dynamic Bones From Friends", false, false);
        }
    }
}
