using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Module.World.World_Hacks.STD
{
    internal class STDHealth : BaseModule
    {
        public STDHealth() : base("Give Health", "", Main.Instance.STDButton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                STDMisc.Health();
                LogHandler.Log(LogHandler.Colors.Green, "Gave You Max Health", false, false); 
            }
            catch (Exception ex)
            {
                LogHandler.Error(ex);
            }
        }
    }
}
