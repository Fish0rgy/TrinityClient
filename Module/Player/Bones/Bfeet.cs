using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Bones
{
    internal class Bfeet : BaseModule
    {
        public Bfeet() : base("Feet", "Bones For Feet", Main.Instance.DynamicBonesButton, null, true, false) { }

        public override void OnEnable()
        {
            SDK.Config.Instance.GB_FeetColliders = true;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "Added Feet Dynamic Bones To Players", false, false);
        }
        public override void OnDisable()
        {
            SDK.Config.Instance.GB_FeetColliders = false;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "Removed Feet Dynamic Bones From Players", false, false);
        }
    }
}
