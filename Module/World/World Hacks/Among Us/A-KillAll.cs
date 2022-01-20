using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Among_Us
{
    class A_KillAll : BaseModule
    {
        public A_KillAll() : base("Kill All", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Killed Everyone", false, false);
                A_Misc.AmongUsMod("KillLocalPlayer");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
