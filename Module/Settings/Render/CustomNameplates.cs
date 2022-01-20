using Area51.Events;

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
            while(toggled)
            {
                CustomNameplate nameplate = player.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
                nameplate.player = player;
            }
        }
    }
}
