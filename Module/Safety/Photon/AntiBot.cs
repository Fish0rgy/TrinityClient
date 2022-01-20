using Area51.Events;
using Area51.SDK;
using ExitGames.Client.Photon;

namespace Area51.Module.Safety
{
    class AntiBot : BaseModule, OnEventEvent
    {
        public AntiBot() : base("Anti PhotonBot", "Detects PhotonBots In Lobby And Blocks There Events", Main.Instance.Networkbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            VRC.Player player = PlayerWrapper.GetPlayerByActorID(eventData.Sender);
            bool EventCodes = eventData.Code == 6 || eventData.Code == 9 || eventData.Code == 209 || eventData.Code == 210;
            if (EventCodes)
            {
                if (player != null)
                {
                    if (player.IsBot())
                    {  
                      return false;
                    }
                    if (player == PlayerWrapper.LocalPlayer)
                        return true;
                }
               
            }
            return true;
        }
    }
}
