using ExitGames.Client.Photon;
using MelonLoader;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Trinity.SDK;
using Trinity.SDK.Photon;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Animation;
using VRC.Core; 
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI;
using VRC.UI.Elements.Menus;

namespace Trinity.Utilities 
{
    static class PU
    {
        //converted all the bs to one lines to clean the class

        private static PageUserInfo InfoUser;
        public static Player GetPlayer() => Player.prop_Player_0;
        public static Player GetPlayer(int ActorNumber) => (from p in AllPlayers2().Array.ToList() where p.GetActorNumber2() == ActorNumber select p).FirstOrDefault<Player>();
        public static VRCPlayer GetVRCPlayer() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static Player[] GetAllPlayers() => PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
        public static Player GetByUsrID(string usrID) => GetAllPlayers().ToList().First(x => x.prop_APIUser_0.id == usrID); 
        public static Player SelectedVRCPlayer() => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0.prop_String_0.ReturnUserID();
        public static IUser SelectedIUserPlayer() => UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0;
        public static void TeleportLocation(float x, float y, float z) => GetVRCPlayer().transform.position = new Vector3(x, y, z); 
        private static GameObject avatarPreviewBase => GameObject.Find("UserInterface/MenuContent/Screens/Avatar/AvatarPreviewBase");
        private static GameObject playernode; 
        private static int noUpdateCount = 0;
        public static string backupID = "";
        public static bool isRestored = false;
        public static bool GetIsMaster(this Player Instance) => Instance.GetVRCPlayerApi().isMaster;
        public static USpeaker GetUspeaker(this Player player) => player.prop_USpeaker_0;
        public static int GetActorNumber2(this Player player) => player.GetVRCPlayerApi().playerId;
        public static string GetName(this Player player) => player.GetAPIUser().displayName; 
        public static Dictionary<int, VRC.Player> PlayersActorID = new Dictionary<int, VRC.Player>();
        public static List<string> ClientUserIDs = new List<string>();
        public static void Teleport(this Player player) => GetVRCPlayer().transform.position = player.prop_VRCPlayer_0.transform.position; 
        public static APIUser GetAPIUser(this VRC.Player player) => player.prop_APIUser_0;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;
        public static bool ZeroPingFPS(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 && player.GetFrames() <= -1;
        public static bool VectorZero(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.transform.position == Vector3.zero;
        public static IUser GetSelectedUser(this SelectedUserMenuQM selectMenu) => selectMenu.field_Private_IUser_0;
        public static Player GetPlayer(this VRCPlayer player) => player.prop_Player_0;
        public static Player[] GetPlayers(this PlayerManager playerManager) => playerManager.field_Private_List_1_Player_0.ToArray(); 
        public static int GetPhotonID(this VRC.Player player) => player.prop_Int32_0;
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
        public static APIUser GetAPIUser(this VRCPlayer Instance) => Instance.GetPlayer().GetAPIUser();
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance) => Instance?.prop_VRCPlayerApi_0;
        public static VRC.Core.Pool.PooledArray<Player> AllPlayers2() => PlayerManager.prop_PlayerManager_0.prop_PooledArray_1_Player_0;
        public static VRCPlayer GetVRCPlayer(this Player Instance) => (Instance == null) ? null : Instance._vrcplayer; 
        public static VRCAvatarManager GetAvatarManager(this VRCPlayer Instance) =>  Instance.prop_VRCAvatarManager_0;
        public static GameObject GetAvatar(this Player Instance) => Instance.GetVRCPlayer().GetAvatarManager().transform.gameObject;
        public static void ReloadAvatar(this VRCPlayer Instance) => VRCPlayer.Method_Public_Static_Void_APIUser_0(Instance.GetAPIUser());
        public static void ReloadAvatar(this Player Instance) => Instance.GetVRCPlayer().ReloadAvatar();
        public static int GetActorNumber(this Player player) => player.GetVRCPlayerApi() != null ? player.GetVRCPlayerApi().playerId : -1;
        public static void SetHide(this Player Instance, bool State) { Instance.transform.Find("ForwardDirection").gameObject.active = !State; }
        public static bool GetIsFriend(this APIUser Instance) =>  Instance.isFriend || APIUser.IsFriendsWith(Instance.id) || APIUser.CurrentUser.friendIDs.Contains(Instance.id); 
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static bool IsBot(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.GetFrames() <= -1 || player.transform.position == Vector3.zero;
        internal static int PlayerbyteValue(VRC.Player player) => (int)(1000f / (float)player.prop_PlayerNet_0.field_Private_Byte_0);



        public static Player GetPlayer(this PlayerManager playerManager, int actorNr)
        {
            foreach (var player in playerManager.GetPlayers())
            {
                if (player == null)
                    continue;
                if (player.prop_Int32_0 == actorNr)
                    return player;
            }

            return null;
        }
        internal static PageUserInfo SocialInfo()
        {

            InfoUser = GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").GetComponent<PageUserInfo>();
            return InfoUser;
        }
        public static void ShowSelf(bool state)
        {
            backupID = APIUser.CurrentUser.avatarId;
            avatarPreviewBase.SetActive(state);
            GetVRCPlayer().prop_VRCAvatarManager_0.gameObject.SetActive(state);
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(state);
        }
        internal static PI GetLocalPlayerInformation()
        {
            if (PlayerUtils.localPlayerInfo == null)
            {
                if (PlayerUtils.playerCachingList.ContainsKey(APIUser.CurrentUser.displayName) == true)
                {
                    PlayerUtils.localPlayerInfo = PlayerUtils.playerCachingList[APIUser.CurrentUser.displayName];

                    return PlayerUtils.playerCachingList[APIUser.CurrentUser.displayName];
                }

                return null;
            }

            return PlayerUtils.localPlayerInfo;
        }
        internal static PI GetPlayerInformation(Player player)
        {
            string displayName = string.Empty;

            if (player != null)
            {
                if (player.prop_APIUser_0 != null)
                {
                    displayName = player.prop_APIUser_0.displayName;
                }
                else if (player.prop_VRCPlayerApi_0 != null)
                {
                    displayName = player.prop_VRCPlayerApi_0.displayName;
                }
            }

            if (displayName == string.Empty)
            {
                return null;
            }

            if (displayName == APIUser.CurrentUser.displayName)
            {
                return GetLocalPlayerInformation();
            }

            if (PlayerUtils.playerCachingList.ContainsKey(displayName) == true)
            {
                return PlayerUtils.playerCachingList[displayName];
            }

            return null;
        }
        internal static void Delay(float del, Action action) => MelonCoroutines.Start(DelayFunc(del, action));
        private static System.Collections.IEnumerator DelayFunc(float del, Action action)
        {
            yield return new WaitForSeconds(del);
            action.Invoke();
            yield break;
        }
        internal static string Params2JSON(ParameterDictionary paramDict)
        {
            var p = new Dictionary<byte, object>();
            foreach (var kvp in paramDict)
                p[kvp.Key] = Serialization.FromIL2CPPToManaged<object>(kvp.Value);

            return JsonConvert.SerializeObject(p);
        }
        public static GameObject GetPlayerMirrFix()
        {
            foreach (GameObject objectName in WU.GetAllGameObjects())
            {
                if (objectName.name.StartsWith("_AvatarMirrorClone"))
                {
                    return objectName;
                }
            }
            return new GameObject();
        }

        public static GameObject GetPlayerMirrFix2()
        {
            foreach (GameObject objectName in WU.GetAllGameObjects())
            {
                if (objectName.name.StartsWith("_AvatarShadowClone"))
                {
                    return objectName;
                }
            }
            return new GameObject();
        } 
        internal static void ReloadAllAvatars()
        {
            bool LocalCheck = VRCPlayer.field_Internal_Static_VRCPlayer_0 == null;
            if (!LocalCheck)
            {
                foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
                {
                    bool NullCheck = player != null && player.prop_APIUser_0 != null;
                    if (NullCheck)
                    {
                        player.ReloadAvatar();
                    }
                }
            }
        }
        public static void ClearAssets()
        {
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Cache_0.ClearCache();
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Queue_1_AssetBundleDownload_0.Clear();
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Queue_1_AssetBundleDownload_1.Clear();
        }
        public static Player ReturnUserID(this string User)
        {
            foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player.field_Private_APIUser_0.id == User)
                {
                    return player;
                }
            }
            return null;
        }
        public static IEnumerator SetCrasher(string crasherid)
        {
            backupID = APIUser.CurrentUser.avatarId;
            ShowSelf(false);
            yield return new WaitForSecondsRealtime(0.01f);
            ChangeAvatar(crasherid);
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Crashing World...", false, false);
            yield return new WaitForSecondsRealtime(0.07f);
            isRestored = true;
        }


