using Area51.SDK;
using Area51.SDK.ButtonAPI;
using Area51.SDK.Security;
using System;
using System.Diagnostics;
using System.IO;

namespace Area51.Module.Bot.Local
{
     class CuddleBot1 : BaseModule
    {
        public CuddleBot1() : base("Start\nBot One", "Bots Join World", Main.Instance.Privatebotbutton, Area51.SDK.ButtonAPI.QMButtonIcons.CreateSpriteFromBase64(Alien.clientLogo)) { } //Button
        public static string[] Regions { get { return new[] { "usw", "eu", "jp" }; } } //Creates world region strings
        public static string GetWorldRegion => RoomManager.field_Internal_Static_ApiWorldInstance_0.region.ToString(); //Grabs World region from RoomManager
        private static string GetRegion(string input) { switch (input) { case "Europe": return "eu"; case "US_East": return "us"; case "US_West": return "usw"; default: return "usw"; } } //Gets world region extention from input
        public override void OnEnable()
        {
            try
            {
                var BotExists = !Directory.Exists("\\Area51\\Bot\\"); //flags for bot directory
                var BotCheck = BotExists;
                if (BotCheck) //checks if bot directory exists
                {
                    var getWorldID = PlayerWrapper.GetWorldID;             //WorldID
                    var getinstanceID = PlayerWrapper.GetinstanceID;          //InstanceID
                    var getUserID = PlayerWrapper.GetUserID;              //UserID
                    var getClientID = SecurityCheck.keyString; 
                    var botDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Area51\\Bot\\"; //makes string for bot directory
                    var exeExists = File.Exists(botDirectory + "Area51.exe"); //creates flag for Area51.exe exsistence
                    var exeCheck = exeExists;
                    if (exeCheck)
                    {
                        LogHandler.Log(LogHandler.Colors.Green, "[CUDDLEBOT] is joining world.\n"); LogHandler.LogDebug("[CUDDLEBOT] Joining Now!");
                        var UserPass = File.ReadAllText(botDirectory + "bot.txt");
                        var array = UserPass.Split('|');
                        var region = GetRegion(GetWorldRegion);
                        StartBot(botDirectory + "Area51.exe", string.Concat(array[0], "|", region, "|", getWorldID, "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|", getUserID, "|0.7|CUDDLECLI|", getClientID));
                        //StartBot(botDirectory + "Area51.exe", string.Concat(array[1], "|", region, "|", getWorldID, "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|", getUserID, "|-0.7|JoinWorld"));
                    }
                    else { LogHandler.Log(LogHandler.Colors.Green, "[CUDDLEBOT] failed to join world.\n"); LogHandler.LogDebug("[CUDDLEBOT] Failed To Join World."); }
                }
                else { LogHandler.Log(LogHandler.Colors.Red, "[CUDDLEBOT] Directory Unfound.\n"); LogHandler.LogDebug("[CUDDLEBOT] Directory Unfound!"); }
            }
            catch (Exception ex) { }
        }
        public void StartBot(string fileName, string Payload) //generates a new bot process per startbot 
        {
            var File = fileName == "" || Payload == "";//checks for bot exe runs string.Concat(array[0], "|", region, "|", getWorldID, "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|", getUserID,  "|0.7|JoinWorld")); as arg payload
            var checkFile = File;
            if (checkFile) LogHandler.Log(LogHandler.Colors.Green, "Bot files unfound\n", true);
            var process = new Process { StartInfo = { FileName = fileName, Arguments = Payload } }; //creates process with given exe and string argument from OnEnable
            process.Start();
        }
    }
} 
