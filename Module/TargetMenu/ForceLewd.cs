using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;
using Trinity.Utilities;


namespace Trinity.Module.TargetMenu
{
    class ForceLewd : BaseModule
    {
        //list form lucy aka owner from latenight

        public UnityEngine.GameObject localPlayer = PU.GetPlayer().gameObject;
        public UnityEngine.GameObject playerMirrFix = PU.GetPlayerMirrFix();
        public UnityEngine.GameObject playerMirrFix2 = PU.GetPlayerMirrFix2();
        public ForceLewd() : base("Force Lewd", "Removes Players Cloths", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.copyPath), false, false) { }

        public override void OnEnable()
        {
            APIUser SelectedPlayer = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
            if (SelectedPlayer.id == null) return;
            localPlayer.Lewdify();
            playerMirrFix.Lewdify();
            playerMirrFix2.Lewdify();
            LogHandler.Log(LogHandler.Colors.Green, $"Force Lewdifed {SelectedPlayer.displayName}", false, false);
            LogHandler.LogDebug($"Force Lewdifed {SelectedPlayer.displayName}");
        }
    }
}
