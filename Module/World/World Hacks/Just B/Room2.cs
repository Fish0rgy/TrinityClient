using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Just_B
{
    class Room2 : BaseModule
    {
        public Room2() : base("Room 2", "Force Join Room", Main.Instance.Justbbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                JustBMisc.ForceJoin(2);
                LogHandler.Log(LogHandler.Colors.Green, "Force Joined Room!", false, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
