using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.World.World_Hacks.MagicFreezeTag
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
