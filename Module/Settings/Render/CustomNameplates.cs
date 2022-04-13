using Trinity.Events;
using Trinity.SDK;
using System;
using System.Collections.Generic;

namespace Trinity.Module.Settings.Render
{
    internal class CustomNameplates : BaseModule, OnPlayerJoinEvent
    {
        public VRC.Player player = new VRC.Player();
        public static List<string> Users = new List<string>();
        public static List<string> Staff = new List<string>();
        public static List<string> Aliens = new List<string>();
        
        public CustomNameplates() : base("Nameplates", "Cool Kids Nameplate", Main.Instance.SettingsButtonrender, null, true, false)
        {
        }

        public override void OnEnable()
        {
          
            Main.Instance.OnPlayerJoinEvents.Add(this);
            UpdatePlayerlistInfo();
        }

        public override void OnDisable()
        {
          
            Main.Instance.OnPlayerJoinEvents.Remove(this);

        }

        public void OnPlayerJoin(VRC.Player player)
        {
            CustomNameplate nameplate = player.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
            nameplate.player = player;
            var userid = player.prop_APIUser_0.id;
           // var user = StaffCheck(player.prop_APIUser_0.id);
            if (StaffCheck(userid) == true)
            {
                Staff.Add(userid);
                LogHandler.Log(LogHandler.Colors.Green, "Staff:" + player.prop_APIUser_0.displayName + "\nUserID:" + player.prop_APIUser_0.id + "\nUpdated Player Info");
            }
            Users.Add(userid);
        }

        public void OnPlayerLeft(VRC.Player player)
        {
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            try
            {
                Staff.Remove(player.prop_APIUser_0.id);
                Users.Remove(player.prop_APIUser_0.id);

            }
            catch { }
        }

        public void UpdatePlayerlistInfo()
        {
            try
            {

                for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
                {
                    VRC.Player player = PlayerWrapper.GetAllPlayers()[i];
                    CustomNameplate nameplate = player.transform.Find("Player Nameplate/Canvas/Nameplate").gameObject.AddComponent<CustomNameplate>();
                    nameplate.player = player;
                    var userid = player.prop_APIUser_0.id;
                    if(StaffCheck(userid) == true)
                    {
                        Staff.Add(userid);
                        LogHandler.Log(LogHandler.Colors.Green, "Staff:" + player.prop_APIUser_0.displayName + "\nUserID:" + player.prop_APIUser_0.id + "\nUpdated Player Info");
                    }
                    Users.Add(userid);
                  //  LogHandler.Log(LogHandler.Colors.Green, "User:" + player.prop_APIUser_0.displayName + "\nUserID:" + player.prop_APIUser_0.id + "\nUpdated Player Info");
                    if (i >= PlayerWrapper.GetAllPlayers().Length)
                    {
                        break;
                    }
                   
                }
            }
            catch (Exception ERROR)
            {
                LogHandler.Log(LogHandler.Colors.Green, ERROR.Message, false, false);
            }
        }

        public bool StaffCheck(string userID)
        {
            switch (userID)
            {
                case "usr_308873ae-6ae8-48af-9082-c93c7335dbd3":
                    return true; //(true, "<color=#BF40BF>Trinity Developer</color>"); //new Tuple<bool, string>
                case "usr_81aada0b-1b42-46e1-968b-d5688ed92f0a":
                    return true; //(true, "");
                case "usr_8fa49306-0283-47c9-9d5d-8d095c3818f7":
                    return true;//(true, "");
                case "usr_23866f18-773c-47c7-a930-90d322ce97ff":
                    return true;//(true, "");
                case "usr_93cf2938-e1cc-4395-855b-8ad98e955daf":
                    return true;//(true, "");
                case "usr_e63de320-f426-41a2-9afb-cc5ac1c6aeed":
                    return true;//(true, "");
                case "usr_2538ccb2-d331-43ab-ac59-3226a0997038":
                    return true;//(true, "<color=#BF40BF>Trinity Discord Support</color>");
                default: return false; //(false, "VRC User"); ;
            }
        }
    }
}
