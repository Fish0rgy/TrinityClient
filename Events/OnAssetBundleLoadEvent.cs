using UnityEngine;

namespace Trinity.Events
{
    public interface OnAssetBundleLoadEvent
    {
        bool OnAvatarAssetBundleLoad(GameObject avatar, string avatarID);
    }
}