        public static int IsAssetBundleFileTooLarge(VRC.Player player)
        {
            VRC.ValidationHelpers.CheckIfAssetBundleFileTooLarge(VRC.ContentType.Avatar, player.prop_ApiAvatar_0.assetUrl, out int fileSize , false);
            return fileSize;
        }
        public static byte[] SerializeArray(Il2CppSystem.Object customdata)
        {
            if (customdata != null)
            {
                Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                Il2CppSystem.IO.MemoryStream array = new Il2CppSystem.IO.MemoryStream();
                format.Serialize(array, customdata);
                return array.ToArray();
            }
            return null;
        }
        public static T FormatArray<T>(byte[] data)
        {
            T results;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (System.IO.MemoryStream array = new System.IO.MemoryStream(data))
            {
                results = (T)((object)format.Deserialize(array));
            }
            return results;
        }
        public static string CrashDetected(this Player player)
        {
            float frames = player._playerNet.field_Private_Byte_0;
            short ping = player._playerNet.field_Private_Byte_1;
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
                return status = "<color=yellow>Lagging</color>";
            if (noUpdateCount > 375)
                return status = "<color=red>Crashed</color>";
            return status;

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
        public static void AddClientUsers()
        {
            var lines = System.IO.File.ReadLines($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\ClientUsers.txt");
            foreach (var line in lines)
            {
                ClientUserIDs.Add(line);
            }
        }
        public static bool isflying(VRC.Player player)
        {
            bool zero = player.prop_VRCPlayerApi_0.GetVelocity().Equals(0);
            if (player.prop_VRCPlayerApi_0.IsPlayerGrounded() && !zero)
                return true;
            return false;
        }
        public static bool checkcontroller(VRC.Player player)
        {
            bool gay = false;
            try
            {
                player.GetComponentsInChildren<CharacterController>().ToList().ForEach(s =>
                {
                    if (s.enabled == false)
                        gay = true;
                });
            }
            catch (Exception ex)
            {

            }
            return gay;
        }
        public static bool ClientDetect(this Player player)
        {
            bool checkfile = System.IO.File.ReadLines($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\ClientUsers.txt").Any(line => line.Contains(player.prop_APIUser_0.id));
            float fps = player.GetFrames();
            short ping = player.GetPing();
            bool redFlags = ping > 300 || ping < -2 || fps > 100 || fps < -2 || ControllerCheck(player.GetVRCPlayer()) == true || player.transform.localPosition.y < -10 ||  FakeFreezeServerTime(player) == true;
            if (redFlags == true)
            {
                if (!ClientUserIDs.Contains(player.prop_APIUser_0.id))
                {
                    if (player.prop_APIUser_0.IsOnMobile == true)
                        return false;
                    ClientUserIDs.Add(player.prop_APIUser_0.id);
                    MenuUI.Log($"DETECTOR: <color=green>{player.prop_APIUser_0.displayName} Is A Client User</color>");
                    if(!checkfile)
                        System.IO.File.AppendAllText($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\ClientUsers.txt", $"{player.prop_APIUser_0.id}{Environment.NewLine}"); 
                }
                return true;
            }
            else
                return false;
        }
        public static bool FakeFreezeServerTime(Player player)
        {
            try
            {
                float frames = player._playerNet.field_Private_Byte_0;
                short ping = player._playerNet.field_Private_Byte_1;
                float pos = player.transform.localPosition.x;

                if (player == null) return false;
                if (frames == player._playerNet.field_Private_Byte_0 && ping == player._playerNet.field_Private_Byte_1 && pos != player.transform.localPosition.x)
                    noUpdateCount++;
                else
                    noUpdateCount = 0;

                if (noUpdateCount > 200)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
             
        }
        public static void BlockStateChanged(object player, bool isBlocked)
        {
            if (isBlocked)
            {

            }
        }
        public static bool ControllerCheck(VRCPlayer player)
        {
            try
            {
                if (player.GetComponent<CharacterController>().enabled == false)
                    return true;
            }
            catch
            {
                return false;
            } 
            return false;
        }
        public static void MuteStateChanged(object player, bool isMuted)
        {

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

        public static Player GetPlayerWithPlayerID(int playerID)
        {
            for (int i = 0; i < GetAllPlayers().Length; i++)
            {
                if (GetAllPlayers().ToList()[i].prop_VRCPlayerApi_0.playerId == playerID)
                {
                    return GetAllPlayers().ToList()[i];
                }
            }
            return null;
        }
        public static VRCUiPopupManager GetVRCUiPopupManager() { return VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0; }
        public static void AlertPopup(this VRCUiPopupManager manager, string title, string text) => manager.Method_Public_Void_String_String_Single_0(title, text, 10f);

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
                    Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, "Error while executing delegate:\n" + ex.ToString(), false, false);
                }
            }
        }

        public static void ChangeAvatar(string AvatarID)
        {
            PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
            component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar { id = AvatarID };
            component.ChangeToSelectedAvatar();
        }

        public static Player GetPlayerByActorID(int actorId)
        {
            VRC.Player player = null;
            PlayersActorID.TryGetValue(actorId, out player);
            return player;
        }

        public static string LogTagRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType)
        {
            string output = "[TAG] ";

            if (vrcEvent.ParameterObject != null)
            {
                output += $"{vrcEvent.ParameterString}";
            }

            if (vrcEvent.ParameterObjects != null) { for (int i = 0; i < vrcEvent.ParameterObjects.Length; i++) { output += vrcEvent.ParameterObjects[i].name + " "; } }
            try
            {
                var objects = Networking.DecodeParameters(vrcEvent.ParameterBytes);
                for (int i = 0; i < objects.Length; i++) { output += Il2CppSystem.Convert.ToString(objects[i]) + " "; }
            }
            catch { for (int i = 0; i < vrcEvent.ParameterBytes.Length; i++) { output += vrcEvent.ParameterBytes[i] + " "; } }

            return output;
        }
        internal static Solution Il2CppConverter(int ByteArray)
        {
            using (Dictionary<string, Solution>.Enumerator enumerator = ConvertedArray.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, Solution> eventKey = enumerator.Current;
                    if (eventKey.Value.NTB.prop_Int32_0 == ByteArray)
                        return eventKey.Value;
                }
                goto returnNull;
            }
            Solution result;
            return result;
        returnNull:
            return null;
        }
        public static string LogRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType)
        {
            string output = "[RPC] ";
            if (sender != null)
            {
                output += $"{sender.prop_APIUser_0.displayName} sended"; 
            } 
            else
            {
                output += " INVISABLE sended "; 
            }

            output += $"{vrcBroadcastType} " +
                $"{vrcEvent.Name} " +
                $"{vrcEvent.EventType}";

            if (vrcEvent.ParameterObject != null)
            {
                output += $"{vrcEvent.ParameterObject.name} " +
                    $"{vrcEvent.ParameterBool} " +
                    $"{vrcEvent.ParameterBool} " +
                    $"{vrcEvent.ParameterBoolOp} " +
                    $"{vrcEvent.ParameterFloat} " +
                    $"{vrcEvent.ParameterInt} " +
                    $"{vrcEvent.ParameterString}";
            }

            if (vrcEvent.ParameterObjects != null) { for (int i = 0; i < vrcEvent.ParameterObjects.Length; i++) { output += vrcEvent.ParameterObjects[i].name + " "; } }
            try
            {
                var objects = Networking.DecodeParameters(vrcEvent.ParameterBytes);
                for (int i = 0; i < objects.Length; i++) { output += Il2CppSystem.Convert.ToString(objects[i]) + " "; }
            }
            catch { for (int i = 0; i < vrcEvent.ParameterBytes.Length; i++) { output += vrcEvent.ParameterBytes[i] + " "; } }

            return output;
        } 
        public static void ProcessDynamicBones(GameObject avatarObject, APIUser user)
        {
            bool flag = (SDK.Config.Instance.GB_Friends && !user.GetIsFriend() && user.id != APIUser.CurrentUser.id);
            if (!flag)
            {
                bool gb_HeadBones = SDK.Config.Instance.GB_HeadBones;
                if (gb_HeadBones)
                {
                    foreach (DynamicBone item in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform((HumanBodyBones)10).GetComponentsInChildren<DynamicBone>())
                    {
                        SDK.Config.currentWorldDynamicBones.Add(item);
                    }
                }
                bool gb_ChestBones =SDK.Config.Instance.GB_ChestBones;
                if (gb_ChestBones)
                {
                    foreach (DynamicBone item2 in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform((HumanBodyBones)8).GetComponentsInChildren<DynamicBone>())
                    {
                        SDK.Config.currentWorldDynamicBones.Add(item2);
                    }
                }
                bool gb_HipBones = SDK.Config.Instance.GB_HipBones;
                if (gb_HipBones)
                {
                    foreach (DynamicBone item3 in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform(0).GetComponentsInChildren<DynamicBone>())
                    {
                        SDK.Config.currentWorldDynamicBones.Add(item3);
                    }
                }
                bool gb_HandColliders = SDK.Config.Instance.GB_HandColliders;
                if (gb_HandColliders)
                {
                    foreach (DynamicBoneCollider dynamicBoneCollider in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform((HumanBodyBones)17).GetComponentsInChildren<DynamicBoneCollider>())
                    {
                        bool flag2 = dynamicBoneCollider.m_Bound != null;
                        if (flag2)
                        {
                            SDK.Config.currentWorldDynamicBoneColliders.Add(dynamicBoneCollider);
                        }
                    }
                    foreach (DynamicBoneCollider dynamicBoneCollider2 in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform((HumanBodyBones)18).GetComponentsInChildren<DynamicBoneCollider>())
                    {
                        bool flag3 = dynamicBoneCollider2.m_Bound != null;
                        if (flag3)
                        {
                            SDK.Config.currentWorldDynamicBoneColliders.Add(dynamicBoneCollider2);
                        }
                    }
                }
                bool gb_FeetColliders = SDK.Config.Instance.GB_FeetColliders;
                if (gb_FeetColliders)
                {
                    foreach (DynamicBoneCollider dynamicBoneCollider3 in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform((HumanBodyBones)5).GetComponentsInChildren<DynamicBoneCollider>())
                    {
                        bool flag4 = dynamicBoneCollider3.m_Bound != null;
                        if (flag4)
                        {
                            SDK.Config.currentWorldDynamicBoneColliders.Add(dynamicBoneCollider3);
                        }
                    }
                    foreach (DynamicBoneCollider dynamicBoneCollider4 in avatarObject.GetComponentInChildren<Animator>().GetBoneTransform((HumanBodyBones)6).GetComponentsInChildren<DynamicBoneCollider>())
                    {
                        bool flag5 = dynamicBoneCollider4.m_Bound != null;
                        if (flag5)
                        {
                            SDK.Config.currentWorldDynamicBoneColliders.Add(dynamicBoneCollider4);
                        }
                    }
                }
                foreach (DynamicBone dynamicBone in SDK.Config.currentWorldDynamicBones.ToList<DynamicBone>())
                {
                    bool flag6 = dynamicBone == null;
                    if (flag6)
                    {
                        SDK.Config.currentWorldDynamicBones.Remove(dynamicBone);
                    }
                    else
                    {
                        foreach (DynamicBoneCollider dynamicBoneCollider5 in SDK.Config.currentWorldDynamicBoneColliders.ToList<DynamicBoneCollider>())
                        {
                            bool flag7 = dynamicBoneCollider5 == null;
                            if (flag7)
                            {
                                SDK.Config.currentWorldDynamicBoneColliders.Remove(dynamicBoneCollider5);
                            }
                            bool flag8 = dynamicBone.m_Colliders.IndexOf(dynamicBoneCollider5) == -1;
                            if (flag8)
                            {
                                dynamicBone.m_Colliders.Add(dynamicBoneCollider5);
                            }
                        }
                    }
                }
            }
        }
        public static void AddFakeColliders(Player selectedPlayer)
        {
            Animator componentInChildren = selectedPlayer.GetComponentInChildren<Animator>();
            Transform boneTransform = componentInChildren.GetBoneTransform((HumanBodyBones)17);
            Transform boneTransform2 = componentInChildren.GetBoneTransform((HumanBodyBones)18);
            bool flag = boneTransform == null || boneTransform2 == null;
            if (!flag)
            {
                bool flag2 = boneTransform.GetComponent<DynamicBoneCollider>() != null || boneTransform2.GetComponent<DynamicBoneCollider>() != null;
                if (!flag2)
                {
                    Transform boneTransform3 = componentInChildren.GetBoneTransform((HumanBodyBones)32);
                    Transform boneTransform4 = componentInChildren.GetBoneTransform((HumanBodyBones)47);
                    Transform boneTransform5 = componentInChildren.GetBoneTransform((HumanBodyBones)24);
                    Transform boneTransform6 = componentInChildren.GetBoneTransform((HumanBodyBones)48);
                    float height = 0.007f;
                    float height2 = 0.007f;
                    float num = 0.0009f;
                    float num2 = (float)Math.Pow(10.0, 2.0);
                    bool flag3 = boneTransform5 != null && boneTransform6 != null;
                    if (flag3)
                    {
                        num = Mathf.Floor(Vector3.Distance(boneTransform5.position, boneTransform6.position) * num2) / num2 / 1000f;
                        num += num / 4f;
                    }
                    bool flag4 = boneTransform3 != null;
                    if (flag4)
                    {
                        height = Mathf.Floor(Vector3.Distance(boneTransform.position, boneTransform3.position) * num2) / num2 / 100f + num * 4f;
                    }
                    bool flag5 = boneTransform4 != null;
                    if (flag5)
                    {
                        height2 = Mathf.Floor(Vector3.Distance(boneTransform2.position, boneTransform4.position) * num2) / num2 / 100f + num * 4f;
                    } 
                    SDK.LogHandler.Log(SDK.LogHandler.Colors.Yellow, $"[DynamicBones] Collider stats: {height.ToString()}, {height2.ToString()}, {num.ToString()}", false,false);
                    DynamicBoneCollider dynamicBoneCollider = boneTransform.gameObject.AddComponent<DynamicBoneCollider>();
                    DynamicBoneCollider dynamicBoneCollider2 = boneTransform2.gameObject.AddComponent<DynamicBoneCollider>();
                    dynamicBoneCollider.m_Radius = num * 12f;
                    dynamicBoneCollider.m_Height = height;
                    dynamicBoneCollider.m_Center = new Vector3(0f, 0f, 0f);
                    dynamicBoneCollider.m_Bound = 0;
                    dynamicBoneCollider2.m_Radius = num * 12f;
                    dynamicBoneCollider2.m_Height = height2;
                    dynamicBoneCollider2.m_Center = new Vector3(0f, 0f, 0f);
                    dynamicBoneCollider2.m_Bound = 0; 
                    SDK.LogHandler.Log(SDK.LogHandler.Colors.Yellow, $"[DynamicBones] Added fake colliders to {selectedPlayer.GetAPIUser().displayName}", false, false);
                }
            }
        }
        public static void RemoveDynamicBones(GameObject avatarObject, APIUser user)
        {
            bool flag = (SDK.Config.Instance.GB_Friends && !user.GetIsFriend() && user.id != APIUser.CurrentUser.id);
            if (!flag)
            {
                List<DynamicBone> list = new List<DynamicBone>();
                Il2CppSystem.Collections.Generic.List<DynamicBoneCollider> list2 = new Il2CppSystem.Collections.Generic.List<DynamicBoneCollider>();
                foreach (DynamicBone dynamicBone in avatarObject.GetComponentInChildren<Animator>().GetComponentsInChildren<DynamicBone>())
                {
                    list.Add(dynamicBone);
                }
                foreach (DynamicBoneCollider dynamicBoneCollider in avatarObject.GetComponentInChildren<Animator>().GetComponentsInChildren<DynamicBoneCollider>())
                {
                    bool flag2 = dynamicBoneCollider.m_Bound != null;
                    if (flag2)
                    {
                        list2.Add(dynamicBoneCollider);
                    }
                }
                foreach (DynamicBone dynamicBone2 in list)
                {
                    bool flag3 = dynamicBone2 == null;
                    if (!flag3)
                    {
                        SDK.Config.currentWorldDynamicBones.Remove(dynamicBone2);
                        dynamicBone2.m_Colliders = list2;
                    }
                }
                foreach (DynamicBoneCollider dynamicBoneCollider2 in list2)
                {
                    bool flag4 = dynamicBoneCollider2 != null;
                    if (flag4)
                    {
                        SDK.Config.currentWorldDynamicBoneColliders.Remove(dynamicBoneCollider2);
                    }
                }
            }
        }
        public delegate void ResetLastPositionAction(InputStateController @this);
        public delegate void ResetAction(VRCMotionState @this);

