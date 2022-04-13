using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.MagicFreezeTag
{
    class Unfreeze : BaseModule
    {
        public Unfreeze() : base("Un-Freeze", "", Main.Instance.Magictagbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Your Un Frozen", false, false);
                LogHandler.LogDebug("Your Un Frozen");
                UdonExploitManager.udonsend("Unfreeze", "local");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
