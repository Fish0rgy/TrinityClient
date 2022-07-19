using Trinity.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Trinity.WebAPI;
using VRC.Core;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using MelonLoader.ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace Trinity.SDK.Security
{
    class SecurityCheck
    {
        public static readonly string ClientFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\Trinity";
        public static readonly string ZipPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\trinity.zip";
        public static ServerInfoResp ExploitData { get; set; }
        public static void CheckSteam()
        { 
            if (!File.Exists($"{ClientFolder}\\Misc\\Steam.txt"))
            {
                File.Create($"{ClientFolder}\\Misc\\Steam.txt");
                Config.SpoofSteam = false;
            }
            bool steamcheck = File.ReadAllText($"{ClientFolder}\\Misc\\Steam.txt").Contains("true");
            if (steamcheck) 
            {
                Config.SpoofSteam = true;
            } 
        }
        public static void CheckFile()
        {
            try
            {
                bool check = Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\Trinity");
                WebClient wc = new WebClient(); 
                if (!check)
                {
                    LogHandler.Log(LogHandler.Colors.Yellow, $"[MISSING FILE] Downloading necessary files please wait...", false, false);
                    wc.DownloadFile($"https://github.com/Fish0rgy/TrinityClient/releases/latest/download/trinity.zip", $"{AppDomain.CurrentDomain.BaseDirectory}\\trinity.zip");
                    LogHandler.Log(LogHandler.Colors.Red, $"[COMPLETED] We downloaded the files required to use this client. Please unzip trinity.zip located here {ZipPath} after you can continue playing.", false, false);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Trinity");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ~> ");
                    Console.ForegroundColor = LogHandler.getColor(LogHandler.Colors.Yellow);
                    Console.Write("Did You UnZip trinity.zip ? (y/n): "); 
                    Console.ForegroundColor = ConsoleColor.White;
                    string answer = Console.ReadLine();
                    if (answer == "y") return;
                    LogHandler.Log(LogHandler.Colors.Red, $"[NOTICE] PLEASE UNZIP FILE CALLED trinity.zip TO USE THIS CLIENT, YOUR GAME WILL CLOSE IN 5 SECONDS", false, false);
                    LogHandler.Log(LogHandler.Colors.Red, $"5", false, false);
                    Thread.Sleep(1000);
                    LogHandler.Log(LogHandler.Colors.Red, $"4", false, false);
                    Thread.Sleep(1000);
                    LogHandler.Log(LogHandler.Colors.Red, $"3", false, false);
                    Thread.Sleep(1000);
                    LogHandler.Log(LogHandler.Colors.Red, $"2", false, false);
                    Thread.Sleep(1000);
                    LogHandler.Log(LogHandler.Colors.Red, $"1", false, false);
                    Thread.Sleep(1000);
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, "[CHECKER] ERROR: 404 URL DOESNT EXIST", false, false);  //if pastebin is down or im requesting to go to a url the wrong Way 
            }
        }
        public static void CheckUpdate()
        {
            try
            {
                WebClient wc = new WebClient();
                string VR = wc.DownloadString("https://raw.githubusercontent.com/Fish0rgy/TrinityClient/trinityu/version");
                byte[] DR = new WebClient().DownloadData($"https://github.com/Fish0rgy/TrinityClient/releases/latest/download/trinity.dll");
                if (!VR.Contains(Main.fileVersion))
                {
                    File.WriteAllBytes("Mods/trinity.dll", DR);
                    LogHandler.Log(LogHandler.Colors.Red, $"[OUTDATED] CURRENT: {Main.fileVersion} | LATEST: {VR}", false, false); 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Trinity");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ~> ");
                    Console.ForegroundColor = LogHandler.getColor(LogHandler.Colors.Yellow);
                    Console.Write("[SUCCESS] Restart To Apply? (y/n): ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string answer = Console.ReadLine();
                    if (answer != "y") return;
                    Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}\\VRChat.exe");
                    Process.GetCurrentProcess().Kill();
                }
                else
                    LogHandler.Log(LogHandler.Colors.Green, $"[UP2DATE] Version: {Main.fileVersion}", false, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, "[UPDATER] ERROR: 404 URL DOESNT EXIST", false, false);  //if pastebin is down or im requesting to go to a url the wrong Way 
            }
        } 
    }
}
