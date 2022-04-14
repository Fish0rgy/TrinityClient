using Trinity.Utilities;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class CloseDoors : BaseModule
    {
        public CloseDoors() : base("Close Doors", "Close All Doors", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Closed All Doors", false, false);
                LogHandler.LogDebug("Closed All Doors");
                MurderMisc.ObjectInteract("Interact close");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
