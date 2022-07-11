using Trinity.Utilities;
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
            MenuUI.Log("LOGGING: <color=green>Event Logger On</color>");
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("LOGGING: <color=red>Event Logger Off</color>");
            Main.Instance.OnEventEvents.Remove(this);
        }


        public bool OnEvent(EventData eventData)
        {
            try
            {
                //int Sender = eventData.sender; VRC.Player player = PU.GetPlayerByActorID(Sender);
                //string LocalPlayer = player != null ? player.prop_APIUser_0.displayName : "VRC Server";
                //NonAllocDictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;

                //if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8 || eventData.Code == 35) return true;             
                //foreach (Il2CppSystem.Collections.Generic.KeyValuePair<byte, Il2CppSystem.Object> s in parameters)
                //{
                //    string Payload = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(s.value), Formatting.Indented);
                //    LogHandler.Log(LogHandler.Colors.Green, $"{Environment.NewLine}[EventLogger] Event Code -> {eventData.Code}{Environment.NewLine}[EventLogger] Event Was Sent By -> {LocalPlayer}" + $"{Environment.NewLine}[EventLogger] Payload -> {Payload}", false, false);
                //    LogHandler.LogDebug($"[EventLogger] -> {LocalPlayer} Sent Event {eventData.Code}");
                //}
                VRC.Player playerByActorID = PU.GetPlayerByActorID(eventData.sender);
                string Sender = ((playerByActorID != null) ? playerByActorID.prop_APIUser_0.displayName : "VRC Server");
                NonAllocDictionary<byte, Il2CppSystem.Object> nonAllocDictionary = eventData.Parameters;

                if (eventData.Code == 7 || eventData.Code == 1 || eventData.Code == 8 || eventData.Code == 35)
                    return true;
                NonAllocDictionary<byte, Il2CppSystem.Object>.PairIterator enumerator = nonAllocDictionary.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Il2CppSystem.Collections.Generic.KeyValuePair<byte, Il2CppSystem.Object> current = enumerator.Current;
                    string payload = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(current.value), Formatting.Indented);
                    LogHandler.Log(LogHandler.Colors.Green, $"[EventLogger] Event Code -> {eventData.Code}{Environment.NewLine}[EventLogger] Event Was Sent By -> {Sender} {Environment.NewLine}[EventLogger] Payload -> {payload}",false,false);
                    LogHandler.LogDebug($"[EventLogger] -> {Sender} Sent Event {eventData.Code}");
                }

            } catch (UnhollowerBaseLib.Il2CppException ERROR) { LogHandler.Log(LogHandler.Colors.Yellow, ERROR.StackTrace, false, false); }
            
            return true;
        }
    }
}