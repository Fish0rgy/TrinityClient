using Trinity.Utilities;
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
    internal class PingSpoofer : BaseModule
    {
         
        public PingSpoofer() : base("Ping Spoof", "Spoofes Ping to from -1000 to 1000", Main.Instance.SettingsButtonspoofer, null, true, true) { }

        public override void OnEnable()
        {
            Config.SpoofPing = true;
            Config.PingSpoof = 0; 
        }
        public override void OnDisable()
        {
            Config.SpoofPing = false;
        }
    }
}
