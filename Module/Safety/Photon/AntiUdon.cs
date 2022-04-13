using Trinity.Events;
using Trinity.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Safety
{
    class AntiUdon : BaseModule, OnUdonEvent
    {
        public AntiUdon() : base("Anti Udon", "Anti Udon Events", Main.Instance.Networkbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnUdonEvents.Remove(this);
        }
        public bool OnUdon(string __0, VRC.Player __1)
        {
            if(__1.field_Private_APIUser_0.id == PlayerWrapper.LocalPlayer.prop_APIUser_0.id)
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
