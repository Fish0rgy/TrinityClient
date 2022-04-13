using ExitGames.Client.Photon;

namespace Trinity.Events
{
    public interface OnEventEvent
    {
        bool OnEvent(EventData eventData);
    }
}
