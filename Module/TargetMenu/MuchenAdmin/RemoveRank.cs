using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;

namespace Trinity.Module.TargetMenu.MuchenAdmin
{
    internal class RemoveRank : BaseModule
    {
        public RemoveRank() : base("Remove Tag", "This gives you the name, asseturl & imageurl.", Main.Instance.MunchenAdminButton, QMButtonIcons.LoadSpriteFromFile(Serpent.CustomPath), false, false) { } 
        public override void OnEnable()
        {
            try
            {
                MunchenMisc.RemoveRank();
            }
            catch (Exception e)
            {
                LogHandler.Error(e);
            }
        }
    }
}
