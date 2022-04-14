using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class ForceClone : BaseModule
    {
        public ForceClone() : base("ForceClone", "Clones public\\Cloneable avatars.", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.ClonePath), false, false) { }

        public override void OnEnable()
        {
            ApiAvatar avatar = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
            if (avatar.releaseStatus == "public")
                PU.ChangeAvatar(avatar.id);
            LogHandler.LogDebug("[Info] -> ForceClone Completed!");
        }
    }
}
