using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using UnityEngine;

namespace Trinity.Module.Settings.Logging
{
    class AssetBundleLogger : BaseModule, OnAssetBundleLoadEvent
    {
        public AssetBundleLogger() : base("AssetBundle Log", "Logs AssetBundles That Load", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnAssetBundleLoadEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnAssetBundleLoadEvents.Remove(this);
        }

        public bool OnAvatarAssetBundleLoad(GameObject avatar, string avatarID)
        {
            LogHandler.Log(LogHandler.Colors.Blue, $"Type: {avatar.name} |  Loaded Asset Bundle", false, false);
            return true;
        }
    }
}
