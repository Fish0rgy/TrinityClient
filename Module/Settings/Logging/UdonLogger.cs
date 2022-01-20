using Area51.Events;
using Area51.SDK;

namespace Area51.Module.Settings.Logging
{
    class UdonLogger : BaseModule, OnUdonEvent
    {
        public UdonLogger() : base("UdonLogger", "Logs Udon Events", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnUdonEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnUdonEvents.Remove(this);
        }
        public bool OnUdon(string __0, VRC.Player __1)
        {
            Logg.Log(Logg.Colors.Blue, $"Type: {__0} | From {__1.field_Private_APIUser_0.displayName}", false, false);
            Logg.LogDebug($"[Udon Logger] Type: {__0} | From {__1.field_Private_APIUser_0.displayName}");
            return true;
        }
    }
}
