using Trinity.Utilities;
using Trinity.SDK;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.Exploit
{
    class HideSelf : BaseModule
    {
        public HideSelf() : base("Hide Self", "Unloads your Avatar", Main.Instance.PlayerButton, null, true, false) { }
        public override void OnDisable()
        {
            PU.ClearAssets();
            PU.ShowSelf(true);
            PU.ChangeAvatar(PU.backupID);
            MenuUI.Log("AVATAR: <color=green>Hide Self Activated</color>");
        }
        public override void OnEnable()
        {
            PU.backupID = APIUser.CurrentUser.avatarId;
            PU.ShowSelf(false);
            MenuUI.Log("AVATAR: <color=red>Hide Self Deactivated</color>");
        }
    }
}