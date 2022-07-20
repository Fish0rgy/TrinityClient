using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.MovieAndChill
{
    class TargetMCLag : BaseModule
    {
        public TargetMCLag() : base("Teleport", "Targeted Teleport Once", Main.Instance.MoveAndChillSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Teleported", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Teleported"); 
                MenuUI.Log($"MOVIE: <color=green>Target Teleport {SelectedPlayer.displayName}</color>");
                UW.ObjectEvent("Private Room TP", "Teleport", EventTarget.Targeted);

            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        } 
    }
}
