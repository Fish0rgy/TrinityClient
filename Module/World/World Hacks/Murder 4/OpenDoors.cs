using Trinity.Utilities;
using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Murder_4
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
                LogHandler.Log(LogHandler.Colors.Green, "Opened All Doors", false, false);
                LogHandler.LogDebug("Opened All Doors");
                MurderMisc.ObjectInteract("Interact open");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
