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

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class BlindLoopTarget : BaseModule
    {
        public BlindLoopTarget() : base("Blind Loop", "Blinds Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Blinding {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Blinding {SelectedPlayer.displayName}");
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
                UdonExploitManager.udonsend("OnLocalPlayerBlinded","target");
                yield return new WaitForSeconds(0.2f);
                UdonExploitManager.udonsend("OnLocalPlayerFlashbanged","target");
                yield return new WaitForSeconds(0.2f);
            }
            yield break;
        }
    }
}
