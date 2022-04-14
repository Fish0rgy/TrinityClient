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
using VRC.Networking;
using VRC.SDKBase;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    class KillLoop: BaseModule
    {
        internal static int Count;
        private readonly Il2CppSystem.Object[] SyncKill = new Il2CppSystem.Object[]
        {
            "SyncKill"
        };
        public KillLoop() : base("KillLoop", "Everyone Dies", Main.Instance.Murderbutton, null, true)
        {
        }
        public override void OnEnable()
        {
            try
            {
                LogHandler.Log(LogHandler.Colors.Red, "Killed Everyone", false, false);
                LogHandler.LogDebug("Killed Everyone");
                MelonCoroutines.Start(KillingLoop()); 
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Red, ex.ToString(), false, false);
            }
        }
        private void Kill()
        {
            using (IEnumerator<UdonSync> EKill = Resources.FindObjectsOfTypeAll<UdonSync>().GetEnumerator())
            {
                Count = 0;
                while (EKill.MoveNext())
                {
                    UdonSync US;
                    if ((US = EKill.Current).gameObject.name.Contains("Player Node"))
                    {
                        Count++;
                        Networking.RPC(0, US.gameObject, "UdonSyncRunProgramAsRPC", SyncKill);
                    }
                }
            }
        }
        public IEnumerator KillingLoop()
        {
             
            while (toggled)
            { 
                Kill(); 
                yield return new WaitForSeconds(0.1f);
            }
            yield break;
        }
    }
}
