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
                Logg.Log(Logg.Colors.Green, "Ended Game With The Murder As The Victor", false, false);
                Logg.LogDebug("Ended Game With The Murder As The Victor");
                MurderMisc.MurderMod("SyncVictoryM");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
