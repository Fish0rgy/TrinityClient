using Area51.Events;
using Area51.SDK;
using Photon.Realtime;
using VRC;

namespace Area51.Module.Settings.Logging
{
    class SteamID : BaseModule, OnPlayerJoinEvent
    {
        public SteamID() : base("SteamID", "Logs Players Joining And Leaving", Main.Instance.SettingsButtonLoggging, null, true, true)
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

        void OnPlayerJoinEvent.OnPlayerEnteredRoom(Photon.Realtime.Player player)
        {
            if (player.prop_Hashtable_0.ContainsKey("steamUserID"))
            {
                Logg.Log(Logg.Colors.Green, $"HashTable {player.prop_Hashtable_0.ToString()}", false, false);
            }
        }
        public void PlayerEnteredRoom(Photon.Realtime.Player player)
        {
            //public static string GetSteamID(Player player)
            //{
            //    if (player.CustomProperties.ContainsKey("steamUserID"))
            //        if ((string)player.CustomProperties["steamUserID"] != "0")
            //            return (string)player.CustomProperties["steamUserID"];
            //    return "No Steam";
            //}
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
