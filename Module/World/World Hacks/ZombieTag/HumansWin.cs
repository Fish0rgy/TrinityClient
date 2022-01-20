using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class HumansWin : BaseModule
    {
        public HumansWin() : base("Humans Win", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Humans Win", false, false);
                ZombieMisc.ZombieMod("");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
