using Area51.SDK;
using Area51.SDK.ButtonAPI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Area51.Module.TargetMenu.World_Hacks.AmongUs_Settings
{
    class AssignImposter : BaseModule
    {
        public AssignImposter() : base("Assign Imposter", "Forcefully Assigns Player As Imposter", Main.Instance.AmongUsSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Assigned As Imposter", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Assigned As Imposter");
                UdonExploitManager.udonsend("SyncAssignM","target");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
