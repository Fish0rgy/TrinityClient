using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Net;
using VRC.Core;

namespace Area51.Module.TargetMenu
{
    internal class DownloadVRCSelected : BaseModule
    {
        public DownloadVRCSelected() : base("VRCA", "Download Users VRCA", Main.Instance.Targetbutton, QMButtonIcons.CreateSpriteFromBase64(Alien.Save), false, false) { }

        public override void OnEnable()
        {
            using (var wc = new WebClient { Headers = { "User-Agent: Other" } })
            {
               
                ApiAvatar avatar = PlayerWrapper.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
                wc.DownloadFileAsync(new Uri(avatar.assetUrl), "Area51/VRCA/" + avatar.name + "_" + avatar.id + ".vrca");                            
                LogHandler.Log(LogHandler.Colors.Grey, "Downloaded Selected User VRCA Completed", false, false);
                LogHandler.LogDebug("[Ripper] -> Downloaded Selected User VRCA Completed!");
            }
        }
    }
}
