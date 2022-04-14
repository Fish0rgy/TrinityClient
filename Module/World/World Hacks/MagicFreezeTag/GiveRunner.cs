using Trinity.Utilities;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.MagicFreezeTag
{
    class GiveRunner : BaseModule
    {
        public GiveRunner() : base("Give Runner", "", Main.Instance.Magictagbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned you as the runner", false, false);
                LogHandler.LogDebug("Your The Runner!");
                UdonExploitManager.udonsend("AssignRunner","local");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
