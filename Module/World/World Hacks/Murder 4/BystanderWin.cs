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
                Logg.Log(Logg.Colors.Green, "Ended Game With The Bystanders As The Victor", false, false);
                Logg.LogDebug("Ended Game With The Bystanders As The Victor");
                MurderMisc.MurderMod("SyncVictoryB");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
