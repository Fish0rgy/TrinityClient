using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class MakeZombie : BaseModule
    {
        public MakeZombie() : base("Become Zombie", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Turned You Into A Zombie", false, false);
                ZombieMisc.ZombieMod("MakeZombie");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
