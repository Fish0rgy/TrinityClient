using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Diagnostics;
using System.IO;


namespace Area51.Module.Bot.Local
{
    class KIllBots : BaseModule
    {
        public KIllBots() : base("Bots Leave", "Tells Bots To Disconnect", Main.Instance.Privatebotbutton, QMButtonIcons.CreateSpriteFromBase64(Alien.skip), false) { }
        
        public override void OnEnable()
        {
            try
            {
                Console.Clear();
                LogHandler.DisplayLogo();
                foreach (var p in Process.GetProcessesByName("Area51")) { p.Kill(); };
                LogHandler.Log(LogHandler.Colors.Green, "Bots Killed!", false, false);
                LogHandler.LogDebug("Bots Killed!");
            }
            catch (Exception ex)
            {
            }
        }
    }
}