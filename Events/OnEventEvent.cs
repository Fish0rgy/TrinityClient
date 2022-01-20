using ExitGames.Client.Photon;

namespace Area51.Events
{
    public interface OnEventEvent
    {
        bool OnEvent(EventData eventData);
    }
}
