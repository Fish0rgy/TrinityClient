using Area51.Module.World.World_Hacks.Murder_4;
using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Area51.Module.TargetMenu.Murder4_Settings
{
    class MurderAssignTarget : BaseModule
	{
		public MurderAssignTarget() : base("Assign Murder", "Assigns Player As Murder", Main.Instance.MurderSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }

		public override void OnEnable()
		{
            try
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Assigned {SelectedPlayer.displayName} As Murder", false, false);
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
