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
    public  class ReUploadAvatar : BaseModule
    {
        public ReUploadAvatar() : base("ReUpload", "Opens reuploader's folder.", Main.Instance.AvatarSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.ClonePath), false, false) { }
        public override void OnEnable()
        {
            ApiAvatar avatar = PU.SelectedVRCPlayer().prop_ApiAvatar_0;
            var name = avatar.name;
            var url = avatar.assetUrl;
            var img = avatar.imageUrl; 
            var FolderPath = AppDomain.CurrentDomain.BaseDirectory + "\\Trinity\\Reuploader\\"; //makes string for bot directory
            var LoginPath = AppDomain.CurrentDomain.BaseDirectory + "\\Trinity\\Reuploader\\Login.txt"; //makes string for bot directory
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Trinity");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ~> ");
                Console.ForegroundColor = LogHandler.getColor(LogHandler.Colors.Blue);
                Console.Write("Enter username:password: ");
                Console.ForegroundColor = ConsoleColor.White;
                var UserPass = Console.ReadLine(); 
                LogHandler.Log(LogHandler.Colors.Green, $"[Re-Uploader] Lunching ReUploader\n", false, false);
                startReuploader(FolderPath + "CloudyBoop.exe", string.Concat(UserPass, "|", name, "|", url, "|", img));
            }
            catch (Exception ERROR) { }
        }
        public void startReuploader(string fileName, string arguments)
        {
            var File = fileName == "" || arguments == "";
            var checkFile = File;
            if (checkFile) LogHandler.Log(LogHandler.Colors.Green, "Reupload files unfound\n", true);
            var process = new Process { StartInfo = { FileName = fileName, Arguments = arguments } };
            process.Start();
        }
    }
    
}