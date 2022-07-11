using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class ClaimClues : BaseModule
    {
        public ClaimClues() : base("Claim CLues", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Claimed All Clues", false, false);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (1)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (2)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (3)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (4)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (5)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (6)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (7)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (8)", 0);
                GhostMisc.ClaimObject("NameClueSystem/NameClues/CluePickup (9)", 0);
            }
            catch (Exception ex)
            {
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
