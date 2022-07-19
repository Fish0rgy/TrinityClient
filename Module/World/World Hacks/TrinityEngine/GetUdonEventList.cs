using Trinity.Utilities;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using VRC.SDKBase;

namespace Trinity.Module.Player
{
    class GetUdonEventList : BaseModule
    {
        private static List<string> UdonList = new List<string>();
        public GetUdonEventList() : base("Udon\n Event Table", "Gets List Of Sendable Udon Events", Main.Instance.udonexploitbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.EventTablePath), false, false) { }

        public override void OnEnable()
        {
            for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
            {
                foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
                {
                    if (name.StartsWith("_"))
                    {
                      //  LogHandler.Log(LogHandler.Colors.Blue, $"[{WorldWrapper.CurrentWorld().name} Udon Event Table] {Environment.NewLine} Name: {name} | Can Send:No | ObjectName:{WorldWrapper.udonBehaviours[j].gameObject.name} {Environment.NewLine}", false, false);
                    }
                    else
                    {
                        string sLine = $"-------------- {WorldWrapper.CurrentWorld().name} Event Table --------------";
                        LogHandler.Log(LogHandler.Colors.Green, $"{sLine}\n Name: {name}\n Object:{WorldWrapper.udonBehaviours[j].gameObject.name}\n", false, false);
                    }
                     
                }
            }
        }
    }
}
