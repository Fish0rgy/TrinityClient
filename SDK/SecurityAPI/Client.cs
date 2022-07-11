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

namespace Trinity.SDK.Security
{
    class SecurityCheck
    { 
        public static readonly string ClientFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\Trinity";
        public static ServerInfoResp ExploitData { get; set; }
        public static void CheckUpdate()
        { 
            try
            {
                WebClient wc = new WebClient();
                string VR = wc.DownloadString("https://raw.githubusercontent.com/Fish0rgy/Trinity/main/version");  
                byte[] DR = new WebClient().DownloadData($"https://github.com/Fish0rgy/Trinity/releases/latest/download/trinity.dll"); 
                if (!VR.Contains(Main.fileVersion))
                { 
                    LogHandler.Log(LogHandler.Colors.Red, $"[OUTDATED] CURRENT: {Main.fileVersion} | LATEST: {VR}", false, false);
                    File.WriteAllBytes("Mods/trinity.dll", DR);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Trinity");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ~> ");
                    Console.ForegroundColor = LogHandler.getColor(LogHandler.Colors.Yellow);
                    Console.Write("[SUCCESS] Restart To Apply?: ");
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
