using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.Module.Settings.Theme.Buttons
{
    internal class ButtonColor : BaseModule, OnUpdateEvent
    {
        public ButtonColor() : base("Buttons\nColor", "Enable Any Color Of Buttons Buttons", Main.Instance.SettingsButtonTheme, QMButtonIcons.LoadSpriteFromFile(Serpent.ThemePath), false, false) { } 
        public Color buttoncolor;
        public Color textcolor;


        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {

                switch (str)
                {
                    case "red":
                        {
                            buttoncolor = Color.red;
                            textcolor = Color.white;
                        }
                        break;
                    case "blue":
                        {
                            buttoncolor = Color.blue;
                            textcolor = Color.white;
                        }
                        break;
                    case "cyan":
                        {
                            buttoncolor = Color.cyan;
                            textcolor = Color.white;
                        }
                        break;
                    case "green":
                        {
                            buttoncolor = Color.green;
                            textcolor = Color.white;
                        }
                        break;
                    case "magenta":
                        {
                            buttoncolor = Color.magenta;
                            textcolor = Color.white;
                        }
                        break;
                    case "yellow":
                        {
                            buttoncolor = Color.yellow;
                            textcolor = Color.white;
                        }
                        break;
                    case "black":
                        {
                            buttoncolor = Color.black;
                            textcolor = Color.white;
                        }
                        break;
                    case "white":
                        {
                            buttoncolor = Color.white;
                            textcolor = Color.black;
                        }
                        break;
                    default:
                        {
                            buttoncolor = Color.white;
                            textcolor = Color.black;
                        } 
                        break; 
                } 
                UIU.ChangeTextColor(textcolor);
                UIU.ChangeButtonColor(buttoncolor);
                MenuUI.Log($"THEME: <color=green>Set Color To {str}</color>");
            });

            UIU.OpenKeyboardPopup("Set Button Color", "red,blue,cyan,green,magenta,yellow", keyboardAction);
            //Main.Instance.OnUpdateEvents.Add(this);
        }
        public override void OnDisable()
        {
           // Main.Instance.OnUpdateEvents.Remove(this);
        }
        public async Task setUITheme(int WaitToUpdate)
        {
             

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
