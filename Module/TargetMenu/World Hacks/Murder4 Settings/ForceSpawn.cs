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
    class ForceSpawn : BaseModule
    {
        public ForceSpawn() : base("Force Spawn", "Forcefully Spawned", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            { 
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                MenuUI.Log($"MURDER: <color=green>Force Respawned {SelectedPlayer.displayName}</color>");
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Forcefully Spawned", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Forcefully Spawned");
                MurderMisc.TargetedEvent("SyncAssignB");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
