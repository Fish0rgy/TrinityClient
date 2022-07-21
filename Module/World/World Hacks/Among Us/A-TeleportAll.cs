using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.World.World_Hacks.Among_Us
{
    internal class A_TeleportAll : BaseModule
    {
        public A_TeleportAll() : base("Teleport All", "Forcefully Teleports Players", Main.Instance.Amongusbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Game Started", false, false);
                UW.udonsend("SyncVotedOut", EventTarget.Everyone);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
