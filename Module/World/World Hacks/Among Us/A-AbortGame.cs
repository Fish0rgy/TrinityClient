using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Among_Us
{
    class A_AbortGame : BaseModule
    {
        public A_AbortGame() : base("Abort Game", "Abort Game With Nobody As The Winner", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Game Aborted!", false, false);
                A_Misc.AmongUsMod("SyncAbort");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
