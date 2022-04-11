using Area51.SDK;
using System;
using UnityEngine;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class BlindAll : BaseModule
    {
        public BlindAll() : base("Blind Everyone", "Black Screen 4 Seconds", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Blinded Everyone In The Lobby", false, false);
                LogHandler.LogDebug("Blinded Everyone In The Lobby"); 
                UdonExploitManager.udonsend("OnLocalPlayerFlashbanged", "everyone");
                UdonExploitManager.udonsend("OnLocalPlayerBlinded", "everyone");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
