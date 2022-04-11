using Area51.Events;
using Area51.SDK;
using ExitGames.Client.Photon;
using System.Collections.Generic;

namespace Area51.Module.Safety.Photon
{
    class PhotonProtection : BaseModule, OnEventEvent
    {
        public static bool AntiEvents;
        public PhotonProtection() : base("Anti Photon", "Trys to block photon exploits", Main.Instance.Networkbutton, null, true, false)
        {
            PhotonProtection.AntiEvents = !PhotonProtection.AntiEvents; 
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
            var LocalPlayer = PlayerWrapper.GetActorNumber(PlayerWrapper.LocalPlayer); 
            byte code = eventData.Code;

            switch (code)
            {
                case 6:                  
                    return false;
                default:
                    return true;
            }
        }
    }
}


