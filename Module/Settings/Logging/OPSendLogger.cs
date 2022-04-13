using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.Photon;
using Newtonsoft.Json;
using Photon.Realtime;

namespace Trinity.Module.Settings.Logging
{
    class OPSendLogger : BaseModule, OnSendOPEvent
    {
        public OPSendLogger() : base("OPSendLogger", "Logs Photon Events Send by you", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnSendOPEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnSendOPEvents.Remove(this);
        }

        public bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions)
        {
            LogHandler.Log(LogHandler.Colors.Blue, $"[OPSendLog] {opCode} {JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(parameters), Formatting.Indented)}", false, false);
            LogHandler.LogDebug($"[OPSend Logger] Your Event: {opCode}");
            return true;
        }
    }
}
