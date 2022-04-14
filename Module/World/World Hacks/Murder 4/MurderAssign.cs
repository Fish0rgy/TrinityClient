using Trinity.Utilities;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class MurderAssign : BaseModule
    {
        public MurderAssign() : base("Give Murder", "Give Murder Role", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned You The Murder Role", false, false);
                LogHandler.LogDebug("Assigned You The Murder Role");
                MurderMisc.RoleAssign("SyncAssignM");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
