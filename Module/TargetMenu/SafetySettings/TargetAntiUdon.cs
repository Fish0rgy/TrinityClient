using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Trinity.Module.TargetMenu.SafetySettings
{
    class TargetAntiUdon : BaseModule, OnUdonEvent
    {
        public static string targetid;
        public TargetAntiUdon() : base("Anti Udon", "Targeted Anti Udon", Main.Instance.SafetyTargetButton, QMButtonIcons.CreateSpriteFromBase64(Serpent.rocket), true, true) { }

        public override void OnEnable()
        {
            targetid = PlayerWrapper.SelectedVRCPlayer().field_Private_APIUser_0.id;
            Main.Instance.OnUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            targetid = null;
            Main.Instance.OnUdonEvents.Remove(this);
        }

        public bool OnUdon(string __0, VRC.Player __1)
        {
            if (__1.field_Private_APIUser_0.id == PlayerWrapper.LocalPlayer.prop_APIUser_0.id)
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
