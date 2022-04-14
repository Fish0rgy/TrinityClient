using Trinity.Utilities;
using EvilEye.SDK;
using System;
using System.Diagnostics;
using System.IO;

namespace EvilEye.Module.Bot
{
    class JoinWorld : BaseModule
    {
        public JoinWorld() : base("Join World", "Make Photon Join and mimic world", Main.Instance.LocalPhotonButton, null, false) { }


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

    
        public override void OnEnable()
        {
            string Region = WorldWrapper.GetRegion(WorldWrapper.GetWorldRegion);
            string worldID = PlayerWrapper.GetWorldID;
            string userID = PlayerWrapper.GetUserID;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\EvilEye\Bot\";
           


            Logg.Log(Logg.Colors.Red, "WorldID: " + worldID + "\n" + "User:" + userID + "\n" + "BinPath:" + path + "\n" + "Server Region:" + Region, false, false);



            if (File.Exists(path + "EvilEye.exe"))
            {
                Logg.Log(Logg.Colors.Red, "Bots are joining world.", false, false);

                StartBot(path + "EvilEye.exe", "eebot1@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|0.7|JoinWorld");
                StartBot(path + "EvilEye.exe", "eebot2@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|-0.7|JoinWorld");
                //  StartBot(path + "EvilEye.exe", "PhotonBot11lol@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|-0.7|JoinWorld");
                // StartBot(path + "EvilEye.exe", "PhotonBot10lol@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|" + userID + "|-0.7|JoinWorld");

            }
            else
            {
                Logg.Log(Logg.Colors.Red, "Bots failed to join world.", false, false);
            }

        }

    }
}