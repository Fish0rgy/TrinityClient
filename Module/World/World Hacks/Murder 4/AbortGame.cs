using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class AbortGame : BaseModule
    {
        public AbortGame() : base("Abort Game", "No One Wins", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Ended Game With No One As The Victor", false, false);
                Logg.LogDebug("Ended Game With No One As The Victor");
                MurderMisc.MurderMod("SyncAbort");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
