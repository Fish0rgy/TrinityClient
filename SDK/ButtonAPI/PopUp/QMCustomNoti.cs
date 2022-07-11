using System.Collections;
using System.IO;
using System.Linq;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.SDK.ButtonAPI.PopUp
{
    internal class QMCustomNoti
    {
        public static GameObject uia; 
        public static CanvasGroup leCanvasGroup;
        public static Text Msg;
        public static Text TitleText;
        public static readonly float DurationOnScreen = 2f; //duration of how long till fade off
        public static readonly float DurationOfFade = 2f; // how long should alpha of canvas group to turn from 0 to 1

        internal static void SetUp()
        {
            byte[] assetarray = File.ReadAllBytes("Trinity\\Misc\\join");
            var myLoadedAssetBundle = AssetBundle.LoadFromMemory(assetarray);
            if (myLoadedAssetBundle == null)
            {
                LogHandler.Log(LogHandler.Colors.Red, "Failed to load AssetBundle!", false, false); 
            }
            var toinst = myLoadedAssetBundle.LoadAsset<GameObject>("TrinityNoti");
            myLoadedAssetBundle.Unload(false);
            var ToastBoi = GameObject.Instantiate(toinst, GameObject.Find("/UserInterface").transform.Find("UnscaledUI/HudContent").transform);
            ToastBoi.name = "ToastBoi";

            ToastBoi.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -804);
            Object.Destroy(ToastBoi.transform.Find("SensitivitySlider").gameObject);
            Object.Destroy(ToastBoi.transform.Find("InvertedMouse").gameObject); 
            leCanvasGroup = ToastBoi.AddComponent<CanvasGroup>();
            leCanvasGroup.alpha = 0f;

            TitleText = ToastBoi.transform.Find("TitleText").GetComponent<Text>();
            TitleText.alignment = TextAnchor.MiddleCenter;

            GameObject Message = ToastBoi.transform.Find("MouseSensitivityText").gameObject;
            Message.name = "Message";
            Message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -32);
            Message.GetComponent<RectTransform>().sizeDelta = new Vector2(430, 100);
            Msg = Message.GetComponent<Text>();
            Msg.alignment = TextAnchor.MiddleCenter;

        }
        internal static IEnumerator setuplog()
        {
            byte[] assetarray = File.ReadAllBytes("Trinity\\Misc\\join");
            var myLoadedAssetBundle = AssetBundle.LoadFromMemory(assetarray);
            if (myLoadedAssetBundle == null)
            {
                LogHandler.Log(LogHandler.Colors.Red,"Failed to load AssetBundle!",false,false);
                yield break;
            }
            var toinst = myLoadedAssetBundle.LoadAsset<GameObject>("TrinityNoti");
            myLoadedAssetBundle.Unload(false);
            var instanciate = GameObject.Instantiate(toinst, GameObject.Find("/UserInterface").transform.Find("UnscaledUI/HudContent").transform);
            instanciate.name = "Trinity";
            Component.Destroy(instanciate.GetComponent<Canvas>());
            Component.Destroy(instanciate.GetComponent<UnityEngine.UI.CanvasScaler>());
            Component.Destroy(instanciate.GetComponent<UnityEngine.UI.GraphicRaycaster>());
            Misc.reset(instanciate);
            instanciate.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            uia = instanciate.transform.Find("TrinityNoti").gameObject;
            instanciate.transform.Find("TrinityNoti/Image").gameObject.transform.localPosition = new Vector3(0, -330, 0); 
        }
        public static IEnumerator Fade(CanvasGroup zeCanvasgroup, float startAlpha, float endAlpha, float fadeDuration)
        {
            float startTime = Time.time;
            float alpha = startAlpha;

            if (fadeDuration > 0f)
                while (alpha != endAlpha)
                {
                    alpha = Mathf.Lerp(startAlpha, endAlpha, (Time.time - startTime) / fadeDuration);
                    zeCanvasgroup.alpha = alpha;

                    yield return null;
                }

            zeCanvasgroup.alpha = endAlpha;
        }
    }
}
