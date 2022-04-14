using Trinity.Utilities;
using Trinity.SDK.Photon;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;

namespace Trinity.SDK
{
    class Keybinds
    {
        public static IEnumerator udonNukeKeyBind()
        {
            while (Input.GetKeyDown(KeyCode.K))
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
                    if (!Input.GetKeyDown(KeyCode.K))
                        break;

                    yield return new WaitForSeconds(0.1f);
                }
                yield break;
            }
        }
    }
}
