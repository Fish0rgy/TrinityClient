using Trinity.Events;
using Trinity.SDK.Photon;
using Photon.Realtime;
using UnhollowerBaseLib;

namespace Trinity.Module.Settings.Spoofer
{
    internal class FPSSpoofer : BaseModule, OnSendOPEvent
    {
        byte fps;
        public FPSSpoofer() : base("FPS Spoof", "Spoofes FPS to 51", Main.Instance.SettingsButtonspoofer, null, true)
        {
            fps = 0x33; //should be 51 convert to hex if i my mem is right lol
        }

        public bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions)
        {
            if (opCode == 7)
            {
                byte[] movementData = parameters.Cast<Il2CppStructArray<byte>>();
                movementData[71] = fps;
                parameters = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(movementData);
            }

            return true;
        }
    }
}
