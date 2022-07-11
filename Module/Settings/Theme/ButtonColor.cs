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
        public Color secondary; 


        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {

                switch (str)
                {
                    case "red":
                        {
                            secondary = Color.red;
                        }
                        break;
                    case "blue":
                        {
                            secondary = Color.blue;
                        }
                        break;
                    case "cyan":
                        {
                            secondary = Color.cyan;
                        }
                        break;
                    case "green":
                        {
                            secondary = Color.green;
                        }
                        break;
                    case "magenta":
                        {
                            secondary = Color.magenta;
                        }
                        break;
                    case "yellow":
                        {
                            secondary = Color.yellow;
                        }
                        break;
                    default:
                        secondary = Color.white;
                        break;
                        UIU.ChangeTextColor(secondary);
                        UIU.ChangeButtonColor(secondary);
                        MenuUI.Log($"THEME: <color=green>Set Color To {str}</color>");
                }
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
