using Area51.Events;
using Area51.SDK.Photon;
using ExitGames.Client.Photon;

namespace Area51.Module.TargetMenu
{
    class VoiceIM : BaseModule, OnEventEvent
    {
        public VoiceIM() : base("Voice IM", "Logs Photon Events", Main.Instance.Targetbutton, null, true, true)
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
          while(true && eventData.Code == 1)
            {
                PhotonExtensions.OpRaiseEvent(1, eventData.CustomData, new Photon.Realtime.RaiseEventOptions() { field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others, field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache }, default);
                return true;
            }
            return true;
        }
    }
}