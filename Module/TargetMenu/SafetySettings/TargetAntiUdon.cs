using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Networking;

namespace Trinity.Module.TargetMenu.SafetySettings
{
    class TargetAntiUdon : BaseModule, OnUdonEvent
    {
        public static string targetid;
        public TargetAntiUdon() : base("Anti Udon", "Targeted Anti Udon", Main.Instance.SafetyTargetButton, QMButtonIcons.LoadSpriteFromFile(Serpent.rocketPath), true, true) { }

        public override void OnEnable()
        {
            targetid = PU.SelectedVRCPlayer().field_Private_APIUser_0.id;
            MenuUI.Log($"SAFETY: <color=green>Target Anti Udon On</color>");
            Main.Instance.OnUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            targetid = null;
            MenuUI.Log($"SAFETY: <color=red>Target Anti Udon Off</color>");
            Main.Instance.OnUdonEvents.Remove(this);
        }

        public bool OnUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            if (__1.field_Private_APIUser_0.id == PU.GetPlayer().prop_APIUser_0.id)
            {
                return true;
            }
            else
            {
                if(__1.field_Private_APIUser_0.id != targetid)
                {
                    return true;
                }
                else if(targetid == null)
                {
                    return true;
                }
                else
                {
                    return false;
                    LogHandler.Log(LogHandler.Colors.Red, $"[Anti Udon] Blocked {__0} From {__1.name}", false, false);
                }
            }
        }
    }
}
