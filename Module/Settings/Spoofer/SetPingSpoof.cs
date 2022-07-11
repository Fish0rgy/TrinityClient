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
    internal class SetPingSpoof : BaseModule
    {
        public SetPingSpoof() : base("Set Ping", "", Main.Instance.SettingsButtonspoofer, null, false)
        {

        }
        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {
                Config.PingSpoof = Int32.Parse(str);
                MenuUI.Log($"SPOOFER: <color=green>Set Ping To {str}</color>");
                LogHandler.Log(LogHandler.Colors.Green, $"[Spoofer] Set Custom Ping To {str}", false, false);
            });

            UIU.OpenKeyboardPopup("Trinity Ping Spoof", "Enter Ping Value...", keyboardAction);
        }
    }
}
