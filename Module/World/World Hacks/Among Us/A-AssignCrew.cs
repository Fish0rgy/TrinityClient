using Area51.Module.World.World_Hacks.Murder_4;
using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.World.World_Hacks.Among_Us
{
    class A_AssignCrew : BaseModule
    {
        public A_AssignCrew() : base("Give Crew", "", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned You As The Crew", false, false);
                LogHandler.LogDebug("Assigned You As The Crew");
                MurderMisc.RoleAssign("SyncAssignB");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
