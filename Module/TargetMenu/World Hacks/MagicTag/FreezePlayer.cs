using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.TargetMenu.World_Hacks.MagicTag
{
    class FreezePlayer : BaseModule
    {
        public FreezePlayer() : base("Force Tag", "Forcefully Tags Player", Main.Instance.MagicTagSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Forcefully Tagged", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Forcefully Tagged");
                UW.udonsend("OnTaggedPlayer", EventTarget.Targeted);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
