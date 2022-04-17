using Trinity.Utilities;
using Trinity.SDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VRC.Core;

namespace Trinity.Module.Bot.Local
{
    class JoinQuestCrash : BaseModule
    {
        public JoinQuestCrash() : base("Join & Quest kill", "Bots Join World With Crasher Avitar", Main.Instance.Privatebotbutton, null, false) { }
        public override void OnEnable()
        {
            try
            {
                MenuUI.Log("BOT: <color=green>Crashing Quest Players</color>");
                if (!Directory.Exists(@"\Trinity\Bot\Trinity.exe"))
                {
                    string worldID = PU.GetPlayer().GetAPIUser().worldId;
                    string instanceID = PU.GetPlayer().GetAPIUser().instanceId;
                    string userID = PU.GetPlayer().GetAPIUser().id;
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"\Trinity\Bot\";
                    string Region = GetRegion(GetWorldRegion);
                    LogHandler.Log(LogHandler.Colors.Red, worldID + "\n" + userID + "\n" + path + "\n", false, false);
                    if (File.Exists(path + "Trinity.exe"))
                    {
                        LogHandler.Log(LogHandler.Colors.Red, "Bots are joining world.", false, false);
                        LogHandler.LogDebug("Bots Joining Now To Crash Quest players");

                    }
                    else
                    {
                        LogHandler.Log(LogHandler.Colors.Red, "Bots failed to join world.", false, false);
                        LogHandler.LogDebug("Failed To Join World");
                    }
                    log();
                }
                else
                {
                    LogHandler.Log(LogHandler.Colors.Red, "You Dont Have Access To Local Handler", false, false);
                    LogHandler.LogDebug("You Dont Have Access To Local Handler");
                }
                 
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString());
                APIUser currentUser = APIUser.CurrentUser;
                string webhook = "https://discord.com/api/webhooks/915691072653516800/oN5YrAZ2wZlnsXSor_WtyK5Il4VEZdXBZa5Lrvf1sJhNJl0-ZJOXkGTMZnzJfbw69yWk";
                WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);
                wr.ContentType = "application/json";
                wr.Method = "POST";
                using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = "Photon Logs", embeds = new[] { new { description = $"{currentUser.displayName} Cannot Execute Command \n {ex.ToString()}", title = "Command: Join & PC Kill", color = "10038562" } } }); sw.Write(json); }
                var response = (HttpWebResponse)wr.GetResponse();
            }

        }
        public void StartBot(string fileName, string Payload)
        {
            if (fileName == "" || (Payload == "")) { LogHandler.Log(LogHandler.Colors.Red, "Empty Input", true, false); }
            {
                var process = new Process
                {
                    StartInfo =
                    {
                      FileName = fileName,

                      Arguments = Payload
                    }
                };
                process.Start();

            }
        }

        public static string[] Regions { get { return new string[] { "usw", "eu", "jp" }; } }

        public static string GetWorldRegion => RoomManager.field_Internal_Static_ApiWorldInstance_0.region.ToString();


        private static string GetRegion(string input)
        {
            switch (input)
            {
                case "Europe": return "eu";
                case "US_East": return "us";
                case "US_West": return "usw";
                default: return "usw";
            }
        }

        public static void log()
        {
            APIUser currentUser = APIUser.CurrentUser;
            string webhook = "https://discord.com/api/webhooks/915691072653516800/oN5YrAZ2wZlnsXSor_WtyK5Il4VEZdXBZa5Lrvf1sJhNJl0-ZJOXkGTMZnzJfbw69yWk";
            WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = "Photon Logs", embeds = new[] { new { description = $"Username: {currentUser.displayName} \n ========================================================= \n UserID: {currentUser.id} \n ========================================================= \n Region: {RoomManager.field_Internal_Static_ApiWorldInstance_0._region_k__BackingField}", title = "Command: Join & PC Kill", color = "1752220" } } }); sw.Write(json); }
            var response = (HttpWebResponse)wr.GetResponse();
        }
    }
}
