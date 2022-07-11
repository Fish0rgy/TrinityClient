using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.Murder4_Settings
{
    internal class MurderGoBoomBoom : BaseModule
    {
        public MurderGoBoomBoom() : base("Boom Boom", "Kill Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Boom Boom {SelectedPlayer.displayName}", false, false);
                MenuUI.Log($"MURDER: <color=green>Boom Boom {SelectedPlayer.displayName}</color>");
                LogHandler.LogDebug($"Boom Boom {SelectedPlayer.displayName}");
                MurderMisc.TargetBoomBoom();
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        } 
    }
}
