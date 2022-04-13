using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_StartGame : BaseModule
    {
        public A_StartGame() : base("Start Game", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Game Started", false, false);
                A_Misc.AmongUsMod("Btn_Start");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
