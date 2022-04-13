using ExitGames.Client.Photon;
using Il2CppSystem.Collections.Generic;

namespace Trinity.Events
{
    public interface OnPhotonPeerEvent
    {
        bool OnPhotonPeer(byte __0, Dictionary<byte, Il2CppSystem.Object> __1, SendOptions __2);
    }
}
