using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class BystanderWin : BaseModule
    {
        public BystanderWin() : base("Bystander Win", "Bystander Wins", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Ended Game With The Bystanders As The Victor", false, false);
                LogHandler.LogDebug("Ended Game With The Bystanders As The Victor");
                MurderMisc.MurderMod("SyncVictoryB");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
