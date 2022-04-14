using Trinity.SDK;

namespace Trinity.Module.World
{
    class CopyUserID : BaseModule
    {
        public CopyUserID() : base("Get User ID", "Copy the UserID to clipboard", Main.Instance.PlayerButton, SDK.ButtonAPI.QMButtonIcons.CreateSpriteFromBase64(Serpent.copy), false, false) { }

        public override void OnEnable()
        {
            if (PlayerWrapper.GetUserID != "")
                Misc.SetClipboard(PlayerWrapper.GetUserID);
            LogHandler.Log(LogHandler.Colors.Green, "User ID: " + PlayerWrapper.GetUserID + " Copied to clipboard.", false, false);
        }

    }
}
