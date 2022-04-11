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
using Area51.SDK;

namespace Area51.Module.Exploit.UdonExploits
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
                for (int f = 0; f < WorldWrapper.udonBehaviours.Length; f++)
                {
                    foreach (UdonBehaviour udonobjects in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
                    {
                        Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppSystem.Collections.Generic.List<uint>>.Enumerator fatblackman = udonobjects._eventTable.GetEnumerator();
                        while (fatblackman.MoveNext())
                        {
                            Il2CppSystem.Collections.Generic.KeyValuePair<string, Il2CppSystem.Collections.Generic.List<uint>> name = fatblackman.current;
                            udonobjects.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, name.Key);
                            name = null;
                        }
                        fatblackman = null;
                    }
                    yield return new WaitForSeconds(0.1f);
                    if (!this.toggled)
                        break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield break;
        }
    }
}
