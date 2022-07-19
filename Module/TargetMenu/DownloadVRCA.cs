using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Net;
using VRC.Core;

namespace Trinity.Module.TargetMenu 
{
    internal class DownloadVRCSelected : BaseModule
    {
        public DownloadVRCSelected() : base("VRCA", "Download Users VRCA", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.SavePath), false, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("AVATAR: <color=green>Downloading Targets VRCA</color>");
            using (var wc = new WebClient { Headers = { "User-Agent: Other" } })
            {
                ApiAvatar avatar = PU.SelectedVRCPlayer().prop_ApiAvatar_0;
                wc.DownloadFileAsync(new Uri(avatar.assetUrl), "Trinity/VRCA/" + avatar.name + "_" + avatar.id + ".vrca");                            
                LogHandler.Log(LogHandler.Colors.Grey, "Downloaded Selected User VRCA Completed", false, false);
                LogHandler.LogDebug("[Ripper] -> Downloaded Selected User VRCA Completed!");
            }
        }
    }
}
