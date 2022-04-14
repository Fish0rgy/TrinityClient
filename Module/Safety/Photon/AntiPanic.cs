using Trinity.Utilities;
using Area51.Events;
using Area51.SDK;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.Safety.Photon
{
    class AntiPanic : BaseModule, OnEventEvent
    { 
        public AntiPanic() : base("Anti-Panic", "Trys to block photon exploits", Main.Instance.networkbutton, null, true, true)
        { 
        }

        public override void OnEnable()
        {
            Main.Instance.onEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.onEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            byte code = eventData.Code;
            switch (code)
            {
                case 6:
                    return false;
                case 9: 
                    return false;
            }
            return true;
        }
    }
}
