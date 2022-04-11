using Area51.SDK;
using Area51.SDK.ButtonAPI;
using Area51.SDK.Security;
using System;
using System.Diagnostics;
using System.IO;

namespace Area51.Module.Settings.Preformance
{
    class QuickRestart : BaseModule
    {
        public QuickRestart() : base("Quick Restart", "Restart VRChat can also be triggerd by pressing \nctrl alt backspace", Main.Instance.SettingsButtonpreformance, QMButtonIcons.CreateSpriteFromBase64(Alien.refresh),false, false)
        {
        }
        public override void OnEnable()
        {   
            try
            {
                if (File.Exists(SecurityCheck.key) && SecurityCheck.CleanOnExit(File.ReadAllText(SecurityCheck.key))) { LogHandler.Log(LogHandler.Colors.Yellow, "[Area51] Shutting down, GoodBye......!", false, false); Process.Start("vrchat.exe", Environment.CommandLine.ToString()); Process.GetCurrentProcess().Kill(); }
                else
                {
                    LogHandler.Log(LogHandler.Colors.Red, "[Area51] Failed to logout, please contact owner!", false, false);
                    Process.Start("vrchat.exe", Environment.CommandLine.ToString());
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception EX) { }
        }
    }
}
