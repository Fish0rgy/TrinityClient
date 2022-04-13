using Trinity.SDK;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class FlashLoop : BaseModule
    {
        public FlashLoop() : base("Flash Loop", "Black Screen Until You Stop Seconds", Main.Instance.Murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Blinded Everyone In The Lobby", false, false);
                LogHandler.LogDebug("Blinded Everyone In The Lobby");
                MelonCoroutines.Start(FlashLooping());
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public IEnumerator FlashLooping()
        { 
            while (toggled)
            {
                GameObject Lights = GameObject.Find("Switchbox (0)");
                yield return new WaitForSeconds(0.9f);
                MurderMisc.udonsend(Lights, "SwitchUp", null, false);
                yield return new WaitForSeconds(0.9f);
                MurderMisc.udonsend(Lights, "SwitchDown", null, false); 
            }
            yield break;
        }
    }
}
