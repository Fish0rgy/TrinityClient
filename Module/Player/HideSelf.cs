using Area51.SDK;
using UnityEngine;
using VRC.Core;


namespace Area51.Module.Exploit
{
    class HideSelf : BaseModule
    {
        private string backupID;
        public static VRCPlayer LocalPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        private static GameObject avatarPreviewBase;
        public static GameObject GetAvatarPreviewBase() => avatarPreviewBase = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/AvatarPreviewBase");
        public HideSelf() : base("Hide Self", "Unloads your Avatar", Main.Instance.PlayerButton, null, true) { }
        public override void OnDisable()
        {
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.field_Private_Cache_0.ClearCache();
            PlayerWrapper.ChangeAvatar(backupID);
            GetAvatarPreviewBase().SetActive(true);
            LocalPlayer().prop_VRCAvatarManager_0.gameObject.SetActive(true);
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(true);
           
        }
        public override void OnEnable()
        {
            backupID = APIUser.CurrentUser.avatarId;
            GetAvatarPreviewBase().SetActive(false);
            LocalPlayer().prop_VRCAvatarManager_0.gameObject.SetActive(false);
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(false);

        }
    }
}