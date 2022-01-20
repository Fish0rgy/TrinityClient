using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class MakeHuman : BaseModule
    {
        public MakeHuman() : base("Become Human", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Turned You Into A Human", false, false);
                ZombieMisc.ZombieMod("MakeHuman");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
