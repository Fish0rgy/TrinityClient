using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class AvatSelected : BaseModule
    {
        public AvatSelected() : base("AvatarID", "Grabs avatarid from selected user", Main.Instance.Targetbutton, QMButtonIcons.CreateSpriteFromBase64(Serpent.copy), false, false) { }

        public override void OnEnable()
        {
            ApiAvatar SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
            if (SelectedPlayer.id != "")
                SDK.Misc.SetClipboard(SelectedPlayer.id);
            LogHandler.LogDebug("[Info] -> Coppied AvatarID to clipboard.");
            
        }
    }
}
