using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class ZombiesWin : BaseModule
    {
        public ZombiesWin() : base("Zombies Win", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Zombies Win", false, false);
                ZombieMisc.ZombieMod("ZombiesWin");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
