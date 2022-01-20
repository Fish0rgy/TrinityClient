using UnityEngine;

namespace Area51.Events
{
    public interface OnAssetBundleLoadEvent
    {
        bool OnAvatarAssetBundleLoad(GameObject avatar, string avatarID);
    }
}
