using Trinity.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using Trinity.SDK;

namespace Trinity.Module.Exploit.UdonExploits
{
    class UdonNuke : BaseModule
    {
        public UdonNuke() : base("Udon Nuker", "Spams Udon A Lot Of UdonShit", Main.Instance.udonexploitbutton, null, true) { } 

        public override void OnEnable()
        {
            MelonCoroutines.Start(UdonKill());
        }
        public IEnumerator UdonKill()
        {
            while (this.toggled)
            {
                WorldWrapper.udonBehaviours.ToList().ForEach(UdonObject =>
                {
                    UdonObject._eventTable.keys.ToString().ToList().ForEach(UdonKey =>
                    {
                        string key = UdonKey.ToString();
                        UdonObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, key);

                    });

                });
                if (!this.toggled)
                    break;
                yield return new WaitForSeconds(0.1f);
                 
            }
            yield break;
        }
    }
}
