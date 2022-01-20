using Area51.Events;
using Area51.Module.Safety.Photon.NetworkSanity.Core;
using Area51.Module.Safety.Photon.Sanitizers;
using Area51.SDK;
using Area51.SDK.Photon;
using ExitGames.Client.Photon;
using MoPhoGames.USpeak.Core;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnhollowerBaseLib;
using UnityEngine.SceneManagement;

namespace Area51.Module.Safety
{
    internal class RateLimit : BaseModule, OnNetworkSanityEvent
    {
        private static readonly List<ISanitizer> Sanitizers = new List<ISanitizer>();
        public static bool Logs;
        public RateLimit() : base("RateLimit", "Anti Networking Safety", Main.Instance.SafetyButton, null, true, true)
        { 
        }
      

        public override void OnEnable()
        {
            Main.Instance.OnNetworkSanityEvents.Add(this);
         ///try { Area51.Patches.networksanitypatch(); } catch (Exception ex) { Logg.Log(Logg.Colors.Red, $"[Patch] [Error] NetworkSanity {ex.ToString()}", false, false); }
        }

        public override void OnDisable()
        {
            Main.Instance.OnNetworkSanityEvents.Remove(this); 
        } 

        public bool networksantiytoggle()
        {
            IEnumerable<Type> types; 
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null);
            }
            foreach (var t in types)
            {
                if (t.IsAbstract)
                    continue;
                if (!typeof(ISanitizer).IsAssignableFrom(t))
                    continue;

                var sanitizer = Activator.CreateInstance(t) as ISanitizer;
                Sanitizers.Add(sanitizer);
            }
            return true;
        }
    }
}