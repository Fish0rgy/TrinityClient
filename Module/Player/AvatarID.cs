using Trinity.Utilities;
using Trinity.SDK;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.Module.Player
{
    class AvatarID : BaseModule
    {
        public AvatarID() : base("Change Avatar By ID", "copy an avatarid into your clipboard then click change. ", Main.Instance.PlayerButton, SDK.ButtonAPI.QMButtonIcons.LoadSpriteFromFile(Serpent.clientLogoPath), false, false) { }
        public override void OnEnable()
        {
            Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
            {
                MenuUI.Log("EXPLOITS: <color=yellow>Starting Avatar Crash...</color>");
                if (!str.StartsWith("avtr")) return;
                PU.ChangeAvatar(str);
            });
            UIU.OpenKeyboardPopup("Change Avi", "Enter Avatar ID...", keyboardAction);
            MenuUI.Log("AVATAR: <color=green>Copied ID To Cilp Board</color>");
        }
    }
}
