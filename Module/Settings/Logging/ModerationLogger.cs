using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using ExitGames.Client.Photon;
using Il2CppSystem; 
using UnhollowerBaseLib;

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
            MenuUI.Log("LOGGING: <color=green>Moderation Logger On</color>");
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("LOGGING: <color=red>Moderation Logger Off</color>");
            Main.Instance.OnEventEvents.Remove(this);
        }


        public bool OnEvent(EventData eventData)
        {
            try
            {
                int Sender = eventData.sender; VRC.Player player = PU.GetPlayerByActorID(Sender);
                string LocalPlayer = player != null ? player.prop_APIUser_0.displayName : "VRC Server";
                Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters[eventData.CustomDataKey].Cast<Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object>>();
                byte b = parameters[0].Unbox<byte>();
                if (b != 2)
                {
                    if (b != 8)
                    {
                        if (b == 21)
                        {
                            if (parameters.Count == 4)
                            {
                                bool Blocked = parameters[10].Unbox<bool>();
                                bool isMuted = parameters[11].Unbox<bool>();
                                //Solution tbHL1exsXB9TuqoNijN = LSybw88J0X3ZGflYdO7.X1J8xhXukb(dictionary[1].Unbox<int>());
                                if (Moderations.ContainsKey(1))
                                if (Blocked)
                                {
                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Blocked You", false, false);
                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Blocked You");
                                }
                                if (isMuted)
                                {
                                    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Muted You", false, false);
                                    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Muted You");
                                }
                            }
                            else if (parameters.Count == 3)
                            {
                                Il2CppStructArray<int> CppArray = parameters[10].Cast<Il2CppStructArray<int>>();
                                Il2CppStructArray<int> CppArray2 = parameters[11].Cast<Il2CppStructArray<int>>();
                                for (int i = 0; i < CppArray.Count; i++)
                                {
                                    Solution DataType = PU.Il2CppConverter((CppArray)[i]);
                                    if (DataType != null)
                                    {
                                        PU.BlockStateChanged(DataType, true);
                                    }
                                }
                                for (int j = 0; j < CppArray2.Count; j++)
                                {
                                    Solution MuteType = PU.Il2CppConverter(CppArray2[j]);
                                    if (MuteType != null)
                                    {
                                        PU.MuteStateChanged(MuteType, true);
                                    }
                                }
                            }
                            else
                            {
                                //if (Blocked)
                                //{
                                //    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Blocked You", false, false);
                                //    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Blocked You");
                                //}
                                //if (Muted)
                                //{
                                //    LogHandler.Log(LogHandler.Colors.Green, $"[Moderation] -> {LocalPlayer} Muted You", false, false);
                                //    LogHandler.LogDebug($"[Moderation] -> {LocalPlayer} Muted You");
                                //}
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