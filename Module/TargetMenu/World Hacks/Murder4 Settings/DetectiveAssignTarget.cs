using Trinity.Utilities;
using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class DetectiveAssignTarget : BaseModule
    {
        public DetectiveAssignTarget() : base("Assign Detective", "Assigns Player As Detective", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                MenuUI.Log("MURDER: <color=green>Assigned Player As Detective</color>");
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Assigned {SelectedPlayer.displayName} As Detective", false, false);
                LogHandler.LogDebug($"Assigned {SelectedPlayer.displayName} As Detective");
                MurderMisc.TargetedEvent("SyncAssignD");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
