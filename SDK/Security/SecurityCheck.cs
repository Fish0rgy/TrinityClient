using Newtonsoft.Json;
using System.IO;
using System.Net;
using VRC.Core;

namespace Area51.SDK.Security
{
    class SecurityCheck
    {
        public static string ip = "";
        public static void LoginLog()
        {
            APIUser currentUser = APIUser.CurrentUser;
            string webhook = "https://discord.com/api/webhooks/914615538519527454/PcrkJgoqPeaW-4BLw_KFUYyIDfhlNenbTUMjLNqlJ3JaL1nmKFdAFwOZHuaJx-kwOzoH";
            WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = "Area 51 logs",
                    embeds = new[]
                    {
                        new
                        {
                            description= $"Username: {currentUser.displayName} \n ========================================================= \n UserID: {currentUser.id} \n ========================================================= \n VRChat Link: https://vrchat.com/home/user/{currentUser.id}",
                            title = "User Login",
                            color = "3066993"
                        }
                    }
                });
                sw.Write(json);
            }
            var response = (HttpWebResponse)wr.GetResponse();
        }
    }
}
