using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK.Photon;
using Photon.Realtime;
using UnhollowerBaseLib;
using UnityEngine;
using Trinity.SDK;
using System;

namespace Trinity.Module.Settings.Spoofer
{
    internal class FPSSpoofer : BaseModule 
    { 
        public FPSSpoofer() : base("FPS Spoof", "Spoofes FPS to 51", Main.Instance.SettingsButtonspoofer, null, true)
        {
             
        }
        public override void OnEnable()
        {
            Config.SpoofFps = true;
            Config.FPSSpoof = 0;
            Config.FPSSpoof1 = -12340;
            Config.FPSSpoof2 = 20232;
            Config.FPSSpoof3 = -10;
            Config.FPSSpoof4 = 300;
        }
        public override void OnDisable()
        {
            Config.SpoofFps = false;
        }
    }
}
