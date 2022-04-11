using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class EveryoneMurder : BaseModule
    {
        public EveryoneMurder() : base("Everyone Murder", "Give Murder Role To Everyone", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned Everyone The Murder Role", false, false);
                LogHandler.LogDebug("Assigned Everyone The Murder Role");
                MurderMisc.RoleAssignEveryone("SyncAssignM");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
