using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class UserIDSelected : BaseModule
    {
        public UserIDSelected() : base("UserID", "Grabs userid from selected user", Main.Instance.Targetbutton, QMButtonIcons.CreateSpriteFromBase64(Alien.copy), false, false) { }

        public override void OnEnable()
        {
            APIUser SelectedPlayer = PlayerWrapper.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
            if (SelectedPlayer.id != "")
                SDK.Misc.SetClipboard(SelectedPlayer.id);
            LogHandler.LogDebug("[Info] -> Coppied UserID to clipboard.");
        }


    }
}
