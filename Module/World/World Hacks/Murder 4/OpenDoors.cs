using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class OpenDoors : BaseModule
    {
        public OpenDoors() : base("Open Doors", "Open All Doors", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Opened All Doors", false, false);
                Logg.LogDebug("Opened All Doors");
                MurderMisc.MurderMod("SyncOpenL");
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
