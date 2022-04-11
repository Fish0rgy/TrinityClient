using Area51.SDK;
using UnityEngine;

namespace Area51.Module.World.World_Hacks.MovieAndChill
{
    class MCMisc
    {
        public static void Teleport()
        {
            GameObject[] gameobjects = Resources.FindObjectsOfTypeAll<GameObject>();
            for (int i = 0; i < gameobjects.Length; i++)
            {
                if (gameobjects[i].gameObject.name.Contains(""))
                {
                    UdonExploitManager.SetEventOwner(gameobjects[i].gameObject, PlayerWrapper.SelectedVRCPlayer());
                    gameobjects[i].gameObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "Teleport");
                }
            }
        }
    }
}
