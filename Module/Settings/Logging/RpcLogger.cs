using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using VRC.SDKBase;

namespace Trinity.Module.Settings.Logging
{
    class RpcLogger : BaseModule, OnRPCEvent
    {
        public RpcLogger() : base("RPCLogger", "Logs RPCs", Main.Instance.SettingsButtonLoggging, null, true, true) { }
      
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
            var output = PU.LogRPC(sender, vrcEvent, vrcBroadcastType);
            LogHandler.Log(LogHandler.Colors.Cyan, output, false, false);
            LogHandler.LogDebug($"{output}");
            return true;
        }      
    }
}
