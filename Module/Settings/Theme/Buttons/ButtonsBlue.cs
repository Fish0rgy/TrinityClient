using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Trinity.Module.Settings.Theme
{
    class ButtonsBlue : BaseModule, OnUpdateEvent
    {
        public ButtonsBlue() : base("Buttons\nBlue", "Enable Blue Buttons", Main.Instance.SettingsButtonTheme, QMButtonIcons.LoadSpriteFromFile(Serpent.SpaceShipPath), true, false) { }
        private bool SetTheme, QMTheme = false;
        public Sprite Background = null;
        public bool BGSet = false;


        public override void OnEnable()
        {
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnUpdateEvents.Remove(this);
        }
        public async Task setUITheme(int WaitToUpdate)
        {

            for (int i = 0; i < WaitToUpdate; i++)
            {
                Trinity.Utilities.UIU.SetColor(Color.black, Color.blue);
            }

        }

        public void OnUpdate()
        {
            try
            {
                setUITheme(1).Start();
            }
            catch (Exception) { }
        }
    }
}
