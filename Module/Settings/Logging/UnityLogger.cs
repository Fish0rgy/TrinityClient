using Trinity.Utilities;
namespace Trinity.Module.Settings.Logging
{
    class UnityLogger : BaseModule
    {
        public static UnityLogger Instance;

        public UnityLogger() : base("UnityLogger", "Logs Unity Debug", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
            Instance = this;
        }
    }
}
