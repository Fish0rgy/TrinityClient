using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Among_Us
{
    class A_CloseVote : BaseModule
    {
        public A_CloseVote() : base("Close Voting", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Closed Voting", false, false);
                A_Misc.AmongUsMod("SyncCloseVoting");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
