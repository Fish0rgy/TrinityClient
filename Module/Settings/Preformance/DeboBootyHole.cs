using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;

namespace Trinity.Module.Settings.Preformance
{
    internal class DeboBootyHole : BaseModule
    {
        public DeboBootyHole() : base("DeboBootyHole", "Debo Is Sexy", Main.Instance.SettingsButtonpreformance, QMButtonIcons.LoadSpriteFromFile(Serpent.TeleportPath), false, false)
        {
        }
        public override void OnEnable()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
