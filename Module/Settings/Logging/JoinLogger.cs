using Area51.Events;
using Area51.SDK;

namespace Area51.Module.Settings.Logging
{
    class JoinLogger : BaseModule, OnPlayerJoinEvent, OnPlayerLeaveEvent
    {
        public JoinLogger() : base("Join/Leave Log", "Logs Players Joining And Leaving", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnPlayerJoinEvents.Add(this);
            Main.Instance.OnPlayerLeaveEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            Main.Instance.OnPlayerLeaveEvents.Remove(this);
        }

        void OnPlayerJoinEvent.OnPlayerJoin(VRC.Player player)
        {
            Logg.Log(Logg.Colors.Blue, $"Player Joined ~> Username: {player.prop_APIUser_0.displayName} | Photon ID: {player.prop_VRCPlayerApi_0.playerId} | UserID: {player.prop_APIUser_0.id}", false, false);
            Logg.LogDebug($"Player Joined ~> Username: {player.prop_APIUser_0.displayName}");
        }

        public void PlayerLeave(VRC.Player player)
        {
            Logg.Log(Logg.Colors.Blue, $"Player Left ~> Username: {player.prop_APIUser_0.displayName} | UserID: {player.prop_APIUser_0.id}", false, false);
            Logg.LogDebug($"Player Left ~> Username: {player.prop_APIUser_0.displayName}");
        }
    }
}
