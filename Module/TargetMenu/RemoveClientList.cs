using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class RemoveClientList : BaseModule
    {
        public RemoveClientList() : base("Remove \nClientUser", "Removes user from the client blacklist", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.raygunPath), false, false) { }
        
        public override void OnEnable()
        {
            APIUser SelectedPlayer = PU.SelectedVRCPlayer().prop_APIUser_0; 
            var lines = System.IO.File.ReadLines($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\ClientUsers.txt");
            foreach (var line in lines)
            {
                if (line.Contains(SelectedPlayer.id))
                {
                    line.Replace(SelectedPlayer.id, "");
                    PU.ClientUserIDs.Remove(SelectedPlayer.id);
                }
            }
            MenuUI.Log($"PLAYER: <color=green>Removed {SelectedPlayer.displayName} From Client BlackList</color>"); 
            LogHandler.LogDebug("[BlackList] -> Removed User From Client BlackList");
        }
    }
}
