using Trinity.Utilities;
using Trinity.Module.World.World_Hacks.Murder_4;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    class A_AssignImposter : BaseModule
    {
        public A_AssignImposter() : base("Give Imposter", "Assigns You As The Imposter", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned You As The Imposter", false, false);
                LogHandler.LogDebug("Assigned You As The Imposter");
                MurderMisc.RoleAssign("SyncAssignM");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
