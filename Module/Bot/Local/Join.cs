using Area51.SDK;
using System;
using System.Diagnostics;
using System.IO;

namespace Area51.Module.Bot.Local
{
    class Join : BaseModule
    {
        public Join() : base("Join", "Bots Join World", Main.Instance.Privatebotbutton, null, false) { }
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

        public void StartBot(string fileName, string Payload)
        {
            if (fileName == "" || (Payload == "")) { Logg.Log(Logg.Colors.Green, "Empty Input\n", true, false); }
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

        public override void OnEnable()
        {
            try
            {
                if (!Directory.Exists(@"\Area51\Bot\"))
                {
                    string worldID = PlayerWrapper.GetWorldID;
                    string instanceID = PlayerWrapper.GetinstanceID;
                    string userID = PlayerWrapper.GetUserID;
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"\Area51\Bot\";
                    if (File.Exists(path + "Area51.exe"))
                    {
                        string accounts = System.IO.File.ReadAllText(@path + @"bot.txt");
                        Logg.Log(Logg.Colors.Green, "[Bots] are joining world.\n", false, false);
                        string[] bots = accounts.Split('|');
                        string Region = GetRegion(GetWorldRegion);
                        Logg.LogDebug("[Bots] Joining Now!");

                        //Josh's PhotonBots
                        //user:pass
                        //-left 
                        //+right
                        StartBot(path + "Area51.exe", bots[0] + "|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|0.7|JoinWorld");
                        StartBot(path + "Area51.exe", bots[1] + "|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|-0.7|JoinWorld");

                    }
                    else
                    {
                        Logg.Log(Logg.Colors.Green, "[Bots] failed to join world.\n", false, false);
                        Logg.LogDebug("[Bots] Failed To Join World.");
                    }
                }
                else
                {
                    Logg.Log(Logg.Colors.Red, "[Bots] You Dont Have Access To Local Handler.\n", false, false);
                    Logg.LogDebug("[Bots] You Dont Have Access To Local Handler!");
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
} 
