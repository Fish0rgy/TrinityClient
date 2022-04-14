using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.TargetMenu.SafetySettings
{
    class TargetAntiPhoton : BaseModule, OnEventEvent
    {
        public static int target;
        public static string targetname;
        public TargetAntiPhoton() : base("Anti RPC", "Targeted Anti RPC", Main.Instance.SafetyTargetButton, QMButtonIcons.LoadSpriteFromFile(Serpent.rocketPath), true, true) { }

        public override void OnEnable()
        {
            target = PU.GetPhotonID(PU.SelectedVRCPlayer());
            targetname = PU.SelectedVRCPlayer().name;
            Main.Instance.OnEventEvents.Add(this);
        }
        public override void OnDisable()
        {
            targetname = null;
            target = 0;
            Main.Instance.OnEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            int id = eventData.Sender;
            var LocalPlayer = PU.GetActorNumber(PU.GetPlayer());
            byte code = eventData.Code; 
            switch (code)
            {
                case 6:
                    {
                        if(id != target)
                        {
                            return true;
                        }
                        else if(target == null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                            LogHandler.Log(LogHandler.Colors.Red, $"[Anti Photon] Blocked Event 6 From {targetname}");
                        }
                        return false;
                    } 
                default:
                    return true;
            }
        }
    }
}
