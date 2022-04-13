using Trinity.SDK;
using System;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Just_B
{
    class Room1 : BaseModule
    {
        public Room1() : base("Room 1", "Force Join Room", Main.Instance.Justbbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                JustBMisc.ForceJoin(1);
                LogHandler.Log(LogHandler.Colors.Green, "Force Joined Room!", false, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }


      
    }
}



  