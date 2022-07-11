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

namespace Trinity.Module.TargetMenu.Murder4_Settings
{
    internal class BringRevolverTarget : BaseModule
    {
        public BringRevolverTarget() : base("Bring Revolver", "Brings Revolver", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                MenuUI.Log("MURDER: <color=green>Brought Revolver To Target</color>");
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"Brought Revolver {SelectedPlayer.displayName}'s Position", false, false);
                LogHandler.LogDebug($"Brought Revolver {SelectedPlayer.displayName}'s Position");
                MurderMisc.MurderTargetGive("Revolver");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
