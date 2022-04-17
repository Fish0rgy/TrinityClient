using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;

namespace Trinity.Module.Settings.Logging
{
    class UdonLogger : BaseModule, OnUdonEvent
    {
        public UdonLogger() : base("Udon Logger", "Logs Udon Events", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("LOGGING: <color=green>Udon Logger On</color>");
            Main.Instance.OnUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("LOGGING: <color=red>Udon Logger Off</color>");
            Main.Instance.OnUdonEvents.Remove(this);
        }
        public bool OnUdon(string __0, VRC.Player __1)
        {
            LogHandler.Log(LogHandler.Colors.Blue, $"Type: {__0} | From {__1.field_Private_APIUser_0.displayName}", false, false);
            LogHandler.LogDebug($"[Udon Logger] Type: {__0} | From {__1.field_Private_APIUser_0.displayName}");
            return true;
        }
    }
}
