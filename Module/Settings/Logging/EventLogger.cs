using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.Photon;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Trinity.Module.Settings.Logging
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
            try
            {              
                int Sender = eventData.sender; VRC.Player player = PlayerWrapper.GetPlayerByActorID(Sender);
                string LocalPlayer = player != null ? player.prop_APIUser_0.displayName : "VRC Server";
                NonAllocDictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;

                if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8 || eventData.Code == 35) return true;             
                foreach (Il2CppSystem.Collections.Generic.KeyValuePair<byte, Il2CppSystem.Object> s in parameters)
                {
                    string Payload = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(s.value), Formatting.Indented);
                    LogHandler.Log(LogHandler.Colors.Green, $"{Environment.NewLine}[EventLogger] Event Code -> {eventData.Code}{Environment.NewLine}[EventLogger] Event Was Sent By -> {LocalPlayer}" + $"{Environment.NewLine}[EventLogger] Payload -> {Payload}", false, false);
                    LogHandler.LogDebug($"[EventLogger] -> {LocalPlayer} Sent Event {eventData.Code}");
                }

            } catch (UnhollowerBaseLib.Il2CppException ERROR) { LogHandler.Log(LogHandler.Colors.Yellow, ERROR.StackTrace, false, false); }
            
            return true;
        }
    }
}