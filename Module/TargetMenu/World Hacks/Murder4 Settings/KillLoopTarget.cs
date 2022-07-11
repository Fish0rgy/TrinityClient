using Trinity.Utilities;
using Trinity.Module.World.World_Hacks.Murder_4;
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
using Trinity.Utilities;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class KillLoopTarget : BaseModule
    {
        public KillLoopTarget() : base("Murder Kill Loop", "Kill Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }

        public override void OnEnable()
        {
            try
            { 
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Killed {SelectedPlayer.displayName}", false, false); 
                MenuUI.Log($"MURDER: <color=green>Killed {SelectedPlayer.displayName}</color>");
                LogHandler.LogDebug($"Killed {SelectedPlayer.displayName}");
                MelonCoroutines.Start(KillingLoop());
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public override void OnDisable()
        {
            MelonCoroutines.Stop(KillingLoop());
        }
        public IEnumerator KillingLoop()
        {
            GameObject frag = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
            while (toggled)
            {
                MurderMisc.TargetedEvent("SyncKill");
                MurderMisc.TargetedEvent("SyncAssignB");
                yield return new WaitForSeconds(0.7f);
            }
            yield break;
        }
    }
}
