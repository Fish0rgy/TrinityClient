using Area51.SDK;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRC.Networking;
using VRC.SDKBase;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class TagAll : BaseModule
    {
        internal static int Count;
        private readonly Il2CppSystem.Object[] PlayerTagged = new Il2CppSystem.Object[]
        {
            "PlayerTagged"
        };
        public TagAll() : base("Tag Everyone", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                Tag();
                Logg.Log(Logg.Colors.Green, "Tagged Everyone", false, false);
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
        private void Tag()
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
                        Networking.RPC(0, US.gameObject, "UdonSyncRunProgramAsRPC", PlayerTagged);
                    }
                }
            }
        }
    }
}
