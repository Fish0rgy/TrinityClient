using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
namespace Trinity.Module.Settings.Preformance
{
    class ConsoleClear : BaseModule
    {
        public ConsoleClear() : base("Clear Console", "Clears Melon Loader Console", Main.Instance.SettingsButtonpreformance, QMButtonIcons.CreateSpriteFromBase64(Serpent.ToggleOff), false, false)
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