using Trinity.Utilities;
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
                GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().sprite = QMButtonIcons.LoadSpriteFromFile(Serpent.RetroPath);
                 
            }
            catch (Exception Error)
            {
                if (Error.Message.Contains("Object reference not set to an instance of an object")) { }
            }
        }
        public override void OnDisable()
        {
            try
            {
                GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>().sprite = Background;
            }
            catch (Exception Error)
            {
                if (Error.Message.Contains("Object reference not set to an instance of an object")) { }
            }
        }
    }
}