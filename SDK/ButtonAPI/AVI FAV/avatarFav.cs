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
        // worldclient fav thanks hacker for the code
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
            


            GameObject LoadButton = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button").gameObject, GameObject.Find("UserInterface/MenuContent/Screens/Avatar").transform);
            if (Misc.ModCheck("MunchenClientLoader"))
            {
                LoadButton.GetComponentInChildren<RectTransform>().localPosition = new Vector3(316.162f, 290.876f, -2.0366f);//
                LoadButton.GetComponentInChildren<RectTransform>().localScale = new Vector3(0.7891f, 0.9309f, 1f);
            } 
            else
                LoadButton.GetComponentInChildren<RectTransform>().localPosition = new Vector3(583.7398f, 373.8442f, -2.0366f);//

            PageAvatar pageAvatar = GameObject.Find("UserInterface/MenuContent/Screens/Avatar").transform.GetComponentInChildren<PageAvatar>();
            MoveButtons();
            Button FavButtonVar = instanciate.GetComponent<Button>();
            Button LoadButtonVar = LoadButton.GetComponent<Button>();
            LoadButtonVar.onClick.RemoveAllListeners();
            LoadButtonVar.transform.Find("Horizontal/FavoritesCountSpacingText").gameObject.SetActive(false);
            LoadButtonVar.transform.Find("Horizontal/FavoritesCurrentCountText").gameObject.SetActive(false);
            LoadButtonVar.transform.Find("Horizontal/FavoritesCountDividerText").gameObject.SetActive(false);
            LoadButtonVar.transform.Find("Horizontal/FavoritesMaxAvailableText").gameObject.SetActive(false);
            LoadButtonVar.GetComponentInChildren<Text>().text = "Reload Avatars";
            LoadButtonVar.gameObject.SetActive(true);
            LoadButtonVar.name = "Reload Avatars"; 
            LoadButtonVar.onClick.AddListener(new System.Action(() =>
            {
                MelonCoroutines.Start(RefreshMenu(0.3f));
            }));
            FavButtonVar.transform.Find("Horizontal/FavoritesCountSpacingText").gameObject.SetActive(false);
            FavButtonVar.transform.Find("Horizontal/FavoritesCurrentCountText").gameObject.SetActive(false);
            FavButtonVar.transform.Find("Horizontal/FavoritesCountDividerText").gameObject.SetActive(false);
            FavButtonVar.transform.Find("Horizontal/FavoritesMaxAvailableText").gameObject.SetActive(false);
            FavButtonVar.GetComponentInChildren<Text>().text = "Favorite/UnFav";
            FavButtonVar.gameObject.SetActive(true);
            FavButtonVar.name = "Fav Avatar";
            FavButtonVar.onClick.RemoveAllListeners();
            FavButtonVar.gameObject.transform.localPosition = new Vector3(602.9193f, 289.609f, -2.0366f);
            FavButtonVar.gameObject.transform.localScale = new Vector3(0.7891f, 0.9309f, 1f); 
            FavButtonVar.onClick.AddListener(new System.Action(() =>
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
            ButtonPre.GetComponent<Button>().onClick.AddListener(listener);
            return ButtonPre;
        }

        public static void MoveButtons()
        {
            GameObject checkfav = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Favorite Button");
            if (checkfav)
            {
                    checkfav.GetComponentInChildren<RectTransform>().localPosition = new Vector3(-561.1137f, -411.4602f, -2.0047f);
            }
            GameObject checkChange = GameObject.Find("UserInterface/MenuContent/Screens/Avatar/Change Button");
            if (checkChange)
            {
                if (!Main.CompLayer)
                    checkChange.GetComponentInChildren<RectTransform>().localPosition = new Vector3(-561f, -289.3963f, -2f);
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
