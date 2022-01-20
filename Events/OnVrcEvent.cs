using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Events
{
    public interface OnVrcEvent
    {
        bool VRCNetworkingClientOnPhotonEvent(EventData eventData);
    }
}
