using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class BringRevolver : BaseModule
    {
        public BringRevolver() : base("Give Revolver", "Gives You Every Item", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Teleported Revolver To Your Position", false, false);
                LogHandler.LogDebug("Teleported Revolver To Your Position");
                MurderMisc.MurderGive("Revolver");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
