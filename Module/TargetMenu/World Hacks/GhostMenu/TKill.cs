using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Module.World.World_Hacks.Ghost;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.GhostMenu
{
    class TKill : BaseModule
    {
        public TKill() : base("Kill", "Gives Player Max Money", Main.Instance.GhostTargetButton, QMButtonIcons.LoadSpriteFromFile(Serpent.Games), false, false) { }

        public override void OnEnable()
        {
            try
            {
                 
                GhostMisc.KillTarget();
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Killed {SelectedPlayer.displayName}", false, false);
                MenuUI.Log($"JUSTB: <color=green>Killed {SelectedPlayer.displayName}</color>");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
