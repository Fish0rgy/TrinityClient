using Area51.SDK;
using Area51.SDK.ButtonAPI;
using System;
using System.Net;
using VRC.Core;

namespace Area51.Module.TargetMenu
{
    internal class CopyData : BaseModule
    {
        public CopyData() : base("Copy Info", "This gives you the name, asseturl & imageurl.", Main.Instance.AvatarSettings, QMButtonIcons.CreateSpriteFromBase64(Alien.copy), false, false) { }

        public override void OnEnable()
        {
            ApiAvatar avatar = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
            Misc.SetClipboard($"Avatar Name: {avatar.name} | AssetURL Name: {avatar.assetUrl} | ImageURL: {avatar.imageUrl}\n");
        }
    }
}
