using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.SDK.Security;
using System;
using System.Diagnostics;
using System.IO;

namespace Trinity.Module.Settings.Preformance
{
    class QuickRestart : BaseModule
    {
        public QuickRestart() : base("Quick Restart", "Restart VRChat can also be triggerd by pressing \nctrl alt backspace", Main.Instance.SettingsButtonpreformance, QMButtonIcons.LoadSpriteFromFile(Serpent.refreshPath),false, false)
        {
        }
        public override void OnEnable()
        {
            Process.Start("vrchat.exe", Environment.CommandLine.ToString());
            Process.GetCurrentProcess().Kill();
        }
    }
}
