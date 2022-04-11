using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class BringItems : BaseModule
    {
        public BringItems() : base("Give Items", "Gives You Every Item", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Teleported Every Object To Your Position", false, false);
                LogHandler.LogDebug("Teleported Every Object To Your Position");
                MurderMisc.MurderGive("Revolver");
                MurderMisc.MurderGive("Knife");
                MurderMisc.MurderGive("Shotgun");
                MurderMisc.MurderGive("Frag");
                MurderMisc.MurderGive("Luger");
                MurderMisc.MurderGive("Bear Trap");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
