using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Just_B
{
    class Room7 : BaseModule
    {
        public Room7() : base("VIP Room", "Force Join Room", Main.Instance.Justbbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                JustBMisc.ForceJoin(7);
                LogHandler.Log(LogHandler.Colors.Green, "Force Joined Room!", false, false);

            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
