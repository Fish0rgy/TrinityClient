using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System.Diagnostics;
using VRC.Core;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.IO;

namespace Trinity.Module.TargetMenu
{
    internal class ReUploadTUT : BaseModule
    {
        public ReUploadTUT() : base("Tutorial", "Opens the tutorial textfile.", Main.Instance.AvatarSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }
        public override void OnEnable()
        {
            try
            {
                Process.Start($@"{Directory.GetCurrentDirectory()}\Trinity\Reuploader\Data\Tutorial");
                LogHandler.Log(LogHandler.Colors.Green, $"[Re-Uploader] Lunching ReUploader\n", false, false);
            }
            catch (Exception ERROR) { }
        }
    }
}