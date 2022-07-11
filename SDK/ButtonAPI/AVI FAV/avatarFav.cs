using Il2CppSystem.Collections.Generic;
using MelonLoader;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using UnityEngine;
using MelonLoader;
using VRC.UI;
using VRC.Core;
using VRC.SDKBase; 
using VRC;
using System.Diagnostics;
using UnhollowerRuntimeLib;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Collections;
using Trinity.Utilities;
using UnityEngine.Networking;

namespace Trinity.SDK.ButtonAPI.AVI_FAV
{
    internal class avatarFav : MonoBehaviour
    {
        internal static VRCList AvatarList;
        public static System.Collections.Generic.List<APIObjects.AvatarObject> AvatarObjects = new System.Collections.Generic.List<APIObjects.AvatarObject>();
        private static bool IsClicked = false;
        private static GameObject avatarPage;
        private static GameObject objToSpawn;
        private static PageAvatar currPageAvatar;
        private static GameObject PublicAvatarList;
        public static bool QuickCheck = false;

        public avatarFav(IntPtr ptr) : base(ptr) { }
        public static void UI()
        {
            avatarPage = GameObject.Find("UserInterface/MenuContent/Screens/Avatar");
            PublicAvatarList = GameObject.Find("/UserInterface/MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content/Public Avatar List");
            currPageAvatar = avatarPage.GetComponent<PageAvatar>();
            GameObject instanciate = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button").gameObject, GameObject.Find("UserInterface/MenuContent/Screens/Avatar").transform);
            instanciate.GetComponentInChildren<RectTransform>().localPosition = new Vector3(instanciate.transform.localPosition.x + 200, instanciate.transform.localPosition.y, instanciate.transform.localPosition.z);
            bool clientcheck = File.Exists("Mods\\MunchenClientLoader.dll");


            GameObject WCLoadButton = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button").gameObject, GameObject.Find("UserInterface/MenuContent/Screens/Avatar").transform);
            if (clientcheck)
            {
                WCLoadButton.GetComponentInChildren<RectTransform>().localPosition = new Vector3(316.162f, 290.876f, -2.0366f);//
                WCLoadButton.GetComponentInChildren<RectTransform>().localScale = new Vector3(0.7891f, 0.9309f, 1f);
            } 
            else
                WCLoadButton.GetComponentInChildren<RectTransform>().localPosition = new Vector3(583.7398f, 373.8442f, -2.0366f);//

            PageAvatar pageAvatar = GameObject.Find("UserInterface/MenuContent/Screens/Avatar").transform.GetComponentInChildren<PageAvatar>();
            MoveButtons();
            Button WCFavButtonVar = instanciate.GetComponent<Button>();
            Button WCLoadButtonVar = WCLoadButton.GetComponent<Button>();
            WCLoadButtonVar.onClick.RemoveAllListeners();
            WCLoadButtonVar.transform.Find("Horizontal/FavoritesCountSpacingText").gameObject.SetActive(false);
            WCLoadButtonVar.transform.Find("Horizontal/FavoritesCurrentCountText").gameObject.SetActive(false);
            WCLoadButtonVar.transform.Find("Horizontal/FavoritesCountDividerText").gameObject.SetActive(false);
            WCLoadButtonVar.transform.Find("Horizontal/FavoritesMaxAvailableText").gameObject.SetActive(false);
            WCLoadButtonVar.GetComponentInChildren<Text>().text = "Reload Avatars";
            WCLoadButtonVar.gameObject.SetActive(true);
            WCLoadButtonVar.name = "Reload Avatars"; 
            WCLoadButtonVar.onClick.AddListener(new System.Action(() =>
            {
                MelonCoroutines.Start(RefreshMenu(0.3f));
                //ConsoleLogger.CLog.Log("Reloaded List!", false, ConsoleColor.Magenta);
            }));
            WCFavButtonVar.transform.Find("Horizontal/FavoritesCountSpacingText").gameObject.SetActive(false);
            WCFavButtonVar.transform.Find("Horizontal/FavoritesCurrentCountText").gameObject.SetActive(false);
            WCFavButtonVar.transform.Find("Horizontal/FavoritesCountDividerText").gameObject.SetActive(false);
            WCFavButtonVar.transform.Find("Horizontal/FavoritesMaxAvailableText").gameObject.SetActive(false);
            WCFavButtonVar.GetComponentInChildren<Text>().text = "Favorite/UnFav";
            WCFavButtonVar.gameObject.SetActive(true);
            WCFavButtonVar.name = "Fav Avatar";
            WCFavButtonVar.onClick.RemoveAllListeners();
            WCFavButtonVar.gameObject.transform.localPosition = new Vector3(602.9193f, 289.609f, -2.0366f);
            WCFavButtonVar.gameObject.transform.localScale = new Vector3(0.7891f, 0.9309f, 1f);
            //Controls if an avatar is added to/removed from the favorites
            WCFavButtonVar.onClick.AddListener(new System.Action(() =>
            {
                if (!AvatarObjects.Exists(m => m.id == currPageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0.id))
                {
                    FavoriteAvatar(currPageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0);
                }
                else
                {
                    UnfavoriteAvatar(currPageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0);
                }
                MelonCoroutines.Start(RefreshMenu(0.3f));
            }));
            AvatarList = new VRCList(PublicAvatarList.transform.parent, "Trinity Favorites", 0);
            AvatarList.GameObject.GetComponent<UiAvatarList>().field_Public_Category_0 = UiAvatarList.Category.Internal;
            AvatarObjects = JsonConvert.DeserializeObject<System.Collections.Generic.List<APIObjects.AvatarObject>>(File.ReadAllText("Trinity\\AvatarConfig.json"));
            GameObject changeintoavatar = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button");
            GameObject detailsbutton = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/AvatarDetails Button");
            changeintoavatar.transform.localPosition = new Vector3(-561.1137f, -352.4222f, -2.0047f);
            detailsbutton.transform.localPosition = new Vector3(-565.3154f, -416.8459f, -2.0047f);
            //ConsoleLogger.CLog.Log("[AvatarFavs] Avatars Loaded!", false, ConsoleColor.Gray);
        }//  
        public static GameObject NewFavButton(Vector3 Poz, Quaternion Rot, Vector3 Scale, string name, bool Ison, Action listener)
        {
            GameObject ButtonPre = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button").gameObject, GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Extra Avatar Buttons").transform);
            ButtonPre.GetComponentInChildren<RectTransform>().localPosition = Poz;
            ButtonPre.GetComponentInChildren<RectTransform>().localScale = Scale;
            ButtonPre.GetComponentInChildren<RectTransform>().localRotation = Rot;
            Button ButtonPreVar = ButtonPre.GetComponent<Button>();
            ButtonPreVar.transform.Find("Horizontal/FavoritesCountSpacingText").gameObject.SetActive(false);
            ButtonPreVar.transform.Find("Horizontal/FavoritesCurrentCountText").gameObject.SetActive(false);
            ButtonPreVar.transform.Find("Horizontal/FavoritesCountDividerText").gameObject.SetActive(false);
            ButtonPreVar.transform.Find("Horizontal/FavoritesMaxAvailableText").gameObject.SetActive(false);
            ButtonPreVar.GetComponentInChildren<Text>().text = name;
            ButtonPreVar.gameObject.SetActive(true);
            ButtonPreVar.name = name;
            ButtonPreVar.onClick.RemoveAllListeners();
            //Controls if an avatar is added to/removed from the favorites
            ButtonPre.GetComponent<Button>().onClick.AddListener(listener);
            return ButtonPre;
        }

        public static void MoveButtons()
        {
            GameObject checkfav = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button");
            if (checkfav)
            {
                checkfav.GetComponentInChildren<RectTransform>().localPosition = new Vector3(-561.1137f, -411.4602f, -2.0047f); //-561.1137 -411.4602 -2.0047
            }
            GameObject checkChange = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Change Button");
            if (checkChange)
            {
                checkChange.GetComponentInChildren<RectTransform>().localPosition = new Vector3(-561f, -289.3963f, -2f); // -561 -289.3963 -2
            }
        }
        public static IEnumerator RefreshMenu(float v)
        {
            yield return new WaitForSeconds(v);
            var avilist = new Il2CppSystem.Collections.Generic.List<ApiAvatar>();
            AvatarObjects.ForEach(avi => avilist.Add(avi.ToApiAvatar()));
            AvatarList.RenderElement(avilist);
            yield break;
        }

        internal static void FavoriteAvatar(ApiAvatar avatar)
        {
            if (!AvatarObjects.Exists(avi => avi.id == avatar.id))
                AvatarObjects.Insert(0, new APIObjects.AvatarObject(avatar));
            MelonCoroutines.Start(RefreshMenu(1f));
            string contents = JsonConvert.SerializeObject(AvatarObjects, Formatting.Indented);
            File.WriteAllText("Trinity\\AvatarConfig.json", contents);
        }
        internal static void UnfavoriteAvatar(ApiAvatar avatar)
        {
            if (AvatarObjects.Exists(avi => avi.id == avatar.id))
                AvatarObjects.Remove(AvatarObjects.Where(avi => avi.id == avatar.id).FirstOrDefault());

            MelonCoroutines.Start(RefreshMenu(1f));
            string contents = JsonConvert.SerializeObject(AvatarObjects, Formatting.Indented);
            File.WriteAllText("Trinity\\AvatarConfig.json", contents);
        }
    }
}
