using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.MovieAndChill
{
    class TargetMCLag : BaseModule
    {
        public TargetMCLag() : base("Target Lag", "Targeted Item/Trigger Lagger", Main.Instance.MoveAndChillSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), true, true) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Is Lagging", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Is Lagging");
                for (int i = 0; i < 10; i++)
                {
                    UdonExploitManager.udonsend("OnObjectRootPickupUseDown", "target");

                }
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        } 
    }
}
