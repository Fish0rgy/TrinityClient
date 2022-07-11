using Trinity.Utilities;
using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class MurderAssignTarget : BaseModule
	{
		public MurderAssignTarget() : base("Assign Murder", "Assigns Player As Murder", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

		public override void OnEnable()
		{
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Assigned {SelectedPlayer.displayName} As Murder", false, false);

                MenuUI.Log($"MURDER: <color=green>Assigned {SelectedPlayer.displayName} As Murder</color>");
                LogHandler.LogDebug($"Assigned {SelectedPlayer.displayName} As Murder");
                MurderMisc.TargetedEvent("SyncAssignM");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
	}
}
