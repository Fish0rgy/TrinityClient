using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.Player.Bones
{
    internal class Breload : BaseModule
    {
        public Breload() : base("Reload Avis", "Bones For hips", Main.Instance.DynamicBonesButton, null, false, false) { }

        public override void OnEnable()
        { 
            Utilities.PU.ReloadAllAvatars(); 
        } 
    }
}