        private static ResetLastPositionAction ourResetLastPositionAction;
        private static ResetAction ourResetAction;

        public static ResetLastPositionAction ResetLastPositionAct
        {
            get
            {
                if (ourResetLastPositionAction != null)
                {
                    return ourResetLastPositionAction;
                }
                MethodInfo method = typeof(InputStateController).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public).Single((MethodInfo it) => XrefScanner.XrefScan(it).Any((XrefInstance jt) => jt.Type == XrefType.Method && jt.TryResolve() != null && jt.TryResolve().Name == "get_transform"));
                ourResetLastPositionAction = (ResetLastPositionAction)System.Delegate.CreateDelegate(typeof(ResetLastPositionAction), method);
                return ourResetLastPositionAction;
            }
        }

        public static void ResetLastPosition(this InputStateController instance)
        {
            ResetLastPositionAct(instance);
        }
        public static List<Transform> GetAllTransforms(this GameObject g, bool getHidden = true)
        {
            List<Transform> TransformList = new List<Transform>();
            Transform[] TransformList2 = g.GetComponents<Transform>();
            Transform[] ShownList = g.GetComponentsInChildren<Transform>(getHidden);
            int Transformers = TransformList2.Length;
            int ShownAvis = ShownList.Length;
            for (int i = 0; i < ShownAvis; i++)
            {
                bool Exist = !TransformList.Contains(TransformList2[i]);
                if (Exist)
                {
                    TransformList.Add(TransformList2[i]);
                }
            }
            for (int j = 0; j < ShownAvis; j++)
            {
                bool Shown = !TransformList.Contains(ShownList[j]);
                if (Shown)
                {
                    TransformList.Add(ShownList[j]);
                }
            }
            return TransformList;
        }

        public static void Lewdify(this GameObject avatar)
        {
            if (avatar == null) return;

            foreach (Transform transform in avatar.GetAllTransforms(true))
            {
                if (!Oldlist.Contains(transform.gameObject.name.ToLower())) continue;

                if (transform.GetComponent<MeshRenderer>() || transform.GetComponent<SkinnedMeshRenderer>())
                {
                    UnityEngine.Object.DestroyImmediate(transform.gameObject);
                }

            }

        } 
        internal static readonly Dictionary<string, Solution> ConvertedArray;

        public static List<string> Oldlist = new List<string>
        {
            "cloth", "shirt", "pant", "under", "undi", "jacket", "top", "bra", "skirt", "jean",
            "trouser", "boxers", "hoodi", "bottom", "dress", "bandage", "bondage", "sweat", "cardig", "corset",
            "tiddy", "pastie", "suit", "stocking", "jewel", "frill", "gauze", "cover", "pubic", "sfw",
            "harn", "biki", "outfit", "panties", "short", "clothing", "shirt top", "pasties", "inv_swimsuit", "pants",
            "shoes", "underclothes", "shorts", "Hoodie", "plaster", "pussy cover", "radialswitch", "ribbon", "bottom1", "shorts nsfw",
            "top nsfw", "pastie+harness", "bralette harness", "bottom2", "robe", "rope", "ropes", "ropes", "lingerie toggle", "sandals",
            "shirt.001", "skrt", "sleeve", "sleeves", "snapdress", "socks", "tank", "stickers", "denimtop_b", "fish nets",
            "chest harness", "stockings", "straps", "strapsbottom", "body suit", "sweater", "swimsuit", "tank top", "tape", "shirt dress",
            "tearsweater", "thong", "toob", "toppants", "rf mask top", "longshirt", "asphalttop", "hood", "sweatshirt", "uppertop",
            "toggle top.001", "jacket.002", "underwear", "undies", "tokyohoodie", "wraps", "wrap", "outerwear", "wraps-top", "Одежка",
            "sticker", "dressy", "capeyyy", "bodysuity", "bodysuit", "верх", "низ", "パンティー", "ビキニ", "ブラジャー",
            "下着", "무녀복", "브라", "비키니", "속옷", "젖소", "gasmask", "팬티", "skirt.001", "huku_top",
            "other_glasses", "other_mask", "huku_pants", "huku_skirt", "huku_jacket", "clothes", "top_mesh", "kemono", "garterbelt", "langerie",
            "tap", "calça", "camisa", "beziercircle.001", "dress.001", "floof corset", "paisties", "string and gatter", "crop top", "panty",
            "sleeveless", "harness", "pantie", "bandaid", "mask", "chainsleeve", "hat", "hoodoff", "hoodon", "metal muzzle",
            "top2", "rush", "huku_bra", "huku_lace shirt", "huku_panties", "huku_shoes", "huku_shorts", "o_harness", "o_mask", "bottoms",
            "daddys slut", "bra.strapless", "butterfly dress", "chainnecklace", "denim shorts", "panties_berryvee", "tanktop", "waist jacket", "chocker_jhp", "brazbikini_bottoms",
            "brazbikini_top", "full harness", "glasses", "panty_misepan", "top1", "top3", "top4", "top5_bottom", "top5_top", "top6",
            "eraser", "bikini", "headset", "screen", "就是一个胡萝卜", "chain", "hesopi", "merino_scarf", "merino_bag", "bikini bottoms",
            "merino_panties", "tsg_buruma", "merino_cap", "kyoueimizugi", "kyoueimizugi_oppaiooki", "leotard", "hotpants", "hotpants_side_open", "merino_culottes", "merino_leggins",
            "merino_socks", "bikini", "merino_bra", "merino_jacket", "merino_inner", "tsg_shirt", "beer hat", "cuffs", "lace", "panties",
            "pasties", "shorts and shoes", "undergarments", "irukanicar", "ベルト", "wear", "tshirt", "waistbag", "nekomimicasquette", "dango",
            "penetrator", "comfy bottom", "comfy top", "hoodie", "strawberry panty", "strawberry top", "vest", "sleevedtop", "baggy top by cupkake", "harness by heyblake",
            "heart pasties by cupkake", "straps!", "crop strap hoodie flat", "harness & panties", "bunnycostume", "handwarmers", "belt", "cardigan", "turtle neck", "bandages",
            "holysuit", "nipplecovers"
        };
    }
}
class Solution
{

    internal VRCNetworkBehaviour NTB;
}