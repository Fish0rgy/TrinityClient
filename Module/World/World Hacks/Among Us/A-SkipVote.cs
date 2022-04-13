using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_SkipVote : BaseModule
    {
        public A_SkipVote() : base("Skip Voting", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Voting Skipped", false, false);
                A_Misc.AmongUsMod("Btn_SkipVoting");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
