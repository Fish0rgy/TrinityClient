using Trinity.Utilities;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.MagicFreezeTag
{
    class GiveTagger : BaseModule
    {
        public GiveTagger() : base("Give Tagger", "", Main.Instance.Magictagbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Assigned you as the runner", false, false);
                LogHandler.LogDebug("Your The Runner!");
                UdonExploitManager.udonsend("AssignTagger", "local");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
