using Area51.SDK;

namespace Area51.Module.Player
{
    class AvatarID : BaseModule
    {
        public AvatarID() : base("Change Avatar By ID", "copy an avatarid into your clipboard then click change. ", Main.Instance.PlayerButton, SDK.ButtonAPI.QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }
        public override void OnEnable()
        {
            if (Misc.GetClipboard().StartsWith("avtr")) { PlayerWrapper.ChangeAvatar(Misc.GetClipboard()); }
        }
    }
}
