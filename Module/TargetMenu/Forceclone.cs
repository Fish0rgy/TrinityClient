using Area51.SDK;
using Area51.SDK.ButtonAPI;
using VRC.Core;

namespace Area51.Module.TargetMenu
{
    internal class ForceClone : BaseModule
    {
        public ForceClone() : base("ForceClone", "Clones public\\Cloneable avatars.", Main.Instance.Targetbutton, QMButtonIcons.CreateSpriteFromBase64(Alien.Clone), false, false) { }

        public override void OnEnable()
        {
            ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
            if (avatar.releaseStatus == "public")
                PlayerWrapper.ChangeAvatar(avatar.id);
            LogHandler.LogDebug("[Info] -> ForceClone Completed!");
        }
    }
}
