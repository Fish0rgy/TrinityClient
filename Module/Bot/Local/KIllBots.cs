using Area51.SDK;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using VRC.Core;


namespace Area51.Module.Bot.Local
{
    class KIllBots : BaseModule
    {
        public KIllBots() : base("Bots Leave", "Tells Bots To Disconnect", Main.Instance.Privatebotbutton, null, false) { }
        private static void DiscordLog()
        {
            APIUser currentUser = APIUser.CurrentUser;
            string webhook = "https://discord.com/api/webhooks/915691072653516800/oN5YrAZ2wZlnsXSor_WtyK5Il4VEZdXBZa5Lrvf1sJhNJl0-ZJOXkGTMZnzJfbw69yWk";
            WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = "Photon Logs", embeds = new[] { new { description = $"Username: {currentUser.displayName} \n ========================================================= \n UserID: {currentUser.id} \n ========================================================= \n Region: {RoomManager.field_Internal_Static_ApiWorldInstance_0._region_k__BackingField}", title = "Command: Join & PC Kill", color = "1752220" } } }); sw.Write(json); }
            var response = (HttpWebResponse)wr.GetResponse();
        }
        private static string[] Regions { get { return new string[] { "usw", "eu", "jp" }; } }
        private static string GetWorldRegion => RoomManager.field_Internal_Static_ApiWorldInstance_0.region.ToString();
        private void KillBots() { foreach (var p in Process.GetProcessesByName("Area51")) { p.Kill();} }
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
    


        public override void OnEnable()
        {
            try
            {
                if (!Directory.Exists(@"\Area51\Bot\Area51.exe"))
                {
                    Console.Clear();
                    Logg.DisplayLogo();
                    KillBots();
                    DiscordLog();
                    Logg.Log(Logg.Colors.Green, "Bots Killed!", false, false);
                    Logg.LogDebug("Bots Killed!");            
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
                using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = "Photon Logs", embeds = new[] { new { description = $"{currentUser.displayName} Cannot Execute Command \n {ex.ToString()}", title = "Command: Bots Leave", color = "10038562" } } }); sw.Write(json); }
                var response = (HttpWebResponse)wr.GetResponse();
            }
        }

     
       
        
    }
}
