using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.SDK.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Settings.Preformance
{
    class ReLogin : BaseModule
    {
        public ReLogin() : base("Re-Login", "Failed To Login? Press Me To Try Again!", Main.Instance.SettingsButtonpreformance, QMButtonIcons.LoadSpriteFromFile(Serpent.refreshPath), false, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                if (File.Exists(SecurityCheck.key) && SecurityCheck.GetServerInfo(File.ReadAllText(SecurityCheck.key)))
                {
                    //200
                    MenuUI.Log("API: <color=green>Successful Relogin</color>");
                    LogHandler.Log(LogHandler.Colors.Green, "[Trinity] Successful Relogin", false, false);
                }
                else { LogHandler.Log(LogHandler.Colors.Red, "[Trinity] Unsuccessful Relogin", false, false);
                    MenuUI.Log("API: <color=red>Unsuccessful Relogin</color>");
                }
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
            }
        }
    }
}
