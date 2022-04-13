using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace Trinity.Module.World.World_Hacks.TrinityEngine
{
    class TriggerNuke : BaseModule
    {
        public TriggerNuke() : base("Trigger Nuker", "Spamms Trigger Objects FAST AS FUCK",Main.Instance.udonexploitbutton, null, true) { }

        public override void OnEnable()
        {
            VRC_Trigger[] triggers = Resources.FindObjectsOfTypeAll<VRC_Trigger>();
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].Interact();
            }
        }

    }
}
