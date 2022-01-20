using Area51.Events;
using Area51.SDK;
using Photon.Realtime;
using System;

namespace Area51.Module.Settings.Render
{
    internal class CustomNameplates : BaseModule, OnPlayerJoinEvent, OnUpdateEvent 
    {
        public VRC.Player player = new VRC.Player();
        public CustomNameplates() : base("Nameplate Info", "Cool Kids Nameplate", Main.Instance.SettingsButtonrender, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnPlayerJoinEvents.Add(this);
            RunOnce();
        }

        public override void OnDisable()
        {
            Main.Instance.OnPlayerJoinEvents.Remove(this);         
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            CustomNameplate nameplate = player.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
            nameplate.player = player;
        }


        public void OnPlayerLeft(VRC.Player player)
        {
            Main.Instance.OnPlayerJoinEvents.Remove(this);
       
        }

        public void OnUpdate()
        {
        }

        public void RunOnce()
        {
           
                try
                {
                    for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
                    {
                        VRC.Player player = PlayerWrapper.GetAllPlayers()[i];
                        CustomNameplate nameplate = player.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
                        nameplate.player = player;
                    if (i >= PlayerWrapper.GetAllPlayers().Length)
                    {
                        break;
                    }

                }
            
                }
                catch (Exception ERROR)
                {
                    Logg.Log(Logg.Colors.Green, ERROR.Message, false, false);
                }       
        }

        public void OnPlayerEnteredRoom(Photon.Realtime.Player player)
        {
            throw new NotImplementedException();
        }
    }
}
