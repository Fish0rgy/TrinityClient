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
            MenuUI.Log("LOCATION: <color=green>Teleported To Target</color>"); 
            PU.Teleport(PU.SelectedVRCPlayer());
            LogHandler.LogDebug($"[Info] -> Teleported To: {PU.SelectedVRCPlayer().prop_APIUser_0.displayName}");
        }
    }
}
