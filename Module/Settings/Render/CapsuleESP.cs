using Trinity.Events;
using Trinity.SDK;
using Photon.Realtime;
using UnityEngine;

namespace Trinity.Module.Settings.Render
{
    class CapsuleEsp : BaseModule, OnPlayerJoinEvent
    {
        public CapsuleEsp() : base("Player ESP", "See Players n shit", Main.Instance.SettingsButtonrender, null, true, false) { }

        public override void OnEnable()
        {
            for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
            {
                CapsuleEsp.HighlightPlayer(PlayerWrapper.GetAllPlayers()[i], true);
            }
            Main.Instance.OnPlayerJoinEvents.Add(this);
        }

        public override void OnDisable()
        {
            for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
            {
                CapsuleEsp.HighlightPlayer(PlayerWrapper.GetAllPlayers()[i], false);
            }
            Main.Instance.OnPlayerJoinEvents.Remove(this);
        }

        public static void HighlightPlayer(VRC.Player player, bool state)
        {
            Renderer renderer;
            if (player == null)
            {
                renderer = null;
            }
            else
            {
                Transform transform = player.transform.Find("SelectRegion");
                renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
            }

            Renderer renderer2 = renderer;
            if (renderer2)
            {
                HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, state);
              
            }
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            HighlightPlayer(player, true);
        }

        public void OnPlayerEnteredRoom(Photon.Realtime.Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}