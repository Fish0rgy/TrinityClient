using Area51.Events;
using Area51.SDK;
using ExitGames.Client.Photon;
using UnhollowerBaseLib;

namespace Area51.Module.Safety
{
    class Anti210 : BaseModule, OnEventEvent
    {
        public Anti210() : base("Event210", "Anti for the Event210 Exploit", Main.Instance.Networkbutton, null, true, true)
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
            if (eventData.Code == 210)
            {
                VRC.Player player = PlayerWrapper.GetPlayerWithPlayerID(eventData.sender);
                if (player == null)
                {
                    Logg.Log(Logg.Colors.Red,"Blocked Invalid 210", false,false);
                    return false;
                }
                Il2CppStructArray<int> il2CppStructArray = eventData.Parameters[245].TryCast<Il2CppStructArray<int>>();
                if (il2CppStructArray[1] != player.prop_VRCPlayerApi_0.playerId)
                {
                    Logg.Log(Logg.Colors.Red,"Blocked Invalid 210", false,false);
                    return false;
                }
            }

            return true;
        }

    }
}
