using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Bones
{
    internal class Bhead : BaseModule
    {
        public Bhead() : base("Head", "Bones For heads", Main.Instance.DynamicBonesButton, null, true, false) { }

        public override void OnEnable()
        {
            SDK.Config.Instance.GB_HeadBones = true;
            SDK.Config.Instance.SaveConfig();
            Utilities.PU.ReloadAllAvatars();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "Added Head Dynamic Bones To Players", false, false);
        }
        public override void OnDisable()
        {
            SDK.Config.Instance.GB_HeadBones = false;
            SDK.Config.Instance.SaveConfig();
            Trinity.SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "Removed Head Dynamic Bones From Players", false, false);
        }
    }
}
