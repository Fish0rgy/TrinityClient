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
    class MakeRunner : BaseModule
    {
        public MakeRunner() : base("Assign Runner", "Assigns Runner", Main.Instance.MurderSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                APIUser SelectedPlayer = Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Forcefully Assigned Runner", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Assigned Runner");
                UW.udonsend("AssignRunner", EventTarget.Local);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
