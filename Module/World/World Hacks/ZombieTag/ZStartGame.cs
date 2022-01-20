using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class ZStartGame : BaseModule
    {
        public ZStartGame() : base("Start Game", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Game Started", false, false);
                ZombieMisc.ZombieMod("StartGame");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
