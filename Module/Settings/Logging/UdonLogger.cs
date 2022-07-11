using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using VRC.Networking;

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
        public bool OnUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            LogHandler.Log(LogHandler.Colors.Blue, $"\nKey: {__0} \nGameObject: {__instance.gameObject.name} \nFrom: {__1.field_Private_APIUser_0.displayName} \n", false, false);
            LogHandler.LogDebug($"[Udon Logger] Key: {__0} | GameObject: {__instance.gameObject.name} | From {__1.field_Private_APIUser_0.displayName}");
            return true;
        }
    }
}
