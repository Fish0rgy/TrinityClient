using Trinity.Utilities;
using Trinity.SDK;

namespace Trinity.Module.Player
{
    class AvatarID : BaseModule
    {
        public AvatarID() : base("Change Avatar By ID", "copy an avatarid into your clipboard then click change. ", Main.Instance.PlayerButton, SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }
        public override void OnEnable()
        {
            if (Misc.GetClipboard().StartsWith("avtr")) { PU.ChangeAvatar(Misc.GetClipboard()); }
        }
    }
}
