using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Events
{
    public interface OnPhotonEvent
    {
        bool OnEventPatch(LoadBalancingClient loadBalancingClient, EventData eventData);
    }
}
