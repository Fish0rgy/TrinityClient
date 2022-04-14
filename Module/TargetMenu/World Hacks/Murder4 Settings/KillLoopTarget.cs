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
                APIUser SelectedPlayer = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, "Killed {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug("Killed {SelectedPlayer.displayName}");
                MelonCoroutines.Start(KillingLoop());
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        } 
        public IEnumerator KillingLoop()
        {

            while (toggled)
            {
                MurderMisc.TargetedEvent("SyncKill");
                yield return new WaitForSeconds(0.1f);
            }
            yield break;
        }
    }
}
