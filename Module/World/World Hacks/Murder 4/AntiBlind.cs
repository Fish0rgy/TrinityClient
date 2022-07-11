using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class AntiBlind : BaseModule
    {
        public AntiBlind() : base("Anti Blind", "no blinding", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Blinding Objects Disabled", false, false);
                MenuUI.Log($"MURDER4: <color=green>Blinding Objects Disabled</color>");
                MurderMisc.antiblind();
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
