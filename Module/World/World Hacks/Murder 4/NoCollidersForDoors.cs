using Trinity.Utilities;
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
    internal class NoCollidersForDoors : BaseModule
    {
        public NoCollidersForDoors() : base("Disable Doors", "Disables Door Colliders", Main.Instance.Murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Green, "Door Colliders Disabled", false, false);
                check = true;
                MelonCoroutines.Start(toggledoorcolideroff());
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
                LogHandler.Log(LogHandler.Colors.Green, "Door Colliders Enabled", false, false);
                check = false;
                MelonCoroutines.Stop(toggledoorcolideroff());
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        public static bool check = false;
        public static IEnumerator toggledoorcolideroff()
        {
            foreach (var doorss in Resources.FindObjectsOfTypeAll<BoxCollider>())
            {
                if (doorss.gameObject.name.Contains("Closed collision geo"))
                {
                    doorss.GetComponent<BoxCollider>().enabled = !check;
                }
                yield return null;
            }
            yield return null;

        }
    }
}
