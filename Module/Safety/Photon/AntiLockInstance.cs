using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using Trinity.SDK;
using UnityEngine;

namespace Trinity.Module.Safety.Photon
{
    internal class AntiLockInstance : BaseModule, OnUpdateEvent
    {
        public AntiLockInstance() : base("Anti Lock Instance", "Anti Lock Instanc", Main.Instance.Networkbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Lock Instance Enabled</color>");
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Lock Instance Disabled</color>");
            Main.Instance.OnUpdateEvents.Remove(this);
        }

        public void OnUpdate()
        {
            try
            {
                VRC_EventLog.field_Internal_Static_VRC_EventLog_0.field_Internal_EventReplicator_0.field_Private_Boolean_0 = true;
            }
            catch
            {

            } 
        }
    }
}
