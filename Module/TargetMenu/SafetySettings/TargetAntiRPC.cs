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
        public TargetAntiRPC() : base("Anti RPC", "Targeted Anti RPC", Main.Instance.SafetyTargetButton, QMButtonIcons.CreateSpriteFromBase64(Serpent.rocket), true, true) { }

        public override void OnEnable()
        {
            target = PlayerWrapper.SelectedVRCPlayer().prop_VRCPlayer_0;
            Main.Instance.OnRPCEvents.Add(this);
        }
        public override void OnDisable()
        {
            target = null;
            Main.Instance.OnRPCEvents.Remove(this);
        }
        public bool OnRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            if(sender == PlayerWrapper.LocalPlayer)
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
