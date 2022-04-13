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
            PlayerWrapper.ClearAssets();
            PlayerWrapper.ShowSelf(true);
            PlayerWrapper.ChangeAvatar(PlayerWrapper.backupID);
        }
        public override void OnEnable()
        {
            PlayerWrapper.backupID = APIUser.CurrentUser.avatarId;
            PlayerWrapper.ShowSelf(false); 
        }
    }
}