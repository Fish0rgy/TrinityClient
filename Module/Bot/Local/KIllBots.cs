using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Diagnostics;
using System.IO;


namespace Trinity.Module.Bot.Local
{
    class KIllBots : BaseModule
    {
        public KIllBots() : base("Bots Leave", "Tells Bots To Disconnect", Main.Instance.Privatebotbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.stopbots), false) { }
        
        public override void OnEnable()
        {
            try
            {
                Console.Clear();
                LogHandler.DisplayLogo();
                foreach (var p in Process.GetProcessesByName("Trinity")) { p.Kill(); };
                LogHandler.Log(LogHandler.Colors.Green, "Bots Killed!", false, false);
                LogHandler.LogDebug("Bots Killed!");
            }
            catch (Exception ex)
            {
            }
        }
    }
}