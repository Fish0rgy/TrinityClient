using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK.Photon;
using ExitGames.Client.Photon;
using Trinity.SDK;

namespace Trinity.Module.TargetMenu
{
    class VoiceIM : BaseModule, OnEventEvent
    {
        public VoiceIM() : base("Voice IM", "Logs Photon Events", Main.Instance.Targetbutton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("VOICE: <color=green>Starting Imitation On Target</color>");
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("VOICE: <color=red>Stopped Imitating Target</color>");
            Main.Instance.OnEventEvents.Remove(this);
        }

        public bool OnEvent(EventData eventData)
        {
            while (true && eventData.Code == 1)
            {
                PhotonExtensions.OpRaiseEvent(1, eventData.CustomData, new Photon.Realtime.RaiseEventOptions() { field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others, field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache }, default);
                return true;
            }
            return true;
        }
    }
}