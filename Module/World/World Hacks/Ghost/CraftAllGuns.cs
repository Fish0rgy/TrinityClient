using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class CraftAllGuns : BaseModule
    {
        public CraftAllGuns() : base("Give Sniper", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Crafted Guns", false, false);
                GhostMisc.GiveSniper();
            }
            catch (Exception ex)
            {
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
