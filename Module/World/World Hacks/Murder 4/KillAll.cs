using Area51.SDK;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRC.Networking;
using VRC.SDKBase;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class KillAll : BaseModule
    {
        internal static int Count;
        private readonly Il2CppSystem.Object[] SyncKill = new Il2CppSystem.Object[]
        {
            "SyncKill"
        };
        public KillAll() : base("Kill All", "Everyone Dies", Main.Instance.Murderbutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Logg.Log(Logg.Colors.Red, "Killed Everyone", false, false);
                Logg.LogDebug("Killed Everyone");
                Kill();
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
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
    }
}
