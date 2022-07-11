using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    class GiveCurrency : BaseModule
    {
        public GiveCurrency() : base("Give Money", "", Main.Instance.GhostButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Gave You Max Currency", false, false);
                GhostMisc.GiveSelfMaxCurrency();
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    } 
}
