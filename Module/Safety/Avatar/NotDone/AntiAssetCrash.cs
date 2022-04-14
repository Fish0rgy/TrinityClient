using Trinity.Utilities;
using Trinity.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Trinity.Module.Safety.Avatar
{
    class AntiAssetCrash : BaseModule 
    {
        public AntiAssetCrash() : base("Anti Currupted", "Destroys Currupted Asset Bundles", Main.Instance.Avatarbutton, null, true, false)
        {
        }
        public override void OnEnable()
        {
            //Main.Instance.OnAssetBundleLoadEvents.Add(this);
        }

        public override void OnDisable()
        {
            //Main.Instance.OnAssetBundleLoadEvents.Remove(this);
        }  
    }
}
