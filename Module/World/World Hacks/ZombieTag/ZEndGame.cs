using Area51.SDK;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRC.Networking;
using VRC.SDKBase;

namespace Area51.Module.World.World_Hacks.ZombieTag
{
    class ZEndGame : BaseModule
    {
        internal static int Count;
        private readonly Il2CppSystem.Object[] QuitGame = new Il2CppSystem.Object[]
        {
            "QuitGame"
        };
        public ZEndGame() : base("End Game", "", Main.Instance.Zombiebutton, null, false)
        {
        }
        public override void OnEnable()
        {
            try
            {
                End();
                Logg.Log(Logg.Colors.Green, "Game Ended", false, false);
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
        private void End()
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
                        Networking.RPC(0, US.gameObject, "UdonSyncRunProgramAsRPC", QuitGame);
                    }
                }
            }
        }
    }
}
