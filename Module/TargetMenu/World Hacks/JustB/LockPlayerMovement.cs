using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.JustB
{
    class LockPlayerMovement : BaseModule
    {
        public LockPlayerMovement() : base("Lock Movement", "Forcefully Tags Player", Main.Instance.JubstBSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }

        public override void OnEnable()
        {
            try
            {
                UW.udonsend("OnDesktopTopDownViewStart", EventTarget.Targeted);
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Locked {SelectedPlayer.displayName}'s Movement", false, false);
                LogHandler.LogDebug($"Locked {SelectedPlayer.displayName}'s Movement");
                MenuUI.Log($"JUSTB: <color=green>Locked {SelectedPlayer.displayName}'s Movement</color>");
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
                UW.udonsend("OnPutDownCueLocally", EventTarget.Targeted);
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Unlocked {SelectedPlayer.displayName}'s Movement", false, false);
                MenuUI.Log($"JUSTB: <color=green>Unlocked {SelectedPlayer.displayName}'s Movement</color>");
                LogHandler.LogDebug($"Unlocked {SelectedPlayer.displayName}'s Movement");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
