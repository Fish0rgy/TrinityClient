using Area51.Events;
using Area51.SDK;
using Area51.SDK.Photon;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using System;


namespace Area51.Module.Settings.Logging
{
    class EventLogger : BaseModule, OnEventEvent
    {
        public EventLogger() : base("EventLogger", "Logs Photon Events", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            byte code = eventData.Code;
            string Payload = "";
            int sender = eventData.sender; VRC.Player player = PlayerWrapper.GetPlayerByActorID(sender); Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;
            string LocalPlayer = player != null ? player.prop_APIUser_0.displayName : "NiggaRinBot";
            if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8) return true;
            if (parameters != null)
                Payload = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(parameters), Formatting.Indented);
            Logg.Log(Logg.Colors.Cyan, $"{Environment.NewLine}[Tupper] Event Code -> {code}{Environment.NewLine}[Tupper] Event Was Sent By -> {LocalPlayer}{Environment.NewLine}[Tupper] Payload -> {Payload}", false, false);
            Logg.LogDebug($"[Tupper] -> {LocalPlayer} Sent Event {code}");

            return true;
        }
    }
}