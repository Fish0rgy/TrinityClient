using Trinity.SDK;
using System;

namespace Trinity.Module.World.World_Hacks.Just_B
{
    class Room4 : BaseModule
    {
        public Room4() : base("Room 4", "Force Join Room", Main.Instance.Justbbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                JustBMisc.ForceJoin(4);
                LogHandler.Log(LogHandler.Colors.Green, "Force Joined Room!", false, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
