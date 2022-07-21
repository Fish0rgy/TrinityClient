using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.AmongUs_Settings
{
    internal class AmongUsTeleport : BaseModule
    {
        public AmongUsTeleport() : base("Teleport", "Forcefully Teleports Player", Main.Instance.AmongUsSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Teleported", false, false); 
                UW.udonsend("SyncVotedOut", EventTarget.Targeted);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
