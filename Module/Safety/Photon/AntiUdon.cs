using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.Networking;

namespace Trinity.Module.Safety
{
    class AntiUdon : BaseModule, OnUdonEvent
    {
        public AntiUdon() : base("Anti Udon", "Anti Udon Events", Main.Instance.Networkbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Udon Enabled</color>");
            Main.Instance.OnUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Udon Disabled</color>");
            Main.Instance.OnUdonEvents.Remove(this);
        }
        public bool OnUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            if(__1.field_Private_APIUser_0.id == PU.GetPlayer().prop_APIUser_0.id)
            {
                return true;
            }
            else
            { 
                return false; 
            } 
        }
    }
}
