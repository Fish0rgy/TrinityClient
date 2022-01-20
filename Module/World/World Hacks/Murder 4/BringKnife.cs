using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class BringKnife : BaseModule
    {
        public BringKnife() : base("Give Knife", "Gives You Every Item", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Teleported Knife To Your Position", false, false);
                Logg.LogDebug("Teleported Knife To Your Position");
                MurderMisc.MurderGive("Knife");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
