using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.World.World_Hacks.STD
{
    internal class STDMoney : BaseModule
    { 
        public STDMoney() : base("Give Money", "", Main.Instance.STDButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                STDMisc.Money();
                LogHandler.Log(LogHandler.Colors.Green, "Gave You Max Currency", false, false); 
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
        }
    }
}
