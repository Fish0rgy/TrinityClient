using Area51.SDK;
using System;
using TMPro;
using UnityEngine;

namespace Area51.Module
{
    class CustomNameplate : MonoBehaviour
    {
        public VRC.Player player;
        private TextMeshProUGUI Nameplatext;
        private ImageThreeSlice background;
        private byte frames, ping;
        private int noUpdateCount = 0;
        string UserID = "";




        public CustomNameplate(IntPtr ptr) : base(ptr)
        {
        }


        public Vector3 DaQMCheck()
        {
            if (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/").activeSelf == true)
            {
               return new Vector3(0f, 62f, 0f);

            }
            return new Vector3(0f, 42f, 0f);
        }


        void Start()
        {
           
            Transform Nameplate = Instantiate(gameObject.transform.Find("Contents/Quick Stats"), gameObject.transform.Find("Contents"));
            Nameplate.parent = gameObject.transform.Find("Contents");
            Nameplate.name = "Area51_nameplate";
            Nameplate.localPosition = new Vector3(0f, 62f, 0f);
            Nameplate.localScale = new Vector3(1f, 1f, 2f);
            Nameplate.gameObject.SetActive(true);
            Nameplatext = Nameplate.Find("Trust Text").GetComponent<TextMeshProUGUI>();
            Nameplatext.color = Color.white;
            Nameplatext.fontStyle = FontStyles.Subscript;

            Nameplate.Find("Trust Icon").gameObject.SetActive(false);
            Nameplate.Find("Performance Icon").gameObject.SetActive(false);
            Nameplate.Find("Performance Text").gameObject.SetActive(false);
            Nameplate.Find("Friend Anchor Stats").gameObject.SetActive(false);


            background = gameObject.transform.Find("Contents/Main/Background").GetComponent<ImageThreeSlice>();
          
            //  VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Main/Dev Circle").GetComponent<ImageThreeSlice>().color = Color.gray;
            background._sprite = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.Find("Player Nameplate/Canvas/Nameplate/Contents/Main/Dev Circle").GetComponent<ImageThreeSlice>()._sprite;        
            background.color = player.GetTrustColor();         
            frames = player._playerNet.field_Private_Byte_0;
            ping = player._playerNet.field_Private_Byte_1;
            UserID = PlayerWrapper.GetUserID;
        }

        void Update()
        {
          
            if (frames == player._playerNet.field_Private_Byte_0 && ping == player._playerNet.field_Private_Byte_1)
            {
                noUpdateCount++;
            }
            else
            {
                noUpdateCount = 0;
            }
            frames = player._playerNet.field_Private_Byte_0;
            ping = player._playerNet.field_Private_Byte_1;

            string status = "<color=green>Stable</color>";

            if (noUpdateCount > 35)
                status = "<color=yellow>Lagging</color>";
            if (noUpdateCount > 375)
                status = "<color=red>Crashed</color>";

        
            var TitleText = CustomNamePlate(UserID);
            if (player.GetIsMaster())
            {
                Nameplatext.text = $"[<color=green>{player.GetPlatform()}</color>] [<color=red>Host</color>] |[{status}] |<color=white>Ping:</color> {player.GetPingColord()} |<color=white>FPS</color>: {player.GetFramesColord()} |{TitleText}";
            }
            Nameplatext.text = $"[<color=green>{player.GetPlatform()}</color>] |[{status}] |<color=white>Ping:</color> {player.GetPingColord()} |<color=white>FPS</color>: {player.GetFramesColord()} |{TitleText}";

        }
     

        private string CustomNamePlate(string userID)
        {         
            switch (userID)
            {
               
                case "usr_6f71bbac-1a26-4d6d-b75d-376050db3c57":
                    return " [<color=green>Monkey</color>]";
                case "usr_8fa49306-0283-47c9-9d5d-8d095c3818f7":
                    return " [<color=yellow>Chickens</color>]"; 
                case "usr_81e172f3-7439-4ce1-9971-cd0bfd532725":
                    return " [<color=red>Ur Mom!</color>]";
                case "usr_3085603a-4343-46e2-afa4-c06951c56ed0":
                    return " [<color=black>:P</color>]";
                case "usr_5225280e-886a-4327-bec1-3719985e94ec":
                    return "";              
                default: return " [<color=#00FF00>Area 51</color>]";
            }
        }
    }
}
