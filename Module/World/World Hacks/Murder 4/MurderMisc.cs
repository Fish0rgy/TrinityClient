using Trinity.Utilities;
using Trinity.SDK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    public static class MurderMisc
    {
        public static bool toggled;
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
        public static void TargetedNadeNLag()
        {
            Resources.FindObjectsOfTypeAll<GameObject>().ToList().ForEach(Obj =>
            {
                if (Obj.gameObject.name != "Frag (0)") return;
                UW.SetEventOwner(Obj, PU.SelectedVRCPlayer());
                Obj.gameObject.transform.position = new Vector3(PU.SelectedVRCPlayer().gameObject.transform.position.x, PU.SelectedVRCPlayer().gameObject.transform.position.y + 1f, PU.SelectedVRCPlayer().gameObject.transform.position.z);
                UW.PathEvent(Obj, "Explode", EventTarget.Targeted);
            });
        }
        public static void ObjectInteract(string ObjectName)
        {
            foreach(GameObject Doors in DoorList()) 
            {
                Doors.transform.Find(ObjectName).gameObject.SetObjecOwner();
                Doors.transform.Find(ObjectName).GetComponent<UdonBehaviour>().Interact();
            }
        }
        public static void TargetedEvent(string udonevent)
        {
            GameObject playernode = GameObject.Find("Player Nodes");
            foreach (Transform player in playernode.GetComponentsInChildren<Transform>())
            {
                if (player.name != playernode.name)
                {
                    player.gameObject.udonsend(udonevent, PU.SelectedVRCPlayer(), false);
                }
            }
        }
        public static void antiblind()
        {
            try { GameObject.Find("Flashbang HUD Anim").SetActive(false); GameObject.Find("Blind HUD Anim").SetActive(false); } catch { } 
        }
        public static void TargetedEvent2(this GameObject gameObject, string udonEvent, VRC.Player player = null, bool componetcheck = false)
        {
            UdonBehaviour component = gameObject.GetComponent<UdonBehaviour>();
            if (!(player != null))
            {
                if (!componetcheck)
                {
                    if (player == PU.SelectedVRCPlayer())
                    {
                        component.SendCustomEvent(udonEvent);
                        return;
                    }
                    component.SendCustomNetworkEvent(0, udonEvent);
                }
                return;
            }
            SetEventOwner(gameObject, player);
            component.SendCustomNetworkEvent((VRC.Udon.Common.Interfaces.NetworkEventTarget)1, udonEvent);
        }
        public static void RoleAssign(string udonevent)
        {
            GameObject gameObject = GameObject.Find("Player Nodes");
            foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>())
            {
                if (transform.name != gameObject.name)
                {
                    UW.PathEvent(transform.gameObject, udonevent, EventTarget.Local);
                }
            }
        }
        public static void RoleAssignEveryone(string udonevent)
        {
            GameObject gameObject = GameObject.Find("Player Nodes");
            foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>())
            {
                if (transform.name != gameObject.name)
                {
                    udonsend(transform.gameObject, udonevent, null, false);
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
        public static void TargetBoomBoom()
        {
            GameObject frag = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
            MoveObjLocation(frag, PU.SelectedVRCPlayer());
            UW.PathEvent(frag, "Explode", EventTarget.Targeted);
        }
        public static void MoveObjLocation(GameObject obj, VRC.Player player)
        {
            foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
            {
                Transform FragObj = obj.transform;
                Networking.LocalPlayer.TakeOwnership(FragObj.gameObject);
                Transform FragTransform = FragObj.transform;
                FragTransform.transform.position = player.transform.position;
            }
        }
        public static void MurderTargetGive(string ObjectName)
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                bool GameLogic = gameObject.name.Contains(ObjectName);
                if (GameLogic)
                {
                    Networking.SetOwner(PU.SelectedVRCPlayer().field_Private_VRCPlayerApi_0, gameObject);
                    gameObject.transform.position = PU.SelectedVRCPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
                }
            }
        }
        public static void MurderFlash()
        {

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
        public static void udonsend(this GameObject gameObject, string udonEvent, VRC.Player player = null, bool componetcheck = false)
        {
            UdonBehaviour component = gameObject.GetComponent<UdonBehaviour>();
            if (!(player != null))
            {
                if (!componetcheck)
                {
                    if (player == VRCPlayer.field_Internal_Static_VRCPlayer_0._player)
                    {
                        component.SendCustomEvent(udonEvent);
                        return;
                    }
                    component.SendCustomNetworkEvent(0, udonEvent);
                }
                return;
            }
            SetEventOwner(gameObject,player);
            component.SendCustomNetworkEvent((VRC.Udon.Common.Interfaces.NetworkEventTarget)1, udonEvent);
        }
        public static void SetEventOwner(this GameObject gameObject, VRC.Player player)
        {
            if (GrabOwner(gameObject) != player)
            {
                Networking.SetOwner(player.field_Private_VRCPlayerApi_0, gameObject);
            }
        }
        public static VRC.Player GrabOwner(this GameObject gameObject)
        {
            foreach (VRC.Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player.field_Private_VRCPlayerApi_0.IsOwner(gameObject))
                {
                    return player;
                }
            }
            return null;
        }
         
        public static void SetObjecOwner(this GameObject gameObject)
        {
            if (GrabOwner(gameObject) != VRCPlayer.field_Internal_Static_VRCPlayer_0._player)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0, gameObject);
            }
        }
        public static List<GameObject> DoorList()
        {
            List<GameObject> doors = new List<GameObject>();
            try
            {
                doors.Add(GameObject.Find("Door").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge").gameObject);
                doors.Add(GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge").gameObject);
            }
            catch
            {
            }
            return doors;
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
