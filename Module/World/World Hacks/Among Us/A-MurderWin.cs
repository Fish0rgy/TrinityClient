using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_MurderWin : BaseModule
    {
        public A_MurderWin() : base("Murder Win", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Game Ended And Set The Murder As The Winner", false, false);
                A_Misc.AmongUsMod("SyncVictoryM");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
