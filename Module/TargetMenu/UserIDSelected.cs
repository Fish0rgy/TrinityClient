using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class UserIDSelected : BaseModule
    {
        public UserIDSelected() : base("UserID", "Grabs userid from selected user", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.copyPath), false, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("PLAYER: <color=green>Copied Targets User ID</color>");
            APIUser SelectedPlayer = PU.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
            if (SelectedPlayer.id != "")
                SDK.Misc.SetClipboard(SelectedPlayer.id);
            LogHandler.LogDebug("[Info] -> Coppied UserID to clipboard.");
        }


    }
}
