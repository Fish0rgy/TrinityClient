using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using VRC.SDKBase;

namespace Trinity.Module.Safety.Photon
{
    class AntiRPC : BaseModule, OnRPCEvent
    {
        public AntiRPC() : base("Anti RPC", "Anti's All RPCs", Main.Instance.Networkbutton, null, true, true) { }

        public override void OnEnable()
        {
            Main.Instance.OnRPCEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnRPCEvents.Remove(this);
        }

        public bool OnRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        { 
            return false;
        }
    }
}
