

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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


namespace Trinity.SDK
{
    static class PlayerWrapper
    {
        //converted all the bs to one lines to clean the class
        private static GameObject avatarPreviewBase, playernode;
        private static int noUpdateCount = 0;
        public static string backupID = "";
        public static bool isRestored = false;
        public static Player LocalPlayer => Player.prop_Player_0;
        public static string GetUserID => GetAPIUser(LocalPlayer).id;
        public static string GetWorldID => GetAPIUser(LocalPlayer).location;
        public static string GetinstanceID => GetAPIUser(LocalPlayer).instanceId;
        public static bool GetIsMaster(this Player Instance) => Instance.GetVRCPlayerApi().isMaster;
        public static USpeaker GetUspeaker(this Player player) => player.prop_USpeaker_0;
        public static int GetActorNumber2(this Player player) => player.GetVRCPlayerApi().playerId;
        public static string GetName(this Player player) => player.GetAPIUser().displayName;
        public static Player GetByUsrID(string usrID) => GetAllPlayers().First(x => x.prop_APIUser_0.id == usrID);
        public static Dictionary<int, VRC.Player> PlayersActorID = new Dictionary<int, VRC.Player>();
        public static Player[] GetAllPlayers() => PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0;
        public static void Teleport(this Player player) => LocalVRCPlayer.transform.position = player.prop_VRCPlayer_0.transform.position;
        public static void TeleportLocation(float x, float y, float z) => LocalVRCPlayer.transform.position = new Vector3(x, y, z);
        public static APIUser SpoofDisplayname(string name) => (APIUser)(LocalPlayer.field_Private_APIUser_0._displayName_k__BackingField = name);
        public static VRCPlayer LocalVRCPlayer => VRCPlayer.field_Internal_Static_VRCPlayer_0;
        public static APIUser GetAPIUser(this VRC.Player player) => player.prop_APIUser_0;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;
        public static bool ZeroPingFPS(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 && player.GetFrames() <= -1;
        public static bool VectorZero(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.transform.position == Vector3.zero;
        public static IUser GetSelectedUser(this SelectedUserMenuQM selectMenu) => selectMenu.field_Private_IUser_0;
        public static Player GetPlayer(this VRCPlayer player) => player.prop_Player_0;
        public static T DecryptData<T>(Il2CppSystem.Object customdata) => FormatArray<T>(SerializeArray(customdata));
        public static int GetPhotonID(this VRC.Player player) => player.prop_Int32_0;
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
        public static APIUser GetAPIUser(this VRCPlayer Instance) => Instance.GetPlayer().GetAPIUser();
        public static VRCPlayerApi GetVRCPlayerApi(this Player Instance) => Instance?.prop_VRCPlayerApi_0;
        public static List<Player> AllPlayers => PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0.ToList<Player>();
        public static Player GetPlayer(int ActorNumber) => (from p in AllPlayers where p.GetActorNumber2() == ActorNumber select p).FirstOrDefault<Player>();
        public static GameObject[] GetAllGameObjects() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        public static int GetActorNumber(this Player player) => player.GetVRCPlayerApi() != null ? player.GetVRCPlayerApi().playerId : -1;
        public static void SetHide(this Player Instance, bool State) { Instance.transform.Find("ForwardDirection").gameObject.active = !State; }
        public static GameObject GetAvatarPreviewBase() => avatarPreviewBase = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/AvatarPreviewBase");
       
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static bool IsBot(this Player player) => player.GetPing() <= 0 && player.GetFrames() <= 0 || player.GetFrames() <= -1 || player.transform.position == Vector3.zero;
        public static Player SelectedVRCPlayer() => GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0.prop_String_0.ReturnUserID();



        public static void ShowSelf(bool state)
        {
            backupID = APIUser.CurrentUser.avatarId;
            GetAvatarPreviewBase().SetActive(state);
            LocalVRCPlayer.prop_VRCAvatarManager_0.gameObject.SetActive(state);
            AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0.gameObject.SetActive(state);
        }

        public static GameObject GetPlayerMirrFix()
        {
            foreach (GameObject objectName in GetAllGameObjects())
            {
                bool IsMirror = objectName.name.StartsWith("_AvatarMirrorClone");
                if (IsMirror)
                {
                    return objectName;
                }
            }
            return new GameObject();
        }

        public static GameObject GetPlayerMirrFix2()
        {
            foreach (GameObject objectName in GetAllGameObjects())
            {
                bool IsAvatar = objectName.name.StartsWith("_AvatarShadowClone");
                if (IsAvatar)
                {
                    return objectName;
                }
            }
            return new GameObject();
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
            LogHandler.Log(LogHandler.Colors.Green, "Crashing World...", false, false);
            yield return new WaitForSecondsRealtime(0.07f);
            isRestored = true;
        }


        public static int IsAssetBundleFileTooLarge(VRC.Player player)
        {
            VRC.ValidationHelpers.CheckIfAssetBundleFileTooLarge(VRC.ContentType.Avatar, player.prop_ApiAvatar_0.assetUrl, out int fileSize);
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
        public static VRCUiPopupManager GetVRCUiPopupManager() { return VRCUiPopupManager.prop_VRCUiPopupManager_0; }
        public static void AlertPopup(this VRCUiPopupManager manager, string title, string text) => manager.Method_Public_Void_String_String_Single_0(title, text, 10f);

        public static void ShowInputKeyBoard(string title,string placeholder,Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> InputAction)
        {
            VRCUiPopupManager vrcpopup = GetVRCUiPopupManager();
            vrcpopup.field_Public_VRCUiPopupInput_1.gameObject.SetActive(true);
            vrcpopup.field_Public_VRCUiPopupInput_1.Method_Public_Void_String_InputType_String_Action_3_String_List_1_KeyCode_Text_Boolean_PDM_0(title, InputField.InputType.Standard, placeholder, InputAction, true);
            GameObject.Find("UserInterface/MenuContent/Popups/InputKeypadPopup").SetActive(true);
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
                    LogHandler.Log(LogHandler.Colors.Red, "Error while executing delegate:\n" + ex.ToString(), false, false);
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



        public static string LogRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType)
        {
            string output = "[RPC] ";
            if (sender != null) { output += sender.prop_APIUser_0.displayName + " sended "; } else { output += " INVISABLE sended "; }
            output += vrcBroadcastType + " ";
            output += vrcEvent.Name + " ";
            output += vrcEvent.EventType + " ";

            if (vrcEvent.ParameterObject != null)
            {
                output += vrcEvent.ParameterObject.name + " ";
                output += vrcEvent.ParameterBool + " ";
                output += vrcEvent.ParameterBoolOp + " ";
                output += vrcEvent.ParameterFloat + " ";
                output += vrcEvent.ParameterInt + " ";
                output += vrcEvent.ParameterString + " ";
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
        internal static void Lewdify(this GameObject avatar)
        {
            bool NotValid = avatar == null;
            if (!NotValid)
            {
                foreach (Transform transform in avatar.GetAllTransforms(true))
                {
                    bool flag2 = Oldlist.Contains(transform.gameObject.name.ToLower());
                    if (flag2)
                    {
                        bool flag3 = transform.GetComponent<MeshRenderer>() || transform.GetComponent<SkinnedMeshRenderer>();
                        if (flag3)
                        {
                            UnityEngine.Object.DestroyImmediate(transform.gameObject);
                        }
                    }
                }
            }
        }
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
