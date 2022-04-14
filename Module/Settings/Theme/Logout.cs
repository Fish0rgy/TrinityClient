using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.SDK.Security;
using System;
using System.Diagnostics;
using System.IO;

namespace Trinity.Module.Settings.Theme
{
    class Logout : BaseModule
    {
        public Logout() : base("Logout", "This logs you our and exist vrchat.", Main.Instance.SettingsButtonpreformance, QMButtonIcons.LoadSpriteFromFile(Serpent.powerbuttonPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                if (File.Exists(SecurityCheck.key) && SecurityCheck.CleanOnExit(File.ReadAllText(SecurityCheck.key))) { LogHandler.Log(LogHandler.Colors.Yellow, "[Trinity] Logged Out", false, false); Process.GetCurrentProcess().Kill(); }
                else
                {
                    LogHandler.Log(LogHandler.Colors.Red, "[Trinity] Failed To logout", false, false); 
                }
            }
            catch (Exception EX) { }
        }
    }
}

