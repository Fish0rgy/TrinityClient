using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class DetectiveAssign : BaseModule
    {
        public DetectiveAssign() : base("Give Detective", "Give Detective Role", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned You The Detective Role", false, false);
                LogHandler.LogDebug("Assigned You The Detective Role");
                MurderMisc.RoleAssign("SyncAssignD");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
