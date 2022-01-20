using System.Linq;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace Area51.Module.World.World_Hacks.Murder_4
{
    class MurderMisc
    {
        public static void MurderMod(string udonevent)
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                bool GameLogic = gameObject.name.Contains("Game Logic");
                if (GameLogic)
                {
                    gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, udonevent);
                }
            }
        }
        public static void MurderGive(string ObjectName)
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                bool GameLogic = gameObject.name.Contains(ObjectName);
                if (GameLogic)
                {
                    Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                    gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                }
            }
        }
        public static void pickupsteal()
        {
            VRC_Pickup[] gameobj = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
            for (int i = 0; i < gameobj.Length; i++)
            {
                bool check = gameobj[i].gameObject;
                if (check)
                {
                    gameobj[i].DisallowTheft = false;
                }
            }
            VRC_Pickup[] gameobj2 = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
            for (int ii = 0; ii < gameobj2.Length; ii++)
            {
                bool check2 = gameobj2[ii].gameObject;
                if (check2)
                {
                    gameobj2[ii].DisallowTheft = false;
                }
            }
            VRCPickup[] gameobj3 = Resources.FindObjectsOfTypeAll<VRCPickup>().ToArray<VRCPickup>();
            for (int iii = 0; iii < gameobj3.Length; iii++)
            {
                bool check3 = gameobj3[iii].gameObject;
                if (check3)
                {
                    gameobj3[iii].DisallowTheft = false;
                }
            }
        }
    }
}
/* list of udon events for murder 4 so you can add more features 
         * OnLocalPlayerBlinded
         * SyncAssignB
         * SyncAssignD
         * SyncAssignM (still have to figure out how to assign roles, concept is to make host send a udon event where your the assigned role but i havent figured out how to code that so fuck you)
         * SyncVictoryM
         * SyncAbort
         * SyncVictoryB
         * Btn_Start
         * SyncClose
         * SyncOpen
         * KillLocalPlayer
         * SyncLock - lock doors (udon events for doors use a player node so you have to remake one for lock all doors to work :( )
         * SyncBreakL - for doors
         * SyncUnlockL - for doors
         * SyncOpenR - for doors
         * ResetDoor - for doors
         * OBJECT NAMES
         * Bear Trap
         * Knife
         * Frag
         * Luger
         * Revolver
		 * Camera
         * Shotgun
         * Smoke
         */
