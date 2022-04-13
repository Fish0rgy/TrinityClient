using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_TaskDone : BaseModule
    {
        public A_TaskDone() : base("Complete Tasks", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "All Tasks Completed", false, false);
                A_Misc.AmongUsMod("OnLocalPlayerCompletedTask");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
