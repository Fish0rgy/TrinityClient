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
                 
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.Message);
            }
        }
    }
}
