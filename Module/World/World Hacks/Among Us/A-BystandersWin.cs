using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_BystandersWin : BaseModule
    {
        public A_BystandersWin() : base("Bystanders Win", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Game Ended And Set Bystanders As The Winners", false, false);
                A_Misc.AmongUsMod("SyncVictoryB");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
