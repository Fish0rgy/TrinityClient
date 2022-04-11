using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System.Diagnostics;
using VRC.Core;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.IO;

namespace Area51.Module.TargetMenu
{
    internal class ReUploadTUT : BaseModule
    {
        public ReUploadTUT() : base("Tutorial", "Opens the tutorial textfile.", Main.Instance.AvatarSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo), false, false) { }
        public override void OnEnable()
        {
            try
            {
                Process.Start($@"{Directory.GetCurrentDirectory()}\Area51\Reuploader\Data\Tutorial");
                LogHandler.Log(LogHandler.Colors.Green, $"[Re-Uploader] Lunching ReUploader\n", false, false);
            }
            catch (Exception ERROR) { }
        }
    }
}