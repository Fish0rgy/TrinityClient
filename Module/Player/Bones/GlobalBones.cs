using System;
using System.Collections.Generic;
using Trinity.SDK;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;

namespace Trinity.Module.Player
{
    class GlobalBones : BaseModule
    {
        public GlobalBones() : base("Dynamic Bones", "Bones For Everyone", Main.Instance.DynamicBonesButton, null, true, true) { }

        public override void OnEnable()
        {
			PU.AllPlayers2().Array.ToList().ForEach(delegate (VRC.Player plr)
            {
                PU.ProcessDynamicBones(plr.GetAvatar(), plr.GetAPIUser());
            });
            LogHandler.Log(LogHandler.Colors.Green, "Added Global Dynamic Bones To All Players", false, false);
        }
        public override void OnDisable()
        {
            PU.AllPlayers2().Array.ToList().ForEach(delegate (VRC.Player plr)
            {
                PU.RemoveDynamicBones(plr.GetAvatar(), plr.GetAPIUser());
            });
            LogHandler.Log(LogHandler.Colors.Red,"Removed Global Dynamic Bones From All Players",false,false);
        }
    }
}
