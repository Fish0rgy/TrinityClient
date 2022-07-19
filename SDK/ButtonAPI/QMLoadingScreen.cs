using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Trinity.SDK.ButtonAPI
{
    internal class QMLoadingScreen
    {

        public static float HRed = 0;
        public static float HGreen = 0.438850343f;
        public static float HBlue = 0.712937f;
        public static GameObject partsystem;

        public static IEnumerator LoadingScreen()
        {
            try
            {
                //var ovcolor = new Color( HRed, HGreen, HBlue);
                var ovcolor = Color.magenta;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/Rectangle").GetComponent<Image>().color = Color.clear;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/MidRing").GetComponent<Image>().color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/InnerDashRing").GetComponent<Image>().color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ButtonMiddle").GetComponent<Button>().image.color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/RingGlow").GetComponent<Image>().color = ovcolor;
                GameObject.Find("/UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked").SetActive(false);
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ArrowRight").GetComponent<Image>().color = Color.clear;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ArrowLeft").GetComponent<Image>().color = Color.clear;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/TitleText").GetComponent<Text>().color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/ProgressLine").GetComponent<Image>().color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/StandardPopup/TitleText").GetComponent<UnityEngine.UI.Outline>().enabled = false;
                GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel").SetActive(false);
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").SetActive(false);
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles").SetActive(false);
                //GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").GetComponent<Button>().image.color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = Color.clear;
                GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = Color.clear;
                GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = Color.clear;
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = Color.green; //filling
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = Color.red; //Not Filled
                GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Button>().image.color = ovcolor;
                GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = Color.clear;
                MelonCoroutines.Start(loadingscreen());
                if (GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle") == null)
                {
                    LogHandler.Log(LogHandler.Colors.Red, "failed to customize home button", false, false);
                }
                else
                {
                    GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Button>().image.color = ovcolor;
                }  
            }
            catch
            { 
                LogHandler.Log(LogHandler.Colors.Red, "The custom loading screen failed to load", false, false);
            }

            yield return null;
        }

        private static bool isvideo = true;
        private static RenderTexture rendert;

        public static IEnumerator loadingscreen()
        {
            var isbackround = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Image>();
            var isbackround1 = GameObject.Instantiate(isbackround, isbackround.transform.parent);
            isbackround1.gameObject.name = "Video";
            var delettext = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/Video/Text");

            GameObject.Destroy(delettext);
            isbackround1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100); //0 100 425
            isbackround1.GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 425);
            isbackround1.GetComponent<RectTransform>().sizeDelta /= new Vector2(0.9f, 0.3f);
            isbackround1.GetComponent<UnityEngine.UI.Button>().enabled = false;
            isbackround1.GetComponent<RectTransform>().sizeDelta /= new Vector2(0.9f, 0.9f);

            var isbackround2 = GameObject.Instantiate(isbackround1, isbackround1.transform);
            isbackround2.name = "Backround";
            isbackround2.transform.localPosition = new Vector3(0, 0, 0);
            isbackround2.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
            var bg2img = isbackround2.GetComponent<Image>();
            bg2img.color = Color.cyan;
            MelonCoroutines.Start(loadspriterest(bg2img, "http://nocturnal-client.xyz/cl/Download/Media/just%20border.png"));




            var objb = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/Video");
            Component.Destroy(objb.GetComponent<Button>());
            Component.Destroy(objb.transform.Find("Backround").gameObject.GetComponent<Button>());

            objb.GetComponent<Image>().sprite = null;
            objb.AddComponent<UnityEngine.Video.VideoPlayer>();
            var vidcomp = objb.GetComponent<UnityEngine.Video.VideoPlayer>();
            vidcomp.isLooping = true;
            rendert = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
            rendert.Create();
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = default;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.white);

            mat.SetTexture("_EmissionMap", rendert);
            objb.GetComponent<Image>().material = mat;
            vidcomp.targetTexture = rendert;
            vidcomp.url = $"{MelonUtils.GameDirectory}\\Trinity\\Misc\\LoadingVid.mp4";
            while (GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>() == null)
                yield return null;

            //var getauds = GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>();
            //getauds.clip = null;

            vidcomp.audioOutputMode = VideoAudioOutputMode.AudioSource;
            vidcomp.EnableAudioTrack(0, false);  
            isvideo = false;
            yield return null;
        }
        public static IEnumerator loadparticles()
        {
            byte[] loadingparticles = File.ReadAllBytes($"{MelonUtils.GameDirectory}\\Trinity\\Misc\\\\loadingscreen");
            var myLoadedAssetBundle = AssetBundle.LoadFromMemory(loadingparticles);
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }
            partsystem = myLoadedAssetBundle.LoadAsset<GameObject>("ParticleLoader");
            var gmj = GameObject.Instantiate(partsystem, GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup").transform);
            gmj.transform.localPosition = new Vector3(0, 0, 8000);
            gmj.transform.Find("finished").gameObject.transform.localPosition = new Vector3(0, 0, 10000);
            gmj.transform.Find("finished/Other").gameObject.transform.localPosition = new Vector3(0, 0, 3000);
            gmj.transform.Find("middle").gameObject.transform.localPosition = new Vector3(-50, 0f, 10000);
            gmj.transform.Find("cirlce mid").gameObject.transform.localPosition = new Vector3(-673.8608f, 0, 4000);
            gmj.transform.Find("spawn").gameObject.transform.localPosition = new Vector3(800, 0, -8500f);

            foreach (var gmjs in gmj.GetComponentsInChildren<ParticleSystem>(true))
            {
                gmjs.startColor = new Color(HRed, HGreen, HBlue);
                gmjs.trails.colorOverTrail = new Color(HRed, HGreen, HBlue);
            }
            GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/3DElements").gameObject.SetActive(false);

            while (GameObject.Find("/UserInterface").transform.Find("DesktopUImanager") == null)
                yield return null;


            var toload = myLoadedAssetBundle.LoadAsset<GameObject>("Holder");

            myLoadedAssetBundle.Unload(false);
            var gmjsa = GameObject.Instantiate(toload, GameObject.Find("/UserInterface").transform.Find("DesktopUImanager").transform);
            gmjsa.transform.localPosition = new Vector3(0, 360.621f, 700);
            gmjsa.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gmjsa.transform.localScale = new Vector3(1, 1, 1);
            var p1 = gmjsa.transform.Find("Particle System").transform;
            p1.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            p1.localPosition = new Vector3(0, 64.16f, 7.2f);
            var p2 = gmjsa.transform.Find("Particle System (1)").transform;
            p2.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            p2.localPosition = new Vector3(-30.78f, -321.5403f, 8.54f);
            yield return null;
        }
        public static IEnumerator loadspriterest(Image Instance, string url)
        {

            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();
            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
                LogHandler.Log(LogHandler.Colors.Red, www.error, false, false);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            var sprite2 = Instance.sprite = Sprite.CreateSprite(content,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, Vector4.zero, false);

            if (sprite2 != null) Instance.sprite = sprite2;
        }
    }
}
 