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
            byte code = eventData.Code;

            switch (code)
            {
                 
                case 6:
                    {
                        return false;
                    }
                case 9:
                    {
                        return false;
                    }
                case 209:
                    {
                        return false;
                    }
                case 210:
                    {
                        return false;
                    }
                default:
                    return true;
            }
            return true;
        }
    }
}


