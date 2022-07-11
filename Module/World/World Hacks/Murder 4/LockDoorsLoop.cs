using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    internal class LockDoorsLoop : BaseModule, OnUpdateEvent
    {
        public LockDoorsLoop() : base("Lock Doors Loop", "", Main.Instance.Murderbutton, null, true, false)
        {
        }
        public override void OnEnable()
        {
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "No one can open any door", false, false);
            Main.Instance.OnUpdateEvents.Add(this);
        }
        public override void OnDisable()
        {
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, "No one can open any door", false, false);
            Main.Instance.OnUpdateEvents.Remove(this);
        }

        public void OnUpdate()
        {
        }
    }
}
