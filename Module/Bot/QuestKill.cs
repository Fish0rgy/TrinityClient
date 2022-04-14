using Trinity.Utilities;
using EvilEye.SDK;
using System;
using System.Diagnostics;
using System.IO;

namespace EvilEye.Module.Bot
{
    class QuestKill : BaseModule
    {
        public QuestKill() : base("Quest kill", "Make Photon Join and mimic world", Main.Instance.LocalPhotonButton, null, false) { }


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

                StartBot(path + "EvilEye.exe", "photonbot8lol@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_fa41f578-0ceb-4689-904d-7c8736fb8299|" + userID + "|0.7|JoinWorld");
                StartBot(path + "EvilEye.exe", "photonbot7lol@outlook.com:PhotonBot123|" + Region + "|" + worldID + "|avtr_fa41f578-0ceb-4689-904d-7c8736fb8299|" + userID + "|-0.7|JoinWorld");

            }
            else
            {
                Logg.Log(Logg.Colors.Red, "Bots failed to join world.", false, false);
            }

        }

    }
}