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

namespace Trinity.Module.TargetMenu
{
    internal class MurderKillTarget : BaseModule
    {
        public MurderKillTarget() : base("Murder Kill", "Kill Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                MenuUI.Log($"MURDER: <color=green>Killed {SelectedPlayer.displayName}</color>");
                LogHandler.Log(LogHandler.Colors.Green, $"Killed {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Killed {SelectedPlayer.displayName}");
                MurderMisc.TargetedEvent("SyncKill");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            } 
        }
    }
}
