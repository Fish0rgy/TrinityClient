using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using TMPro;
using UnityEngine;

namespace Trinity.Module
{
    class CustomNameplate : MonoBehaviour
    {
        public static bool disable = false;
        public VRC.Player player;
        private TextMeshProUGUI Nameplatext, stafftext;
        
        private byte frames, ping;
        private int noUpdateCount = 0;
        public string UserID = "";
        public bool isStaf  = false; 
        public static string rank;



        public CustomNameplate(IntPtr ptr) : base(ptr)
        {
        }

        void Start()
        {
            try
            {
                Transform ClientInfo = Instantiate(gameObject.transform.Find("Contents/Quick Stats"), gameObject.transform.Find("Contents"));
                ClientInfo.parent = gameObject.transform.Find("Contents");
                ClientInfo.name = "Trinity_nameplate";
                ClientInfo.localPosition = new Vector3(0f, 62f, 0f);
                ClientInfo.localScale = new Vector3(1f, 1f, 2f);
                ClientInfo.gameObject.SetActive(true);
                Nameplatext = ClientInfo.Find("Trust Text").GetComponent<TextMeshProUGUI>();

                Nameplatext.text = "";
                Nameplatext.color = Color.white;
                Nameplatext.fontStyle = FontStyles.Subscript;

                Nameplatext.isOverlay = false;
                //stafftext.isOverlay = false;

                ClientInfo.Find("Trust Icon").gameObject.SetActive(false);
                ClientInfo.Find("Performance Icon").gameObject.SetActive(false);
                ClientInfo.Find("Performance Text").gameObject.SetActive(false);
                ClientInfo.Find("Friend Anchor Stats").gameObject.SetActive(false);
                frames = player._playerNet.field_Private_Byte_0;
                ping = player._playerNet.field_Private_Byte_1;
                UserID = PU.GetPlayer().GetAPIUser().id;
            }
            catch
            {

            }
            //Transform Staff = Instantiate(gameObject.transform.Find("Contents/Quick Stats"), gameObject.transform.Find("Contents"));
            //Staff.parent = gameObject.transform.Find("Contents");
            //Staff.name = "Trinity_Staffplate";
            //Staff.localPosition = new Vector3(0f, 102f, 0f);
            //Staff.localScale = new Vector3(1f, 1f, 2f);
            //Staff.gameObject.SetActive(false);
            //stafftext = Staff.Find("Trust Text").GetComponent<TextMeshProUGUI>();
            //stafftext.text = null;
            //stafftext.fontStyle = FontStyles.Subscript;


             

            //Staff.Find("Trust Icon").gameObject.SetActive(false);
            //Staff.Find("Performance Icon").gameObject.SetActive(false);
            //Staff.Find("Performance Text").gameObject.SetActive(false);
            //Staff.Find("Friend Anchor Stats").gameObject.SetActive(false);



             

        }

        void Update()
        {
            try
            {
                if (frames == player._playerNet.field_Private_Byte_0 && ping == player._playerNet.field_Private_Byte_1)
                    noUpdateCount++;
                else
                    noUpdateCount = 0;

                frames = player._playerNet.field_Private_Byte_0;
                ping = player._playerNet.field_Private_Byte_1;

                 
            }
            catch
            {

            }
            string status = "<color=green>Stable</color>";

            if (noUpdateCount > 35)
                status = "<color=yellow>Lagging</color>";
            if (noUpdateCount > 375)
                status = "<color=red>Crashed</color>";

            try
            {
                //bool clientchecker = Trinity.Module.Settings.Render.CustomNameplates.Munchen.Contains(player.prop_APIUser_0.id) || Trinity.Module.Settings.Render.CustomNameplates.Arctic.Contains(player.prop_APIUser_0.id) || Trinity.Utilities.PU.ClientUserIDs.Contains(player.prop_APIUser_0.id);

                //if (player.GetIsMaster() == true && Trinity.Utilities.PU.ClientUserIDs.Contains(player.prop_APIUser_0.id))
                //{
                //    Nameplatext.text = $"[<color=blue>Host</color>] [<color=green>{player.GetPlatform()}</color>] | [{status}] |<color=white>FPS:</color> {player.GetFramesColord()} |<color=white>Ping</color>: {player.GetPingColord()}";
                //    stafftext.text = $"<color=red>Client User</color>";
                //}
                //else if (clientchecker == true)
                //{
                //    Nameplatext.text = $"[<color=green>{player.GetPlatform()}</color>] | [{status}] |<color=white>FPS:</color> {player.GetFramesColord()} |<color=white>Ping</color>: {player.GetPingColord()}";
                //    stafftext.text = $"<color=red>Client User</color>";
                //}
               if (player.GetIsMaster())
                {
                    Nameplatext.text = $"[<color=blue>Host</color>] [<color=green>{player.GetPlatform()}</color>] | [{status}] |<color=white>FPS:</color> {player.GetFramesColord()} |<color=white>Ping</color>: {player.GetPingColord()}";
                    //stafftext.gameObject.SetActive(false);
                }
                else
                {
                    Nameplatext.text = $"[<color=green>{player.GetPlatform()}</color>] | [{status}] |<color=white>FPS:</color> {player.GetFramesColord()} |<color=white>Ping</color>: {player.GetPingColord()}";
                    //stafftext.gameObject.SetActive(false);
                }
                PU.ClientDetect(player);
            }
            catch { }
             
        }
    }
}
