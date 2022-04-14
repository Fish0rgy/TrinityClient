using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Settings.Logging
{
    class ModerationLogger : BaseModule, OnEventEvent
    {
        public static Dictionary<int, Dictionary<byte, object>> Moderations = new Dictionary<int, Dictionary<byte, object>>();
        public ModerationLogger() : base("Moderation Logger", "Logs Photons Moderation Events", Main.Instance.SettingsButtonLoggging, null, true, true)
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
                int Sender = eventData.sender; VRC.Player player = PU.GetPlayerByActorID(Sender);
                string LocalPlayer = player != null ? player.prop_APIUser_0.displayName : "VRC Server";
                NonAllocDictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;
                if (eventData.Code != 1)
                {
                    if (eventData.Code == 33)
                    {
                        foreach (Il2CppSystem.Collections.Generic.KeyValuePair<byte, Il2CppSystem.Object> s in parameters)
                        {
                            Console.WriteLine(parameters.keys.ToString());
                           
                            if (parameters.ContainsKey(0))
                            {
                                if (parameters.ContainsKey(1))
                                {
                                    if (parameters.ContainsKey(10))
                                    {
                                        if (parameters.ContainsKey(11))
                                        {
                                            bool ReverseMod = (bool)parameters.ContainsKey(1);
                                            bool Blocked = (bool)parameters.ContainsKey(10);
                                            bool Muted = (bool)parameters.ContainsKey(11);
                                            if (Moderations.ContainsKey(1))
                                            {
                                                bool UnBlock = (bool)Moderations[Convert.ToInt32(ReverseMod)][10];
                                                bool UnMuted = (bool)Moderations[Convert.ToInt32(ReverseMod)][11];
                                                if (Blocked && !UnBlock)
                                                {
                                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Blocked You", false, false);
                                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Blocked You");
                                                }
                                                if (UnBlock && !Blocked)
                                                {
                                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} UnBlocked You", false, false);
                                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} UnBlocked You");
                                                }
                                                if (Muted && !UnMuted)
                                                {
                                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Muted You", false, false);
                                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Muted You");
                                                }
                                                if (UnMuted && !Muted)
                                                {
                                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} UnMuted You", false, false);
                                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} UnMuted You");
                                                }
                                            }
                                            else
                                            {
                                                if (Blocked)
                                                {
                                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Blocked You", false, false);
                                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Blocked You");
                                                }
                                                if (Muted)
                                                {
                                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Muted You", false, false);
                                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Muted You");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (UnhollowerBaseLib.Il2CppException ERROR) { LogHandler.Log(LogHandler.Colors.Yellow, ERROR.StackTrace, false, false); }

            return true;
        }
    }
} 