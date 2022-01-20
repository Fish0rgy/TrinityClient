using Area51.SDK;
using System;

namespace Area51.Module.World.World_Hacks.Just_B
{
    class Room3 : BaseModule
    {
        public Room3() : base("Room 3", "Force Join Room", Main.Instance.Justbbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Green, "Force Joined Room!", false, false);
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
