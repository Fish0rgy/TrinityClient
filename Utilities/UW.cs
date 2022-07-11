using Trinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;

namespace Trinity.SDK
{
    public static class UW
    {
        internal static void udonsend(string udonEvent, EventTarget targetnetwork)
        {
            switch (targetnetwork)
            {
                case EventTarget.Targeted:
                    {
                        for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
                        {
                            foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
                            {
                                if (name == udonEvent)
                                {
                                    UW.SetEventOwner(WorldWrapper.udonBehaviours[j].gameObject, PU.SelectedVRCPlayer());
                                    WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, name);
                                }
                            }
                        }
                    }
                    break;
                case EventTarget.Everyone:
                    {
                        for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
                        {
                            foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
                            {
                                if (name == udonEvent)
                                {
                                    WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, name);
                                }
                            }
                        }
                    }
                    break;
                case EventTarget.Local:
                    {
                        for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
                        {
                            foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
                            {
                                if (name == udonEvent)
                                {
                                    UW.SetEventOwner(WorldWrapper.udonBehaviours[j].gameObject, PU.GetPlayer());
                                    WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, name);
                                }
                            }
                        }
                    }
                    break;
            }
        }
        public static void trigersend(string objectname)
        {
            VRC_Trigger[] triggers = Resources.FindObjectsOfTypeAll<VRC_Trigger>();
            for (int i = 0; i < triggers.Length; i++)
            {
                if (triggers[i].name.ToLower().Contains(objectname))
                {
                    triggers[i].Interact();
                }
            }
        }
        public static void PathEvent(GameObject objectpath, string udonEvent, EventTarget target) 
        {
            switch (target)
            {
                case EventTarget.Everyone:
                    {
                        GameObject RoomObject = objectpath.gameObject.TryCast<GameObject>();
                        UdonBehaviour PrivateRoom = objectpath.transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                        PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, udonEvent);
                    }
                    break;
                case EventTarget.Targeted:
                    {
                        UW.SetEventOwner(objectpath.gameObject, PU.SelectedVRCPlayer());
                        GameObject RoomObject = objectpath.gameObject.TryCast<GameObject>();
                        UdonBehaviour PrivateRoom = objectpath.transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                        PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                    }
                    break;
                case EventTarget.Local:
                    {
                        UW.SetEventOwner(objectpath.gameObject, PU.GetPlayer());
                        GameObject RoomObject = objectpath.gameObject.TryCast<GameObject>();
                        UdonBehaviour PrivateRoom = objectpath.transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                        PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                    }
                    break;
            }
        }
        internal static void ObjectEvent(string objectName, string udonEvent, EventTarget target)
        {
            switch (target)
            {
                case EventTarget.Everyone:
                    {
                        GameObject[] gameobjects = Resources.FindObjectsOfTypeAll<GameObject>();
                        for (int i = 0; i < gameobjects.Length; i++)
                        {
                            if (gameobjects[i].gameObject.name.Contains(objectName))
                            {
                                GameObject RoomObject = gameobjects[i].gameObject.TryCast<GameObject>();
                                UdonBehaviour PrivateRoom = gameobjects[i].transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                                PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, udonEvent);
                            }
                        }
                    }
                    break;
                case EventTarget.Targeted:
                    {
                        GameObject[] gameobjects = Resources.FindObjectsOfTypeAll<GameObject>();
                        for (int i = 0; i < gameobjects.Length; i++)
                        {
                            if (gameobjects[i].gameObject.name.Contains(objectName))
                            {
                                UW.SetEventOwner(gameobjects[i].gameObject, PU.SelectedVRCPlayer());
                                GameObject RoomObject = gameobjects[i].gameObject.TryCast<GameObject>();
                                UdonBehaviour PrivateRoom = gameobjects[i].transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                                PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                            }
                        }
                    }
                    break;
                case EventTarget.Local:
                    {
                        GameObject[] gameobjects = Resources.FindObjectsOfTypeAll<GameObject>();
                        for (int i = 0; i < gameobjects.Length; i++)
                        {
                            if (gameobjects[i].gameObject.name.Contains(objectName))
                            {
                                UW.SetEventOwner(gameobjects[i].gameObject, PU.GetPlayer());
                                GameObject RoomObject = gameobjects[i].gameObject.TryCast<GameObject>();
                                UdonBehaviour PrivateRoom = gameobjects[i].transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                                PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                            }
                        }
                    }
                    break;
            }
        }
        public static void TargetedEvent(string udonevent, VRCPlayer player)
        {
            for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
            {
                foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
                {
                    if (name == udonevent)
                    {
                        UW.SetEventOwner(WorldWrapper.udonBehaviours[j].gameObject, player.prop_Player_0);
                        WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, name);
                    }
                }
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
        public static void SetEventOwner(this GameObject gameObject, VRC.Player player)
        {
            if (GrabOwner(gameObject) != player)
            {
                Networking.SetOwner(player.field_Private_VRCPlayerApi_0, gameObject);
            }
        }
    }
    public enum EventTarget
    {
        Everyone,
        Targeted,
        Local
    }
}