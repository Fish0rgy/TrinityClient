using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using VRC;

namespace Trinity.Module.Settings.Logging
{
    class AvatarLogger : BaseModule, OnPlayerJoinEvent
    {
        public AvatarLogger() : base("Avatar Logger", "Logs Avatars In World", Main.Instance.SettingsButtonLoggging, null, true, true) { }

        public override void OnEnable()
        {
            MenuUI.Log("LOGGING: <color=green>Avatar Logger On</color>");
            Main.Instance.OnPlayerJoinEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("LOGGING: <color=red>Avatar Logger Off</color>");
            Main.Instance.OnPlayerJoinEvents.Remove(this);
        }

        public void OnPlayerJoin(VRC.Player player)
        {            
            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"Username: {player.prop_VRCPlayerApi_0.displayName}", true, false);
            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"AvatarID: {player.prop_ApiAvatar_0.id}", true, false);
            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"URL:  {player.prop_ApiAvatar_0.assetUrl}", true, false);
            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"SIZE_LIMIT:  {ValidationHelpers.CONTENT_AVATAR_ASSET_BUNDLE_SIZE_LIMIT_PC}", true, false);
            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"FileSize:  {PU.IsAssetBundleFileTooLarge(player)}", true, false);
            SDK.LogHandler.LogDebug($"[Avatar Logger] User: {player.prop_VRCPlayerApi_0.displayName}");
        }
    }
}