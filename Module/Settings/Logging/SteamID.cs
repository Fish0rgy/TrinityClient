using Area51.Events;

namespace Area51.Module.Settings.Logging
{
    class SteamID : BaseModule
    {
        public SteamID() : base("SteamID", "Logs Players Joining And Leaving", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }
    }
}
