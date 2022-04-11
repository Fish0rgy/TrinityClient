using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class ForcePickup : BaseModule
    {
        public ForcePickup() : base("Force Pickup", "Allows You To Steal Other's Pickups", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Force Pickup Is Active", false, false);
                LogHandler.LogDebug("Force Pickup Is Active");
                MurderMisc.pickupsteal();
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
