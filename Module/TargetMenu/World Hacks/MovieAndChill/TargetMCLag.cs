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
    class TargetMCLag : BaseModule
    {
        public TargetMCLag() : base("Target Lag", "Targeted Item/Trigger Lagger", Main.Instance.MoveAndChillSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                UdonExploitManager.udonsend("OnObjectRootPickupUseDown", "target");
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Is Lagging", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Is Lagging"); 
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
