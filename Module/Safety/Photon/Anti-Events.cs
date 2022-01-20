//using Area51.Events;
//using Area51.SDK;
//using ExitGames.Client.Photon;


//namespace Area51.Module.Safety.Photon
//{
//    class PhotonProtection : BaseModule, OnEventEvent
//    {
//        public static bool AntiEvents;
//        public PhotonProtection() : base("Anti-Events", "Trys to block photon exploits", Main.Instance.networkbutton, null, true, true)
//        {
//            PhotonProtection.AntiEvents = !PhotonProtection.AntiEvents;
//            this.name = $"Anti-Events\n{(PhotonProtection.AntiEvents ? "Enabled" : "Disabled")}";
//        }

//        public override void OnEnable()
//        {
//            Main.Instance.onEventEvents.Add(this);
//        }

//        public override void OnDisable()
//        {
//            Main.Instance.onEventEvents.Remove(this);
//        }

//        public bool OnEvent(EventData eventData)
//        {
//            var LocalPlayer = PlayerWrapper.GetActorNumber(PlayerWrapper.LocalPlayer);
//            byte code = eventData.Code;
//            switch (code)
//            {
//                case 6:
//                    if (eventData.Parameters[245].ToString() != null || eventData.Parameters[245].ToString().Contains("zIvDgD/AAAAAAkAAAAYAFJlbG9hZEF2YXRhck5ldHdvcmtlZFJQQwAAAAAAAAAC"))
//                    {
//                        return false;
//                    }
//                    else if (eventData.Parameters[245].ToString().Length > 375)
//                    {
//                        return false;
//                    }
//                    else if (eventData.Parameters[245].ToString().Contains(null))
//                    {
//                        return false;
//                    }
//                    return false;
//                case 9:
//                    if (eventData.Parameters[245].ToString() != null || eventData.Parameters[245].ToString() == "QQ0DAAAAAAA=")
//                    {
//                        return false;
//                    }
//                    else if (eventData.Parameters[245].ToString().Length > 150)
//                    {
//                        return false;
//                    }
//                    else if (eventData.Parameters[245].ToString().Contains(null))
//                    {
//                        return false;
//                    }
//                    return false;
//                default:
//                    if (eventData.sender != LocalPlayer)
//                    {
//                        return false;
//                    }
//                    return true;
//            }
//        }
//    }
//}




using Area51.Events;
using Area51.SDK;
using ExitGames.Client.Photon;
using System.Collections.Generic;

namespace Area51.Module.Safety.Photon
{
    class PhotonProtection : BaseModule, OnEventEvent
    {
        public static bool AntiEvents;
        public PhotonProtection() : base("Anti-Events", "Trys to block photon exploits", Main.Instance.Networkbutton, null, true, true)
        {
            PhotonProtection.AntiEvents = !PhotonProtection.AntiEvents;
            this.name = $"Anti-Events\n{(PhotonProtection.AntiEvents ? "Enabled" : "Disabled")}";
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
            Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;
            byte code = eventData.Code;

            switch (code)
            {
                case 6:                  
                    return false;
                case 9:              
                    return false;
                default:
                    if (parameters == null)
                    {
                        return false;
                    }
                    else if (eventData.sender != LocalPlayer)
                    {
                        return true;
                    }
                    return true;
            }
        }
    }
}


