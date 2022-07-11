using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.JustB
{
    class ForceRespawn : BaseModule
    {
        public ForceRespawn() : base("Force Respawn", "Forcefully Respawn's Player", Main.Instance.JubstBSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.MovmentPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                UW.udonsend("StartTimerWithCooldown", EventTarget.Targeted);
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Force Respawned {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Force Respawned -> {SelectedPlayer.displayName}");
                MenuUI.Log($"JUSTB: <color=green>Force Respawned {SelectedPlayer.displayName}</color>");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
