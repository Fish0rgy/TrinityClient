using Area51.SDK;
using System;
namespace Area51.Module.Settings.Preformance
{
    class ConsoleClear : BaseModule
    {
        public ConsoleClear() : base("Clear Console", "Clears Melon Loader Console", Main.Instance.SettingsButtonpreformance, null, false)
        {
        }
        public override void OnEnable()
        {
            Console.Clear();        
            Logg.DisplayLogo();         
            Logg.Log(Logg.Colors.White, "Cleared Console!", false, false);
        }
    }
}
