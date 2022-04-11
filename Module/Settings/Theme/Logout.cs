using Area51.SDK;
using Area51.SDK.ButtonAPI;
using Area51.SDK.Security;
using System;
using System.Diagnostics;
using System.IO;

namespace Area51.Module.Settings.Theme
{
    class Logout : BaseModule
    {
        public Logout() : base("Logout", "This logs you our and exist vrchat.", Main.Instance.SettingsButtonpreformance, QMButtonIcons.CreateSpriteFromBase64(Alien.powerbutton), false, false) { }

        public override void OnEnable()
        {
            try
            {
                if (File.Exists(SecurityCheck.key) && SecurityCheck.CleanOnExit(File.ReadAllText(SecurityCheck.key))) { LogHandler.Log(LogHandler.Colors.Yellow, "[Area51] Logged Out", false, false); Process.GetCurrentProcess().Kill(); }
                else
                {
                    LogHandler.Log(LogHandler.Colors.Red, "[Area51] Failed To logout", false, false); 
                }
            }
            catch (Exception EX) { }
        }
    }
}

