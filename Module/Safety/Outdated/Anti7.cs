using Trinity.Utilities;
using Trinity.Events;
using ExitGames.Client.Photon;
using UnhollowerBaseLib;

namespace Trinity.Module.Safety
{
    class Anti7 : BaseModule, OnEventEvent
    {
        public Anti7() : base("Anti7", "Prevents Menu Exploit", Main.Instance.Networkbutton, null, true, true)
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
            if (eventData.Code == 7)
            {
                var bytes = eventData.CustomData.Cast<Il2CppArrayBase<byte>>();
                if (bytes.Length > 300)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
