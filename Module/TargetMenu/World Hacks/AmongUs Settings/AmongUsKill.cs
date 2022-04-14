using Trinity.SDK;
using Trinity.SDK.ButtonAPI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.AmongUs_Settings
{
    class AmongUsKill : BaseModule
    {
        public AmongUsKill() : base("Kill Player", "Targeted Udon Event That Kills Player", Main.Instance.AmongUsSettings, QMButtonIcons.CreateSpriteFromBase64(Serpent.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Killed", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Killed");
                UdonExploitManager.udonsend("SyncKill", "target");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
