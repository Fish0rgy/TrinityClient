using Trinity.Utilities;
using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_CallMeeting : BaseModule
    {
        public A_CallMeeting() : base("Call Meeting", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Meeting Called", false, false);
                A_Misc.AmongUsMod("StartMeeting");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
