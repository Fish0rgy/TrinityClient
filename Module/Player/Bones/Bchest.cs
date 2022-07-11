using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Bones
{
    internal class Bchest : BaseModule
    {
        public Bchest() : base("Chest", "Bones For Chest", Main.Instance.DynamicBonesButton, null, true, false) { }

        public override void OnEnable()
        {
            SDK.Config.Instance.GB_ChestBones = true;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "Added Chest Dynamic Bones To Players", false, false);
        }
        public override void OnDisable()
        {
            SDK.Config.Instance.GB_ChestBones = false;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "Removed Chest Dynamic Bones From Players", false, false);
        }
    }
}
