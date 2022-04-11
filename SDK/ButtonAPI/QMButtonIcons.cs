using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Area51.SDK.ButtonAPI
{
    internal class QMButtonIcons
    {
        internal static Texture2D CreateTextureFromBase64(string data)
        {
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, Convert.FromBase64String(data));
            texture.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            return texture;
        }

        private static string ImageToBase64(string imagePath) // return Task<string>
        {
            byte[] imgBytes = File.ReadAllBytes(imagePath);
            string base64string = Convert.ToBase64String(imgBytes);
            return base64string;
        }

        internal static Sprite ButtonIco(string url)
        {
           
            string data = ImageToBase64(url);
            Texture2D texture = CreateTextureFromBase64(data);
            Rect rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Vector4 border = Vector4.zero;
            Sprite sprite = Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, 100.0f, 0, SpriteMeshType.Tight, ref border, false);
            return sprite;
        }

        internal static Sprite CreateSpriteFromBase64(string data)
        {
            Texture2D texture = CreateTextureFromBase64(data);
            Rect rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Vector4 border = Vector4.zero;       
            Sprite sprite = Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, 100.0f, 0, SpriteMeshType.Tight, ref border, false);
            return sprite;
        }
    }

}