using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class KillHumans : BaseModule
    {
        public KillHumans() : base("Kill Humans", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Killed Everyone", false, false);
                GhostMisc.KillHumans();
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
