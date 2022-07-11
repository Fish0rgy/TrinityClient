using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using VRC.SDKBase;

namespace Trinity.Module.TargetMenu.SafetySettings
{
    class TargetAntiRPC : BaseModule, OnRPCEvent
    {
        public static VRCPlayer target;
        public TargetAntiRPC() : base("Anti RPC", "Targeted Anti RPC", Main.Instance.SafetyTargetButton, QMButtonIcons.LoadSpriteFromFile(Serpent.rocketPath), true, true) { }

        public override void OnEnable()
        {
            target = PU.SelectedVRCPlayer().prop_VRCPlayer_0;
            MenuUI.Log($"SAFETY: <color=green>Target Anti RPC On</color>");
            Main.Instance.OnRPCEvents.Add(this);
        }
        public override void OnDisable()
        {
            target = null;
            MenuUI.Log($"SAFETY: <color=red>Target Anti RPC Off</color>");
            Main.Instance.OnRPCEvents.Remove(this);
        }
        public bool OnRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            if(sender == PU.GetPlayer())
            {
                return false;
            }
            else
            {
                if(target != null)
                {
                    if(sender == target)
                    {
                        return false; 
                        LogHandler.Log(LogHandler.Colors.Red, $"[Anti RPC] Blocked {vrcEvent.Name} From {sender.name}", false, false);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }
    }
}
