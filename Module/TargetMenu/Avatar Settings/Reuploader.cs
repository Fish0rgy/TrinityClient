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
    internal class ReUploadAvatar : BaseModule
    {
        public ReUploadAvatar() : base("ReUpload", "Opens reuploader's folder.", Main.Instance.AvatarSettings, QMButtonIcons.CreateSpriteFromBase64(Serpent.Clone), false, false) { }
        public override void OnEnable()
        {
             try
            {
                Process.Start($"{Directory.GetCurrentDirectory()}\\Trinity\\Reuploader");
                LogHandler.Log(LogHandler.Colors.Green, $"[Re-Uploader] Lunching ReUploader\n", false, false);
            }catch (Exception ERROR) { }
        }
    }
}