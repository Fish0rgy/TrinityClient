using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;

namespace Trinity.Module.Player.Bones
{
    internal class Bhip : BaseModule
    {
        public Bhip() : base("Hips", "Bones For hips", Main.Instance.DynamicBonesButton, null, true, false) { }

        public override void OnEnable()
        {
            Config.Instance.GB_HipBones = true;
            Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "Added Hip Dynamic Bones To Players", false,false);
        }
        public override void OnDisable()
        {
            Config.Instance.GB_HipBones = false;
            Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "Removed Hip Dynamic Bones From Players", false, false);
        }
    }
}
