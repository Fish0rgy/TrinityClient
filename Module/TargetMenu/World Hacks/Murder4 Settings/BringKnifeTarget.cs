using Trinity.Utilities;
using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;
using Trinity.Utilities;

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    class BringKnifeTarget : BaseModule
    {
        public BringKnifeTarget() : base("Bring Knife", "Brings All Knifes", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Brought All Knifes {SelectedPlayer.displayName}'s Position", false, false);
                LogHandler.LogDebug($"Brought All Knifes {SelectedPlayer.displayName}'s Position"); 
                MurderMisc.MurderTargetGive("Knife");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
