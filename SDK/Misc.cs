using System.Net;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

namespace Area51.SDK
{
    class Misc
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

        internal static void SetClipboard(string Set)
        {
            if (Clipboard.ContainsText())
            {
                Clipboard.Clear();
                Clipboard.SetText(Set);
            }
            Clipboard.SetText(Set);

        }
    }
}
