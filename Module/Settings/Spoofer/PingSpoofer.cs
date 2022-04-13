using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.Photon;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using UnhollowerBaseLib;
using UnityEngine;

namespace Trinity.Module.Settings.Spoofer
{
    internal class PingSpoofer : BaseModule, OnEventEvent
    {

        byte[] ping = BitConverter.GetBytes(1337);
        public PingSpoofer() : base("PingSpoofer", "Spoofes Ping to 1337", Main.Instance.SettingsButtonspoofer, null, true, false)
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
            while(true)
            {
                byte code = eventData.Code;
                switch (code)
                {
                    case 7:
                        byte[] viewIDOut = BitConverter.GetBytes(int.Parse($"{PlayerWrapper.LocalPlayer.GetActorNumber()}00001"));
                        byte[] movementData = eventData.customData.Cast<Il2CppStructArray<byte>>();
                        Buffer.BlockCopy(viewIDOut, 0, movementData, 0, 4);
                        Buffer.BlockCopy(ping, 0, movementData, 68, 2);
                        PhotonExtensions.OpRaiseEvent(9, movementData, new Photon.Realtime.RaiseEventOptions { field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others, field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache }, SendOptions.SendUnreliable);
                        return true;
                }
                return false;

            }
        }
    }
}
