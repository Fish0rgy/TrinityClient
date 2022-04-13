using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    class ForceLewd : BaseModule
    {
        //list form lucy aka owner from latenight

        public UnityEngine.GameObject localPlayer = PlayerWrapper.LocalPlayer.gameObject;
        public UnityEngine.GameObject playerMirrFix = PlayerWrapper.GetPlayerMirrFix();
        public UnityEngine.GameObject playerMirrFix2 = PlayerWrapper.GetPlayerMirrFix2();
        public ForceLewd() : base("Force Lewd", "Removes Players Cloths", Main.Instance.Targetbutton, QMButtonIcons.CreateSpriteFromBase64(Alien.copy), false, false) { }

        public override void OnEnable()
        {
            APIUser SelectedPlayer = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0;
            if (SelectedPlayer.id != "")
            { 
                localPlayer.Lewdify();
                playerMirrFix.Lewdify();
                playerMirrFix2.Lewdify();
            }
            LogHandler.Log(LogHandler.Colors.Green,$"Force Lewdifed {SelectedPlayer.displayName}",false,false);
            LogHandler.LogDebug($"Force Lewdifed {SelectedPlayer.displayName}");
        }
    }
}
