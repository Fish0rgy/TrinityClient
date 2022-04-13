using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class ShoveDoors : BaseModule
    {
        public ShoveDoors() : base("Shove Doors", "Shove All Doors", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Lock All Doors", false, false);
                LogHandler.LogDebug("Lock All Doors");
                MurderMisc.ObjectInteract("Interact shove");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
