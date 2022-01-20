using Area51.Events;
using ExitGames.Client.Photon;

namespace Area51.Module.Safety
{
    class Anti209 : BaseModule, OnEventEvent
    {
        public Anti209() : base("Event209", "Anti for the Event209 Exploit", Main.Instance.Networkbutton, null, true, true)
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
            if (eventData.Code == 209)
            {
                return false;
            }

            return true;
        }
    }
}
