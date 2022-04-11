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
    class ForceRespawn : BaseModule
    {
        public ForceRespawn() : base("Force Respawn", "Forcefully Respawn's Player", Main.Instance.JubstBSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.Movment), false, false) { }

        public override void OnEnable()
        {
            try
            {
                UdonExploitManager.udonsend("StartTimerWithCooldown", "target");
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Force Respawned {SelectedPlayer.displayName}", false, false);
                LogHandler.LogDebug($"Force Respawned -> {SelectedPlayer.displayName}");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
