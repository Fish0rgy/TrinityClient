using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Just_B
{
    class Room6 : BaseModule
    {
        public Room6() : base("Room 6", "Force Join Room", Main.Instance.Justbbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                JustBMisc.ForceJoin(6);
                LogHandler.Log(LogHandler.Colors.Green, "Force Joined Room!", false, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
