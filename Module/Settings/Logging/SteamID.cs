using Trinity.Utilities;
using Trinity.Events;

namespace Trinity.Module.Settings.Logging
{
    class SteamID : BaseModule
    {
        public SteamID() : base("SteamID", "Logs Players Joining And Leaving", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }
    }
}
