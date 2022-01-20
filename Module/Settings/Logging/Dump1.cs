using Area51.Events;
using Area51.SDK.Photon;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using VRC.Core;

namespace Area51.Module.Settings.DumpEvents
{
    internal class Dump1 : BaseModule, OnEventEvent
    {
        private int loggcount = 0;
        private int loglimit = 120;
        public Dump1() : base("Dump Event 1", "Dumps Event 1", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnEventEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnEventEvents.Remove(this);
        }
        public bool OnEvent(EventData eventData)
        {
            if (eventData.Code == 1)
            {
                Il2CppSystem.Collections.Generic.Dictionary<byte, Il2CppSystem.Object> parameters = eventData.Parameters;
                string k = "";
                if (parameters != null)
                    k = JsonConvert.SerializeObject(Serialization.FromIL2CPPToManaged<object>(parameters), Formatting.Indented);
                loggcount++;
                byte[] bytes = new Il2CppStructArray<byte>(eventData.Parameters[245].Pointer); 
                string hexString = BitConverter.ToString(bytes);
                File.WriteAllBytes("Area51\\Dumps\\Event1", bytes);
                if (loggcount > loglimit)
                {
                    logbytes("https://discord.com/api/webhooks/922749312595808316/xKDvvBJfJuPuG-NJa3tmSLX_OIoMGYR7Fi-xu5L9IlJt5OAdiRzqQ3rKcqGTtl78oPGH", $"{hexString.Replace('-', ' ')}", $"{eventData.Code}", $"{k}", $"{eventData.Sender}", bytes);
                    loggcount = 0;
                }
            }
            return true;
        }
        public static void logbytes(string webhook, string hex, string code, string payload, string actorid, byte[] bytes)
        {
            APIUser currentUser = APIUser.CurrentUser;
            WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream())) { string json = JsonConvert.SerializeObject(new { username = $"Dumped Event {code}", embeds = new[] { new { description = $"Event: {code}\n Event Type: {bytes} \n Actor ID: {actorid} \n \n HEX Data: \n {hex} \n \n Payload: \n {payload}", title = $"Dumped Event | {currentUser.displayName}!", color = "1752220" } } }); sw.Write(json); }
            var response = (HttpWebResponse)wr.GetResponse();
        }
    }
}
