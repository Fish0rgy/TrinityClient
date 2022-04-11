using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
namespace Area51.Module.Settings.Preformance
{
    class ConsoleClear : BaseModule
    {
        public ConsoleClear() : base("Clear Console", "Clears Melon Loader Console", Main.Instance.SettingsButtonpreformance, QMButtonIcons.CreateSpriteFromBase64(Alien.ToggleOff), false, false)
        {
        }
        public override void OnEnable()
        {
            Console.Clear();        
            LogHandler.DisplayLogo();         
            LogHandler.Log(LogHandler.Colors.White, "Cleared Console!", false, false);
        }
    }
}