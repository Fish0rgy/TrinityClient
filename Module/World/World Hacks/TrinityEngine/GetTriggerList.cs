using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase; 

namespace Trinity.Module.World.World_Hacks.TrinityEngine
{
    class GetTriggerList : BaseModule
    {
        public GetTriggerList() : base("Trigger Table", "Gets List Of Sendable Trigger Events", Main.Instance.udonexploitbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.EventTablePath), false, false) { }

        public override void OnEnable()
        {
            VRC_Trigger[] triggers = Resources.FindObjectsOfTypeAll<VRC_Trigger>();
            for (int i = 0; i < triggers.Length; i++)
            {
                string sLine = $"-------------- {WorldWrapper.CurrentWorld().name} Trigger Table --------------";
                LogHandler.Log(LogHandler.Colors.Green, $"{sLine}\nObject Name: {triggers[i].name.ToLower()}\n", false, false);
            }
        }
    }
}
