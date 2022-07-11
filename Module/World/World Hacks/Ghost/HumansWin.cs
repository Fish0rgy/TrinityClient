using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class HumansWin : BaseModule
    {
        public HumansWin() : base("Humans Win", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Humans Won", false, false);
                Trinity.SDK.UW.ObjectEvent("LobbyManager", "Local_HumanWin", 0);
            }
            catch (Exception ex)
            {
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
