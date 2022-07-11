﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;

namespace Trinity.Module.TargetMenu.MuchenAdmin
{
    internal class RemoveColor : BaseModule
    {
        public RemoveColor() : base("Remove Color", "This gives you the name, asseturl & imageurl.", Main.Instance.MunchenAdminButton, QMButtonIcons.LoadSpriteFromFile(Serpent.CustomPath), false, false) { }

        public override void OnEnable()
        {

        }
    }
}