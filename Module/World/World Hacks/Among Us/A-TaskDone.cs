using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Among_Us
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
                Logg.Log(Logg.Colors.Green, "All Tasks Completed", false, false);
                A_Misc.AmongUsMod("OnLocalPlayerCompletedTask");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
