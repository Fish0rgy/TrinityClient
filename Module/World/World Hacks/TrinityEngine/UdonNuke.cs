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
        IEnumerator UdonKill()
        {
            while (this.toggled)
            {
                UnityEngine.Object.FindObjectsOfType<UdonBehaviour>().ToList().ForEach(UdonObject =>
                { 
                    Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppSystem.Collections.Generic.List<uint>>.Enumerator fatblackman = UdonObject._eventTable.GetEnumerator();
                    while (fatblackman.MoveNext())
                    {
                        Il2CppSystem.Collections.Generic.KeyValuePair<string, Il2CppSystem.Collections.Generic.List<uint>> name = fatblackman.current;
                        UdonObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, name.Key); 
                    }
                });  
                if (!this.toggled)
                    break;
                yield return new WaitForSeconds(0.1f);
            }
            yield break;
        }
    }
}
