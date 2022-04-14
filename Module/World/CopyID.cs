using Trinity.SDK;

namespace Trinity.Module.World
{
    class CopyWID : BaseModule
    {
        public CopyWID() : base("World ID", "Copies the World & InstanceID", Main.Instance.WorldButton, SDK.ButtonAPI.QMButtonIcons.CreateSpriteFromBase64(Serpent.earth), false, false) { }

        public override void OnEnable()
        {
            if (WorldWrapper.GetWorldID != "") { Misc.SetClipboard(PlayerWrapper.GetAPIUser(PlayerWrapper.LocalPlayer).location);          
            LogHandler.Log(LogHandler.Colors.Green, "World ID: " + WorldWrapper.GetWorldID + " copied to clipboard.", false, false);
            LogHandler.LogDebug($"Copied World ID To Clipboard!");
            }
        }
    }
}
