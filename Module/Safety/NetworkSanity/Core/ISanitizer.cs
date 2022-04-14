using Trinity.Utilities;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Area51.Module.Safety.Photon.NetworkSanity.Core
{
    internal interface ISanitizer
    {
        // check event and reject if necessary
        bool OnPhotonEvent(LoadBalancingClient loadBalancingClient, EventData eventData);
        // check if currently ratelimited to make sure vrchat doesn't log those events in case debug logging is enabled
        bool VRCNetworkingClientOnPhotonEvent(EventData eventData);
    }
}
