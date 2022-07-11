using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnhollowerBaseLib;
using MelonLoader;
using UnityEngine;
using System;
using Trinity.SDK.Photon;
using Newtonsoft.Json;
using VRC;

namespace Trinity.Module.Safety.Photon
{
    class PhotonProtection : BaseModule, OnEventEvent
    {
        public static bool AntiEvents;
        internal static List<string> E1Data = new List<string>();
        internal static List<string> E1BlockedPlayers = new List<string>();
        public PhotonProtection() : base("Anti Photon", "Trys to block photon exploits", Main.Instance.Networkbutton, null, true, false)
        {
            PhotonProtection.AntiEvents = !PhotonProtection.AntiEvents; 
        }

        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Photon Enabled</color>");
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Photon Disabled</color>");
            Main.Instance.OnEventEvents.Remove(this);
        }


        public bool OnEvent(EventData eventData)
        {
            ParameterDictionary parameters = eventData.Parameters;
            int Sender = eventData.sender;
            var player = PlayerManager.field_Private_Static_PlayerManager_0.GetPlayer(Sender);
            string EventSender = player != null ? player.prop_APIUser_0.displayName : "VRC Server";
            bool paramsCheck = player._playerNet.field_Private_Int16_0 <= 0 && 1000 / (int)player._playerNet.field_Private_Byte_0 <= 0;
            byte code = eventData.Code;

            switch (code)
            {
                case 1:
                    {
                        if (Config.AntiE1)
                        {
                            byte[] EventData = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
                            var payload = PU.Params2JSON(eventData.Parameters);
                            if (E1BlockedPlayers.Contains(player.field_Private_APIUser_0.id))
                                return false;

                            if (E1Data.Contains(payload) || BitConverter.ToInt32(EventData, 0) != eventData.sender || EventData.Length <= 8)
                            {
                                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, $"[Event Safety] \nEvent: {eventData.Code} \nEvent Sender: {EventSender} \nValid: False", false, false);
                                MenuUI.Log($"Safety: <color=red>Event 1 From {EventSender} | Valid: False</color>");
                                E1BlockedPlayers.Add(player.field_Private_APIUser_0.id);
                                PU.Delay(27f, delegate
                                {
                                    MenuUI.Log($"Safety: <color=green>Event 1 From {EventSender} | Valid: True</color>");
                                    Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, $"[Event Safety] \nEvent: {eventData.Code} \nEvent Sender: {EventSender} \nValid: True", false, false);
                                    E1BlockedPlayers.Remove(player.field_Private_APIUser_0.id);
                                });
                                return false;
                            }
                            if (!E1Data.Contains(payload))
                                E1Data.Add(payload);
                        }
                        break;
                    }
                case 6:
                    {
                        if (paramsCheck && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                        {
                            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, $"[Event Safety] \nEvent: {eventData.Code} \nEvent Sender: {EventSender} \nValid: False \nBot: True", false, false);
                            MenuUI.Log($"Safety: <color=red> {eventData.Code} From {EventSender} | Valid: False | Bot: True</color>");
                            return false;
                        }
                        if (player.transform.position == Vector3.zero && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                            return false;
                        return false;
                    }
                case 9:
                    {
                        if (paramsCheck && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                        {
                            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, $"[Event Safety] \nEvent: {eventData.Code} \nEvent Sender: {EventSender} \nValid: False \nBot: True", false, false);
                            MenuUI.Log($"Safety: <color=red> {eventData.Code} From {EventSender} | Valid: False | Bot: True</color>");
                            return false;
                        }
                        if (player.transform.position == Vector3.zero && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                            return false;
                        return false;
                    }
                case 209:
                    {
                        if (paramsCheck && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                        {
                            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, $"[Event Safety] \nEvent: {eventData.Code} \nEvent Sender: {EventSender} \nValid: False \nBot: True", false, false);
                            MenuUI.Log($"Safety: <color=red> {eventData.Code} From {EventSender} | Valid: False | Bot: True</color>");
                            return false;
                        }
                        if (player.transform.position == Vector3.zero && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                            return false;
                        return false;
                    }
                case 210:
                    {
                        if (paramsCheck && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                        {
                            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, $"[Event Safety] \nEvent: {eventData.Code} \nEvent Sender: {EventSender} \nValid: False \nBot: True", false, false);
                            MenuUI.Log($"Safety: <color=red> {eventData.Code} From {EventSender} | Valid: False | Bot: True</color>");
                            return false;
                        }
                        if (player.transform.position == Vector3.zero && player != VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_Player_0)
                            return false;
                        return false;
                    }
                default:
                    return true;
            }
            return true;
        }
    }
}


