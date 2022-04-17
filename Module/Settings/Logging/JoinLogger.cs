using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Photon.Realtime;

namespace Trinity.Module.Settings.Logging
{
    class JoinLogger : BaseModule, OnPlayerJoinEvent, OnPlayerLeaveEvent
    {
        public JoinLogger() : base("Join/Leave Log", "Logs Players Joining And Leaving", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("LOGGING: <color=green>Join/Leave Logger On</color>");
            Main.Instance.OnPlayerJoinEvents.Add(this);
            Main.Instance.OnPlayerLeaveEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("LOGGING: <color=red>Join/Leave Logger Off</color>");
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            Main.Instance.OnPlayerLeaveEvents.Remove(this);
        }

        void OnPlayerJoinEvent.OnPlayerJoin(VRC.Player player)
        {
            LogHandler.Log(LogHandler.Colors.Blue, $"Player Joined ~> Username: {player.prop_APIUser_0.displayName} | Photon ID: {player.prop_VRCPlayerApi_0.playerId} | UserID: {player.prop_APIUser_0.id}", false, false);
            LogHandler.LogDebug($"Player Joined ~> Username: {player.prop_APIUser_0.displayName}");
        }

        public void PlayerLeave(VRC.Player player)
        {
            LogHandler.Log(LogHandler.Colors.Blue, $"Player Left ~> Username: {player.prop_APIUser_0.displayName} | UserID: {player.prop_APIUser_0.id}", false, false);
            LogHandler.LogDebug($"Player Left ~> Username: {player.prop_APIUser_0.displayName}");
        }
    }
}