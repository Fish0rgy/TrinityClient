using Trinity.Utilities;
using System;
using VRC.SDKBase;
using UnityEngine;
using UnityEngine.UI;

using Trinity.SDK;
using Trinity.Utilities;


namespace Trinity.Module.World
{
    class JoinByID : BaseModule
    {
        public JoinByID() : base("Force Join", "Make Sure To Copy A World ID To Your Clipboard Before Clicking", Main.Instance.WorldButton, SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.rocketPath), false, false) { }

        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {
                if (string.IsNullOrWhiteSpace(str)) return; 

                MenuUI.Log("WORLD: <color=green>Force Joined World</color>");
                Networking.GoToRoom(str);
            });

            UIU.OpenKeyboardPopup("Force Join", "Enter Instance ID...", keyboardAction);
        }
    }
}
