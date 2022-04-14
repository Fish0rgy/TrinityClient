using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class Tel2User : BaseModule
    {
        public Tel2User() : base("Teleport", "Teleports to selected user.", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.TeleportPath), false, false) { }

        public override void OnEnable()
        {
                APIUser SelectedPlayer = PU.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
                PU.Teleport(PU.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0));
                LogHandler.LogDebug($"[Info] -> Teleported To: {SelectedPlayer.displayName}");
        }
    }
}
