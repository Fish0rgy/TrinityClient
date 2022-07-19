using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.Settings.Spoofer
{
    internal class NoSteam : BaseModule
    {

        public NoSteam() : base("Spoof Steam", "Disconnnects Steam From Vrchat", Main.Instance.SettingsButtonspoofer, null, true, true) { }

        public override void OnEnable()
        {
            Trinity.SDK.Config.SpoofSteam = true;
            System.IO.File.WriteAllText("Trinity\\Misc\\Steam.txt", "true");
            LogHandler.Log(LogHandler.Colors.Green, "Restart to apply changes", false, false);
        }
        public override void OnDisable()
        {
            Trinity.SDK.Config.SpoofSteam = false;
            System.IO.File.WriteAllText("Trinity\\Misc\\Steam.txt", "false");
            LogHandler.Log(LogHandler.Colors.Green, "Restart to apply changes", false, false);
        }
    }
}
