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
    class MakeTagger : BaseModule
    {
        public MakeTagger() : base("Assign Tagger", "Assigns Tagger", Main.Instance.MagicTagSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }

        public override void OnEnable()
        {
            try
            {
                UdonExploitManager.udonsend("AssignTagger", "target");
                APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                LogHandler.Log(LogHandler.Colors.Green, $"{SelectedPlayer.displayName} Forcefully Assigned Tagger", false, false);
                LogHandler.LogDebug($"{SelectedPlayer.displayName} Assigned Tagger"); 
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
