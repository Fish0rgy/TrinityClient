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
    }
}
