using Trinity.Utilities;
using Trinity.SDK;

namespace Trinity.Module.World
{
    class CopyUserID : BaseModule
    {
        public CopyUserID() : base("Get User ID", "Copy the UserID to clipboard", Main.Instance.PlayerButton, SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.copyPath), false, false) { }

        public override void OnEnable()
        {
            if (PU.GetPlayer().GetAPIUser().id != "")
                Misc.SetClipboard(PU.GetPlayer().GetAPIUser().id);
            LogHandler.Log(LogHandler.Colors.Green, "User ID: " + PU.GetPlayer().GetAPIUser().id + " Copied to clipboard.", false, false);
        }

    }
}
