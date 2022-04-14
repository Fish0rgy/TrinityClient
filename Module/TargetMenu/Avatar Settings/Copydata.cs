using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Net;
using VRC.Core;

namespace Trinity.Module.TargetMenu
{
    internal class CopyData : BaseModule
    {
        public CopyData() : base("Copy Info", "This gives you the name, asseturl & imageurl.", Main.Instance.AvatarSettings, QMButtonIcons.LoadSpriteFromFile(Serpent.copyPath), false, false) { }

        public override void OnEnable()
        {
            ApiAvatar avatar = PU.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_ApiAvatar_0;
            Misc.SetClipboard($"Avatar Name: {avatar.name} | AssetURL Name: {avatar.assetUrl} | ImageURL: {avatar.imageUrl}\n");
        }
    }
}
