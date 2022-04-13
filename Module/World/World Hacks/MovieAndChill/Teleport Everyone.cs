using Trinity.SDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.World.World_Hacks.MovieAndChill
{
    class Teleport_Everyone : BaseModule
    {
        public Teleport_Everyone() : base("Respawn Everyone", "Fuck Everyone", Main.Instance.MovieAndChillButton, null, true)
        {
        }
        public override void OnEnable()
        {
            try
            {
                MelonLoader.MelonCoroutines.Start(gay());
                LogHandler.Log(LogHandler.Colors.Green, "Force Respawned Everyone Because Fuck Everyone", false, false);
                LogHandler.LogDebug("Rorce Repsawned Everyone");
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public override void OnDisable()
        {
            MelonLoader.MelonCoroutines.Stop(gay());
        }
        IEnumerator gay()
        {
            while (toggled)
            {
                UdonExploitManager.ObjectEvent("Door Room 1 OPEN", "Teleport", 2);
                yield return new WaitForSecondsRealtime(0.2f);
            }
            yield break;
        }
    }
}
