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
        public TargetMCLag() : base("Target Lag", "Targeted Item/Trigger Lagger", Main.Instance.MoveAndChillSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Is Lagging", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Is Lagging"); 
                MenuUI.Log($"MOVIE: <color=green>Target Lagging {SelectedPlayer.displayName}</color>");
                for (int i = 0; i < 10; i++)
                {
                    UW.udonsend("OnObjectRootPickupUseDown", EventTarget.Targeted);

                }
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        } 
    }
}
