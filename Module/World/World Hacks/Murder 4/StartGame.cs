using Trinity.Utilities;
using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class StartGame : BaseModule
    {
        public StartGame() : base("Start Game", "Force Start Game", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Red, "Started Game", false, false);
                LogHandler.LogDebug("Started Game");
                MurderMisc.MurderMod("Btn_Start");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
