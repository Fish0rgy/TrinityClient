using Area51.SDK;
using System;

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
                Logg.Log(Logg.Colors.Green, "Blinded Everyone In The Lobby", false, false);
                Logg.LogDebug("Blinded Everyone In The Lobby");
                MurderMisc.MurderMod("OnLocalPlayerBlinded");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
