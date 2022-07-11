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
    class AssignCrew : BaseModule
    {
        public AssignCrew() : base("Assign Crew", "Forcefully Assigns Player As Crew", Main.Instance.AmongUsSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Assigned As Crew", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Assigned As Crew");
                UW.udonsend("SyncAssignB", EventTarget.Targeted);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
