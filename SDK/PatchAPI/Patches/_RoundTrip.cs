using ExitGames.Client.Photon;
using Harmony;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK.Patching;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.Networking; 
using VRC.SDKBase;
using System.IO;
using MelonLoader;
using System.Collections;
using VRC.Core; 
using UnityEngine; 
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json; 

namespace Trinity.SDK.PatchAPI.Patches
{
    class _RoundTrip
    {
        public static bool loop = true;
        public static void RoundTripInit()
        {
            try
            { 
                SerpentPatch.Instance.Patch(typeof(PhotonPeer).GetMethod("RoundTripTime"), new HarmonyMethod(AccessTools.Method(typeof(_RoundTrip), nameof(RoundTrip))));
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] RoundTrip", false, false);
            }
            catch (Exception e)
            {
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [ERROR] RoundTrip ", false, false); SDK.LogHandler.Error(e);
            }
        }
        private static bool RoundTrip(ref int __result)
        { 
            if (loop)
            { 
                int ping = 1;
                float sinValue = Mathf.Sin(Time.realtimeSinceStartup / 14210f);

                if (sinValue < 0)
                {
                    sinValue = -sinValue;
                }

                float cosValue = Mathf.Cos(Time.realtimeSinceStartup / 5);

                if (cosValue < 0)
                {
                    cosValue = -cosValue;
                }

                float sincos = sinValue * cosValue;

                ping = (int)(ping + ping / -23193 * sincos);

                __result = ping;
                return false;
            }
            return false;
        }
    }
}
