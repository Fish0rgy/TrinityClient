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
    class TargetMCTeleport : BaseModule
    {
        public TargetMCTeleport() : base("Respawn Loop", "Targeted Teleport", Main.Instance.MoveAndChillSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }
        public override void OnEnable()
        {
            try
            {
                MelonLoader.MelonCoroutines.Start(gay());
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Force Respawned {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Respawn Loop");
                MenuUI.Log($"MOVIE: <color=green>{SelectedPlayer.displayName} Is In A Respawn Loop</color>");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public override void OnDisable()
        {
            MelonLoader.MelonCoroutines.Stop(gay());
        }
        IEnumerator gay()
        {
            while (toggled)
            { 
                UW.ObjectEvent("Private Room TP", "Teleport", EventTarget.Targeted);
                yield return new WaitForSecondsRealtime(0.2f);
            }
            yield break;
        }
    }
}
