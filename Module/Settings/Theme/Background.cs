using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.Module.Settings.Theme
{
    class CustomBGImage : BaseModule
    {
        public CustomBGImage() : base("Enable\nBackground", "Set's our custom background image", Main.Instance.SettingsButtonTheme, null, true, false) { }

        public Sprite Background = null;
        public override void OnEnable()
        {
            try
            {
                if (Background == null) Background = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().activeSprite;
                string bgImage = SDK.Misc.GetBase64StringForImage(AppDomain.CurrentDomain.BaseDirectory + "//Trinity//Background//custom.png");
                if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Trinity//Background//custom.png"))
                {
                    LogHandler.Log(LogHandler.Colors.Red, bgImage + "\n[Trinity] Failed to locate custom.png in VRChat/Background/custom.png!", false, false);
                }
                else
                {
                    GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().sprite = QMButtonIcons.CreateSpriteFromBase64(bgImage);
                }
               
            }
            catch (NullReferenceException Error)
            {
                if (Error.Message.Contains("not set to an instance"))
                {
                   
                }

            }
        }

        public override void OnDisable()
        {
            try
            {
                GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().sprite = Background;
            }
            catch (NullReferenceException Error)
            {
                if (Error.Message.Contains("not set to an instance"))
                {
                  
                }

            }
        }
    }
}