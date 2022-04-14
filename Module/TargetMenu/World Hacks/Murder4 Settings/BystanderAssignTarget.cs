using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class BystanderAssignTarget : BaseModule
    {
        public BystanderAssignTarget() : base("Assign Bystander", "Assigns Player As Bystander", Main.Instance.MurderSettings, QMButtonIcons.CreateSpriteFromBase64(Serpent.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Assigned {SelectedPlayer.displayName} As Bystander", false, false);
                LogHandler.LogDebug($"Assigned {SelectedPlayer.displayName} As Bystander");
                MurderMisc.TargetedEvent("SyncAssignB");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
