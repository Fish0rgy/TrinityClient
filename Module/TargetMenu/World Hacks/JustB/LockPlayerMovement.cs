using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Area51.Module.TargetMenu.World_Hacks.JustB
{
    class LockPlayerMovement : BaseModule
    {
        public LockPlayerMovement() : base("Lock Movement", "Forcefully Tags Player", Main.Instance.JubstBSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), true, true) { }

        public override void OnEnable()
        {
            try
            {
                UdonExploitManager.udonsend("OnDesktopTopDownViewStart", "target");
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Locked {SelectedPlayer.displayName}'s Movement", false, false);
                LogHandler.LogDebug($"Locked {SelectedPlayer.displayName}'s Movement"); 
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public override void OnDisable()
        {
            try
            {
                UdonExploitManager.udonsend("OnPutDownCueLocally", "target");
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Unlocked {SelectedPlayer.displayName}'s Movement", false, false);
                LogHandler.LogDebug($"Unlocked {SelectedPlayer.displayName}'s Movement");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
