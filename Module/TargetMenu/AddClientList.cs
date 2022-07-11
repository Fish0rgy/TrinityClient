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
    internal class AddClientList : BaseModule
    {
        public AddClientList() : base("Add ClientUser", "Adds user to the client blacklist", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.raygunPath), false, false) { }

        public override void OnEnable()
        {
            APIUser SelectedPlayer = PU.SelectedVRCPlayer().prop_APIUser_0;
            bool checkfile = System.IO.File.ReadLines($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\ClientUsers.txt").Any(line => line.Contains(SelectedPlayer.id)); 
            if (SelectedPlayer.id != "")
            {
                if (!checkfile)
                {
                    System.IO.File.AppendAllText($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\ClientUsers.txt", $"{SelectedPlayer.id}{Environment.NewLine}"); 
                    PU.ClientUserIDs.Add(SelectedPlayer.id);
                    MenuUI.Log($"PLAYER: <color=green>Added {SelectedPlayer.displayName} To Client BlackList</color>");
                } 
            } 
            LogHandler.LogDebug("[BlackList] -> Added User To Client BlackList");
        }
    }  
}
