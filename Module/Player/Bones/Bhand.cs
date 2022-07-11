using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Bones
{
    internal class Bhand : BaseModule
    {
        public Bhand() : base("Hands", "Bones For hands", Main.Instance.DynamicBonesButton, null, true, false) { }

        public override void OnEnable()
        {
            SDK.Config.Instance.GB_HeadBones = true;
            SDK.Config.Instance.SaveConfig();
            Utilities.PU.ReloadAllAvatars();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "Added Hand Dynamic Bones To Players", false, false);
        }
        public override void OnDisable()
        {
            SDK.Config.Instance.GB_HeadBones = false;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "Removed Hand Dynamic Bones From Players", false, false);
        }
    }
}
