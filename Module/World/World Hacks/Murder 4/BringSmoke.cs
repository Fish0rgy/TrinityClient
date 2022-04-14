using Trinity.Utilities;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class BringSmoke : BaseModule
    {
        public BringSmoke() : base("Give Smoke", "Gives You Every Item", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Teleported Smoke To Your Position", false, false);
                LogHandler.LogDebug("Teleported Smoke To Your Position");
                MurderMisc.MurderGive("Smoke");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
