using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.AmongUs_Settings
{
    class AssignImposter : BaseModule
    {
        public AssignImposter() : base("Assign Imposter", "Forcefully Assigns Player As Imposter", Main.Instance.AmongUsSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Assigned As Imposter", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Assigned As Imposter");
                UW.udonsend("SyncAssignM", EventTarget.Targeted);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
