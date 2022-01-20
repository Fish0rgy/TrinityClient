using Area51.SDK;

namespace Area51.Module.Player
{
    class AviToID : BaseModule
    {
        public AviToID() : base("Change Avatar By ID", "copy an avatarid into your clipboard then click change. ", Main.Instance.PlayerButton, null, false) { }
        public override void OnEnable()
        {
            if (Misc.GetClipboard().StartsWith("avtr"))
                PlayerWrapper.ChangeAvatar(Misc.GetClipboard());
        }

    }
}
