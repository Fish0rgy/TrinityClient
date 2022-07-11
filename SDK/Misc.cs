using Trinity.Utilities;
using Il2CppSystem.Threading;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Microsoft.Win32;
using System.Collections.Generic;
using Trinity.SDK.ButtonAPI;
using MelonLoader;
using Trinity.SDK.ButtonAPI.PopUp;
using VRC.SDKBase;
using Trinity.Module;

namespace Trinity.SDK
{
    public static class Misc
    { 
        public static string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!§$%&/()=?";
            string s = "";
            System.Random rand = new System.Random();
            for (int i = 0; i < length; i++)
            {
                s += chars[rand.Next(chars.Length - 1)];
            }
            return s;
        }

        //sorry was lazy https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
        internal static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        internal static string GetClipboard()
        {
            if (Clipboard.ContainsText())
            {
                return Clipboard.GetText();
            }
            return "";
        }


        public static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }


        internal static void SetClipboard(string Set)
        {
            if (Clipboard.ContainsText())
            {
                Clipboard.Clear();
                Clipboard.SetText(Set);
            }
            Clipboard.SetText(Set);

        }
        //https://github.com/BlackHyrax/Loading-Screen-Music/blob/main/LoadingScreenMusic/MyMod.cs
        private static AudioClip audiofile;
        private static AudioSource audiosource;
        private static AudioSource audiosource2;
        public static IEnumerator LoadingMusic()
        {
            // I used some code of Knah´s join notifier for the unitywebrequest.


            var uwr = UnityWebRequest.Get($"file://{Path.Combine(Environment.CurrentDirectory, "Trinity/LoadingScreenMusic/Music.ogg")}");
            uwr.SendWebRequest();

            while (!uwr.isDone) yield return null;

            audiofile = WebRequestWWW.InternalCreateAudioClipUsingDH(uwr.downloadHandler, uwr.url, false, false, AudioType.UNKNOWN);
            audiofile.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            while (audiosource == null)
            {
                audiosource = GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound")?.GetComponent<AudioSource>();

                yield return null;
            }
            audiosource.clip = audiofile;
            audiosource.Stop();
            audiosource.Play();

            while (audiosource2 == null)
            {
                audiosource2 = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/LoadingSound")?.GetComponent<AudioSource>();

                yield return null;
            }
            audiosource2.clip = audiofile;
            audiosource2.Stop();
            audiosource2.Play();
            try
            {
                GameObject.Find("UserInterface/MenuContent/Backdrop/Backdrop/Background").active = false;
                //GameObject.Find("UserInterface/MenuContent/Backdrop/Backdrop/Background").GetComponent<Image>().color = Color.magenta;
                MelonCoroutines.Start(QMLoadingScreen.LoadingScreen());
            }
            catch
            {
            }
            //MelonCoroutines.Start(QMLoadingScreen.loadparticles());

        }
        public static void reset(GameObject gameobject)
        {
            gameobject.transform.localPosition = new Vector3(0, 0, 0);
            gameobject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gameobject.transform.localScale = new Vector3(1, 1, 1);
        }
        public static IEnumerator nameplates()
        {
            foreach (var bg in Resources.FindObjectsOfTypeAll<ImageThreeSlice>())
            {
                if (bg.name == "Background" && bg.transform.parent.name == "Main")
                    MelonCoroutines.Start(Misc.loadImageThreeSlice(bg, "https://nocturnal-client.xyz/cl/Download/Media/Nameplate.png"));
            }
            yield break;
        }
        public static void SetToggleState(BaseModule module, bool toggleOn, bool shouldInvoke = false)
        { 
            //module.SetToggleState(toggleOn);
            //module.SetToggleState(!toggleOn);

            //try
            //{
            //    if (toggleOn == true && shouldInvoke == true)
            //    {
            //        btnOnAction.Invoke();

            //        showWhenOn.ForEach(x => x.SetActive(true));
            //        hideWhenOn.ForEach(x => x.SetActive(false));
            //    }
            //    else if (toggleOn == false && shouldInvoke == true)
            //    {
            //        btnOffAction.Invoke();

            //        showWhenOn.ForEach(x => x.SetActive(false));
            //        hideWhenOn.ForEach(x => x.SetActive(true));
            //    }
            //}
            //catch (Exception e)
            //{
            //    LogHandler.Error(e);
            //}
        }
        public static void StartEnumerator(this IEnumerator e)
        {
            MelonCoroutines.Start(e);
        }
        public static string GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
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
                //Style.Consoles.consolelogger("Error4 : " + www.error);
                LogHandler.Log(LogHandler.Colors.Red,www.error,false,false);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            var sprite2 = Instance.sprite = Sprite.CreateSprite(content,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, Vector4.zero, false);

            if (sprite2 != null) Instance.sprite = sprite2;
        }
        public static IEnumerator WaitForLocalPlayer()
        {
            while (VRCPlayer.field_Internal_Static_VRCPlayer_0 == null)
                yield return null;
            QMCustomNoti.SetUp();
        }
        internal class BufferRW
        {
            public static byte[] Vector3ToBytes(Vector3 vector3)
            {
                byte[] buffer = new byte[12];
                Buffer.BlockCopy(BitConverter.GetBytes(vector3.x), 0, buffer, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(vector3.y), 0, buffer, 4, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(vector3.z), 0, buffer, 8, 4);
                return buffer;
            }

            public static Vector3 ReadVector3(byte[] buffer, int index)
            {
                var x = BitConverter.ToSingle(buffer, index);
                var y = BitConverter.ToSingle(buffer, index + 4);
                var z = BitConverter.ToSingle(buffer, index + 8);
                return new Vector3(x, y, z);
            }
        }
        public static IEnumerator loadImageThreeSlice(ImageThreeSlice Instance, string url)
        {
            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();
            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
                LogHandler.Log(LogHandler.Colors.Red, www.error,false, false);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            var sprite2 = Instance._sprite = Sprite.CreateSprite(content,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, new Vector4(255, 0, 255, 0), false);
            if (sprite2 != null) Instance._sprite = sprite2;
        }
    }
}
