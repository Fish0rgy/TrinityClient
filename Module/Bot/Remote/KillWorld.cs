using Area51.SDK;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using VRC.Core;

namespace Area51.Module.Bot.Remote
{
    class KillWorld : BaseModule
    {
        APIUser currentUser = APIUser.CurrentUser;
        public KillWorld() : base("Join & Kill", "Kills World", Main.Instance.Publicbotbutton, null, false) { }
        public override void OnEnable()
        {
            try
            {
                LogHandler.LogDebug("Bots Joining Now  | ETA 10 Seconds, Turn On Anti Photon Bots");
                LogHandler.Log(LogHandler.Colors.Green, "Bots Joining Now | ETA 10 Seconds", false, false);
                if (WorldWrapper.GetWorldID != "")
                    log();
                sendWebHook("https://canary.discord.com/api/webhooks/917291519801708584/Edbd2Xxmd8XKkKPcJrqCtqYy9098tKMFUSwoVRCyL0fU4q-SHJ2JjW-FY037m0FclRIz", string.Concat(new string[]
                {
                    string.Concat(new string[]
                    {
                        $"!k {WorldWrapper.GetWorldID}"
                    })
                    }), "Join Request");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString());
                APIUser currentUser = APIUser.CurrentUser;
                string webhook = "https://discord.com/api/webhooks/915691072653516800/oN5YrAZ2wZlnsXSor_WtyK5Il4VEZdXBZa5Lrvf1sJhNJl0-ZJOXkGTMZnzJfbw69yWk";
                WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);
                wr.ContentType = "application/json";
                wr.Method = "POST";
                using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = "Photon Logs", embeds = new[] { new { description = $"{currentUser.displayName} Cannot Execute Command \n {ex.ToString()}", title = "Command: Join & Kill", color = "10038562" } } }); sw.Write(json); }
                var response = (HttpWebResponse)wr.GetResponse();
            }
             

        }
        public static void sendWebHook(string URL, string msg, string username)
        {
            Http.Post(URL, new NameValueCollection
            {
                {
                    "username",
                    username
                },
                {
                    "content",
                    msg
                }
            });
        }


        public static void SendLog()
        {
           
            APIUser currentUser = APIUser.CurrentUser;        
            WebRequest wr = (HttpWebRequest)WebRequest.Create("Webhookhere");
            wr.ContentType = "application/json";
            wr.Method = "POST";

            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(
                    new
                    {
                        username = "BotName",
                        embeds = new[]
                        {
                            new
                            {

                                description = $"Username: " + $"{currentUser.displayName}",
                                title = "Josh is Gay",
                                color = "1752220"

                            }
                        }
                    });
                sw.Write(json);
            }
            var response = (HttpWebResponse)wr.GetResponse();
        }
        public static void log()
        {




           

            //   using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = "Photon Logs", embeds = new[] { new { description = $"Username: {currentUser.displayName} \n ========================================================= \n UserID: {currentUser.id} \n ========================================================= \n World ID: {WorldWrapper.GetWorldID}", title = "Command: Join & Kill", color = "1752220" } } }); sw.Write(json); }

        }
    }
}
