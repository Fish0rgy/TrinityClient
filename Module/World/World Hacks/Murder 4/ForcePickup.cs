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
                Logg.Log(Logg.Colors.Green, "Force Pickup Is Active", false, false);
                Logg.LogDebug("Force Pickup Is Active");
                MurderMisc.pickupsteal();
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
