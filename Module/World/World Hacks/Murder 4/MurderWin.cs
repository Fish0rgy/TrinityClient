using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class MurderWin : BaseModule
    {
        public MurderWin() : base("Murder Win", "Murder Wins", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Ended Game With The Murder As The Victor", false, false);
                LogHandler.LogDebug("Ended Game With The Murder As The Victor");
                MurderMisc.MurderMod("SyncVictoryM");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
