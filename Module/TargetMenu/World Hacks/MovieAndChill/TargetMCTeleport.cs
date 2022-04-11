using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Area51.Module.TargetMenu.World_Hacks.MovieAndChill
{
    class TargetMCTeleport : BaseModule
    {
        public TargetMCTeleport() : base("Force Respawn", "Targeted Teleport", Main.Instance.MoveAndChillSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                UdonExploitManager.ObjectEvent("", "Teleport", 1);
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Teleported Outside Map", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Respawned"); 
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
