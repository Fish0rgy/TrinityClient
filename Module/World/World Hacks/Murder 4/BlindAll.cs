using Trinity.Utilities;
using Trinity.SDK;
using System;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Murder_4
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
                MurderMisc.antiblind();
                UW.udonsend("OnLocalPlayerFlashbanged", EventTarget.Everyone);
                UW.udonsend("OnLocalPlayerBlinded", EventTarget.Everyone);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
