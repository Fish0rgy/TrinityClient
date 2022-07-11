using UnityEngine;
using UnityEngine.UI;
using VRC.Core;

namespace Trinity.SDK.ButtonAPI.AVI_FAV
{
    internal class VRCList
	{
        public VRCList(Transform transform, string avi, int pos = 0)
        {
            GameObject = UnityEngine.Object.Instantiate(PublicAvatarList.gameObject, transform);
            UiVRCList = GameObject.GetComponent<UiVRCList>();
            Text = GameObject.transform.Find("Button").GetComponentInChildren<Text>();
            GameObject.transform.SetSiblingIndex(pos);

            UiVRCList.clearUnseenListOnCollapse = false;
            UiVRCList.usePagination = false;
            UiVRCList.hideElementsWhenContracted = false;
            UiVRCList.hideWhenEmpty = false;
            UiVRCList.field_Protected_Dictionary_2_Int32_List_1_ApiModel_0.Clear();


            GameObject.SetActive(true);
            GameObject.name = avi;
            Text.text = avi;
        }

        public GameObject GameObject;
        public UiVRCList UiVRCList;
        public Text Text;
        public void RenderElement(Il2CppSystem.Collections.Generic.List<ApiAvatar> AvatarList)
        {
            UiVRCList.Method_Protected_Void_List_1_T_Int32_Boolean_VRCUiContentButton_0<ApiAvatar>(AvatarList, 0, true);
        }

        private static GameObject PublicAvatarList = GameObject.Find("/UserInterface/MenuContent/Screens/Avatar/Vertical Scroll View/Viewport/Content/Public Avatar List");
    }
}
