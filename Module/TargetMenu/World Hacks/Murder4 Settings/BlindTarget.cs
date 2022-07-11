using Trinity.Utilities;

using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using Trinity.Module.World.World_Hacks.Murder_4;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
     class BlindTarget : BaseModule
    {
        public BlindTarget() : base("Blind Target", "Blind Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                MenuUI.Log("MURDER: <color=green>Blinded Target</color>");
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Blinded {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Blinded {SelectedPlayer.displayName}");
                MurderMisc.antiblind();
                UW.udonsend("OnLocalPlayerBlinded", EventTarget.Targeted);
                UW.udonsend("OnLocalPlayerFlashbanged", EventTarget.Targeted);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
