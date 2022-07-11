using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Module.TargetMenu.MuchenAdmin.MunchenCore;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.Module.TargetMenu.MuchenAdmin
{
    internal class ChangeTag : BaseModule
    {
        public ChangeTag() : base("Change Tag", "This gives you the name, asseturl & imageurl.", Main.Instance.MunchenAdminButton, QMButtonIcons.LoadSpriteFromFile(Serpent.CustomPath), false, false) { }

        public override void OnEnable()
        {
            try
            {
                Bssd1xLohZE2831sTyO player = cRMLef3MSSFd6H8SHap.VKo392tTsZ();
                MunchenMisc.uZpWpgK43o = player.ToString();
                Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text> keyboardAction = new((str, l, txt) =>
                {
                    MunchenMisc.ChangePlayerCustomRankNamega(str, l, txt);

                });
                UIU.OpenKeyboardPopup("Change Munchen Rank", "Enter Rank...", keyboardAction);
            }
            catch(Exception e)
            {
                LogHandler.Error(e);
            }
        }
         
    }
}
