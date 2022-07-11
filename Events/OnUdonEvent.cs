using VRC;
using VRC.Networking;

namespace Trinity.Events
{
    public interface OnUdonEvent
    {
        bool OnUdon(string __0, Player __1, UdonSync __instance);
    }
}
