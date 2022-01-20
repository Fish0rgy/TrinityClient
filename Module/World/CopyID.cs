using Area51.SDK;

namespace Area51.Module.World
{
    class CopyWID : BaseModule
    {
        public CopyWID() : base("Get World ID", "Copy the World + InstanceID", Main.Instance.WorldButton, null, false) { }

        public override void OnEnable()
        {
            if (WorldWrapper.GetWorldID != "")
                Misc.SetClipboard(PlayerWrapper.GetAPIUser(PlayerWrapper.LocalPlayer).location);
            Logg.Log(Logg.Colors.Blue, "World ID: " + WorldWrapper.GetWorldID + " copied to clipboard.", false, false);
            Logg.LogDebug($"Copied World ID To Clipboard!");
        }

    }
}
