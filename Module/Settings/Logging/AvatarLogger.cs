using Area51.Events;
using Area51.SDK;
using UnityEngine;

namespace Area51.Module.Settings.Logging
{
    class AvatarLogger : BaseModule, OnAvatarLoadedEvent
    {

        public AvatarLogger() : base("Avatar Logger", "Logs Avatars In World", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnAvatarLoadEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnAvatarLoadEvents.Remove(this);
        }

        public bool OnAvatarLoad(VRCPlayer player, GameObject __0)
        {
            Logg.Log(Logg.Colors.Blue, $"Username: {player.prop_VRCPlayerApi_0.displayName} | AvatarID: {player.prop_ApiAvatar_0.id}");
            Logg.LogDebug($"[Avatar Logger] User: {player.prop_VRCPlayerApi_0.displayName} | Avi ID: {player.prop_ApiAvatar_0.id}");
            return true;
        }
    }
}
