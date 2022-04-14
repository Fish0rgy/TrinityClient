using System;
using System.Diagnostics;
using System.IO;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.SDK.Security;
using Trinity.Utilities;

namespace Trinity.Module.Bot.Local
{
    class CuddleBot1 : BaseModule
    {
        public CuddleBot1() : base("Start\nBot One", "Bots Join World", Main.Instance.Privatebotbutton, Trinity.SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.bot1)) { } //Button
        public static string[] Regions { get { return new[] { "usw", "eu", "jp" }; } } //Creates world region strings
        public static string GetWorldRegion => RoomManager.field_Internal_Static_ApiWorldInstance_0.region.ToString(); //Grabs World region from RoomManager
        private static string GetRegion(string input) { switch (input) { case "Europe": return "eu"; case "US_East": return "us"; case "US_West": return "usw"; default: return "usw"; } } //Gets world region extention from input
        public override void OnEnable()
        {
            try
            {
                var BotExists = !Directory.Exists("\\Trinity\\Bot\\"); //flags for bot directory
                var BotCheck = BotExists;
                if (BotCheck) //checks if bot directory exists
                {
                    var getWorldID = PU.GetPlayer().GetAPIUser().worldId;             //WorldID
                    var getinstanceID = PU.GetPlayer().GetAPIUser().instanceId;          //InstanceID
                    var getUserID = PU.GetPlayer().GetAPIUser().id;              //UserID
                    var getClientID = SecurityCheck.ExploitData.Key;
                    var botDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Trinity\\Bot\\"; //makes string for bot directory
                    var exeExists = File.Exists(botDirectory + "Trinity.exe"); //creates flag for Trinity.exe exsistence
                    var exeCheck = exeExists;
                    if (exeCheck)
                    {
                        LogHandler.Log(LogHandler.Colors.Green, "[CUDDLEBOT] is joining world.\n"); LogHandler.LogDebug("[CUDDLEBOT] Joining Now!");
                        var UserPass = File.ReadAllText(botDirectory + "bot.txt");
                        var array = UserPass.Split('|');
                        var region = GetRegion(GetWorldRegion);
                        StartBot(botDirectory + "Trinity.exe", string.Concat(array[0], "|", region, "|", getWorldID, "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|", getUserID, "|0.7|CUDDLECLI|", getClientID));
                        //StartBot(botDirectory + "Trinity.exe", string.Concat(array[1], "|", region, "|", getWorldID, "|avtr_c4961195-1980-4a98-bb95-3cbe0e063463|", getUserID, "|-0.7|JoinWorld"));
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
