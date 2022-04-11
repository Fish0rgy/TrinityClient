
using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Area51.Module.TargetMenu.Murder4_Settings
{
     class BlindTarget : BaseModule
    {
        public BlindTarget() : base("Blind Target", "Blind Someone In Murder 4", Main.Instance.MurderSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Blinded {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Blinded {SelectedPlayer.displayName}");
                UdonExploitManager.udonsend("OnLocalPlayerBlinded", "target");
                UdonExploitManager.udonsend("OnLocalPlayerFlashbanged","target");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
