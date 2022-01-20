using Area51.SDK;

namespace Area51.Module.World
{
    class CopyUserID : BaseModule
    {
        public CopyUserID() : base("Get User ID", "Copy the UserID to clipboard", Main.Instance.PlayerButton, null) { }

        public override void OnEnable()
        {
            if (PlayerWrapper.GetUserID != "")
                Misc.SetClipboard(PlayerWrapper.GetUserID);
            Logg.Log(Logg.Colors.Green, "User ID: " + PlayerWrapper.GetUserID + " Copied to clipboard.", false, false);
        }

    }
}
