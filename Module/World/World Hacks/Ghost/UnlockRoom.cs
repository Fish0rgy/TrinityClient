using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class UnlockRoom : BaseModule
    {
        public UnlockRoom() : base("Claim Keys", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Keys Claimed!", false, false);
                GhostMisc.ClaimObject("PoliceStation_A/Functions/KeySpawn/Keys/Key", 1);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
