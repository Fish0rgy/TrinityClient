using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace Area51.SDK
{
    static class PlayerWrapper
    {

        //converted all the bs to one lines to clean the class
        
        private static VRCPlayer Local_Player() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        private static GameObject avatarPreviewBase;
        private static int noUpdateCount = 0;

        public static string backupID = "";
        public static GameObject GetAvatarPreviewBase() => avatarPreviewBase = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/AvatarPreviewBase");
        public static Dictionary<int, VRC.Player> PlayersActorID = new Dictionary<int, VRC.Player>();
        public static Player[] GetAllPlayers() => PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0;
        public static string GetUserID => GetAPIUser(LocalPlayer).id;
        public static string GetWorldID => GetAPIUser(LocalPlayer).location;
        public static string GetinstanceID => GetAPIUser(LocalPlayer).instanceId;
        public static Player GetByUsrID(string usrID) => GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
        public static void Teleport(this Player player) => LocalVRCPlayer.transform.position = player.prop_VRCPlayer_0.transform.position;
        public static Player LocalPlayer => Player.prop_Player_0;
        public static VRCPlayer LocalVRCPlayer => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static APIUser GetAPIUser(this VRC.Player player) => player.prop_APIUser_0;
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;
        public static bool IsBot(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.GetFrames() <= -1 || player.transform.position == Vector3.zero;
        public static bool ZeroPingFPS(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 && player.GetFrames() <= -1;
        public static bool VectorZero(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.transform.position == Vector3.zero;
        public static IUser GetSelectedUser(this SelectedUserMenuQM selectMenu) => selectMenu.field_Private_IUser_0;
        public static Player GetPlayer(this VRCPlayer player) => player.prop_Player_0;
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
        public static APIUser GetAPIUser(this VRCPlayer Instance) => Instance.GetPlayer().GetAPIUser();
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance) => Instance?.prop_VRCPlayerApi_0;
        public static bool GetIsMaster(this Player Instance) => Instance.GetVRCPlayerApi().isMaster;
        public static USpeaker GetUspeaker(this Player player) => player.prop_USpeaker_0;   
        public static int GetActorNumber2(this Player player) => player.GetVRCPlayerApi().playerId;
        public static string GetName(this Player player) => player.GetAPIUser().displayName;
        public static List<Player> AllPlayers => PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0.ToList<Player>();

     

        public static void ShowSelf(bool state)
        {
            GetAvatarPreviewBase().SetActive(state);
            Local_Player().prop_VRCAvatarManager_0.gameObject.SetActive(state);
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(state);
          
        }

        public static void ClearAssets()
        {
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Cache_0.ClearCache();
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Queue_1_AssetBundleDownload_0.Clear();
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Queue_1_AssetBundleDownload_1.Clear();
        }

        public static Player GetPlayer(int ActorNumber)
        {
            return (from p in AllPlayers
                    where p.GetActorNumber2() == ActorNumber
                    select p).FirstOrDefault<Player>();
        }

     

        public static int CrashDetected(this Player player)
        {

            byte frames = player._playerNet.field_Private_Byte_0;
            byte ping = player._playerNet.field_Private_Byte_1;


            if (frames == player._playerNet.field_Private_Byte_0 && ping == player._playerNet.field_Private_Byte_1)
            {
                noUpdateCount++;

            }
            else
            {
                noUpdateCount = 0;
            }

            return noUpdateCount;


        }


        public static void Tele2MousePos()
        {
            Ray posF = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //pos, directon 
            RaycastHit[] PosData = Physics.RaycastAll(posF);
            if (PosData.Length > 0) { RaycastHit pos = PosData[0]; VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position = pos.point; }
        }
        public static string GetFramesColord(this Player player)
        {
            float fps = player.GetFrames();
            if (fps > 80)
                return "<color=green>" + fps + "</color>";
            else if (fps > 30)
                return "<color=yellow>" + fps + "</color>";
            else
                return "<color=red>" + fps + "</color>";
        } 
        public static string ClientDetect(this Player player)
        {
            float fps = player.GetFrames();
            short ping = player.GetPing();
            if (ping > 665)
                return " <color=red>ClientUser</color>";
            else if (ping < -2)
                return " <color=red>ClientUser</color>";
            else if (fps > 140)
                return " <color=red>ClientUser</color>";
            else if (fps < -2)
                return " <color=red>ClientUser</color>";
            return "";
        }
        public static string GetPingColord(this Player player)
        {
            short ping = player.GetPing();
            if (ping > 150)
                return "<color=red>" + ping + "</color>";
            else if (ping > 75)
                return "<color=yellow>" + ping + "</color>";
            else
                return "<color=green>" + ping + "</color>";
        }

        public static string GetPlatform(this Player player)
        {
            if (player.prop_APIUser_0.IsOnMobile)
            {
                return "<color=green>Q</color>";
            }
            else if (player.prop_VRCPlayerApi_0.IsUserInVR())
            {
                return "<color=#CE00D5>VR</color>";
            }
            else
            {
                return "<color=grey>PC</color>";
            }
        }

        public static int GetActorNumber(this Player player)
        {
            return player.GetVRCPlayerApi() != null ? player.GetVRCPlayerApi().playerId : -1;
        }

        public static void SetHide(this VRCPlayer Instance, bool State)
        {
            Instance._player.SetHide(State);
          
        }

        public static Player GetPlayerWithPlayerID(int playerID)
        {
            for (int i = 0; i < GetAllPlayers().Length; i++)
            {
                if (GetAllPlayers()[i].prop_VRCPlayerApi_0.playerId == playerID)
                {
                    return GetAllPlayers()[i];
                }
            }

            return null;
        }

        public static void DelegateSafeInvoke(this Delegate @delegate, params object[] args)
        {
            Delegate[] invocationList = @delegate.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                try
                {
                    invocationList[i].DynamicInvoke(args);
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, "Error while executing delegate:\n" + ex.ToString(), false, false);
                }
            }
        }

        public static void SetHide(this Player Instance, bool State)
        {
            Instance.transform.Find("ForwardDirection").gameObject.active = !State;
        }

        public static void ChangeAvatar(string AvatarID)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
            {
                id = AvatarID
            };
            component.ChangeToSelectedAvatar();
        }

        public static Player GetPlayerByActorID(int actorId)
        {
            VRC.Player player = null;
            PlayersActorID.TryGetValue(actorId, out player);
            return player;
        } 
    }
}
