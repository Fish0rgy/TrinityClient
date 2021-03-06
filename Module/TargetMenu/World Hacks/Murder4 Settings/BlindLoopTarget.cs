using Trinity.Utilities;

using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using Trinity.Module.World.World_Hacks.Murder_4;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class BlindLoopTarget : BaseModule
    {
        public BlindLoopTarget() : base("Blind Loop", "Blinds Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }

        public override void OnEnable()
        {
            try
            {
                MenuUI.Log("MURDER: <color=green>Blind Loop Target</color>");
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Blinding {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Blinding {SelectedPlayer.displayName}");
                MurderMisc.antiblind();
                MelonCoroutines.Start(BlindingLoop());
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public IEnumerator BlindingLoop()
        {

            while (toggled)
            {
                UW.udonsend("OnLocalPlayerBlinded", EventTarget.Targeted); 
                UW.udonsend("OnLocalPlayerFlashbanged", EventTarget.Targeted);
                yield return new WaitForSeconds(0.2f);
            }
            yield break;
        }
    }
}
