using Area51.SDK;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Udon;

namespace Area51.Module.World.World_Hacks.Just_B
{
    class BlindPeople : BaseModule
    {
        public BlindPeople() : base("Lock Everyone", "Blinds People", Main.Instance.Justbbutton, null, true)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Blinded Everyones Vision", false, false);
                UdonExploitManager.udonsend("OnDesktopTopDownViewStart", "everyone");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public override void OnDisable()
        {
            try
            {
                UdonExploitManager.udonsend("OnPutDownCueLocally", "everyone");
                LogHandler.Log(LogHandler.Colors.Green, "UnBlinded Everyones Vision", false, false);
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
//OnDesktopTopDownViewStart start
//OnPutDownCueLocally stop
