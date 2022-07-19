using Trinity.Utilities;
using Trinity.SDK;

namespace Trinity.Module.World
{
    class CopyWID : BaseModule
    { 
        public CopyWID() : base("World ID", "Copies the World & InstanceID", Main.Instance.WorldButton, SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.earthPath), false, false) { }

        public override void OnEnable()
        {
            if (WorldWrapper.GetWorldID != null)
            {
                Misc.SetClipboard(PU.GetAPIUser(PU.GetPlayer()).location);
                LogHandler.Log(LogHandler.Colors.Green, "World ID: " + WorldWrapper.GetWorldID + " copied to clipboard.", false, false);
                LogHandler.LogDebug($"Copied World ID To Clipboard!");
                MenuUI.Log("WORLD: <color=green>Copied World ID To Clipboard</color>");
            }
        }
    }
}
