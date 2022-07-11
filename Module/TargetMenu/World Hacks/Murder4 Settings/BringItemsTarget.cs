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
    class BringItemsTarget : BaseModule
    {
        public BringItemsTarget() : base("Bring Items", "Brings All Items", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                MenuUI.Log("MURDER: <color=green>Brought All Items To Target</color>");
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Brought All Items To {SelectedPlayer.displayName}'s Position", false, false);
                LogHandler.LogDebug($"Brought All Items To {SelectedPlayer.displayName}'s Position");
                MurderMisc.MurderTargetGive("Smoke");
                MurderMisc.MurderTargetGive("Revolver");
                MurderMisc.MurderTargetGive("Knife");
                MurderMisc.MurderTargetGive("Shotgun");
                MurderMisc.MurderTargetGive("Frag");
                MurderMisc.MurderTargetGive("Luger");
                MurderMisc.MurderTargetGive("Bear Trap");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
