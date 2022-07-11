using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.Module.Settings.Spoofer
{
    internal class SetFPSpoof : BaseModule
    {
        public SetFPSpoof() : base("Set FPS", "", Main.Instance.SettingsButtonspoofer, null, false)
        {

        }
        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {
                Config.FPSSpoof = Int32.Parse(str);
                MenuUI.Log($"UDON: <color=green>SPOOFER: Set FPS To {str}</color>");
                LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] Set Custom FPS To {str}", false, false);
            });

            UIU.OpenKeyboardPopup("Trinity FPS Spoof", "Enter FPS Value...", keyboardAction);
        }
    }
}
