using Trinity.Utilities;
using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Murder_4
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
                LogHandler.Log(LogHandler.Colors.Green, "Ended Game With No One As The Victor", false, false);
                LogHandler.LogDebug("Ended Game With No One As The Victor");
                MurderMisc.MurderMod("SyncAbort");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
