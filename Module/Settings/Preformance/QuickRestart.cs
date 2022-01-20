using System;
using System.Diagnostics;

namespace Area51.Module.Settings.Preformance
{
    class QuickRestart : BaseModule
    {
        public QuickRestart() : base("Quick Restart", "Restart VRChat can also be triggerd by pressing \nctrl alt backspace", Main.Instance.SettingsButtonpreformance, null, false)
        {
        }
        public override void OnEnable()
        {
            Process.Start("vrchat.exe", Environment.CommandLine.ToString());
            Process.GetCurrentProcess().Kill();
        }
    }
}
