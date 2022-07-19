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
            //hacker = automating god
            switch (targetnetwork)
            {
                case EventTarget.Targeted:
                    { 
                        
                        WorldWrapper.udonBehaviours.ToList().ForEach(UdonObject =>
                        {
                            if (UdonObject._eventTable.ContainsKey(udonEvent))
                            {
                                UW.SetEventOwner(UdonObject.gameObject, PU.SelectedVRCPlayer());
                                UdonObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                            }
                        });
                    }
                    break;
                case EventTarget.Everyone:
                    {
                        WorldWrapper.udonBehaviours.ToList().ForEach(UdonObject =>
                        {
                            if (UdonObject._eventTable.ContainsKey(udonEvent))
                            {
                                UdonObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, udonEvent);
                            }
                        });
                             
                    }
                    break;
                case EventTarget.Local:
                    {
                        WorldWrapper.udonBehaviours.ToList().ForEach(UdonObject =>
                        {
                            if (UdonObject._eventTable.ContainsKey(udonEvent))
                            {
                                UW.SetEventOwner(UdonObject.gameObject, PU.GetPlayer());
                                UdonObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                            }
                        });
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
                         
                        Resources.FindObjectsOfTypeAll<GameObject>().ToList().ForEach(obj =>
                        {
                            if (obj.name.Contains(objectName))
                            {
                                GameObject RoomObject = obj.gameObject.TryCast<GameObject>();
                                UdonBehaviour PrivateRoom = obj.transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                                PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, udonEvent);
                            }
                        });  
                    }
                    break;
                case EventTarget.Targeted:
                    {
                        Resources.FindObjectsOfTypeAll<GameObject>().ToList().ForEach(obj =>
                        {
                            if (obj.name.Contains(objectName))
                            {
                                UW.SetEventOwner(obj.gameObject, PU.SelectedVRCPlayer());
                                GameObject RoomObject = obj.gameObject.TryCast<GameObject>();
                                UdonBehaviour PrivateRoom = obj.transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                                PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                            }
                        }); 
                    }
                    break;
                case EventTarget.Local:
                    {
                        Resources.FindObjectsOfTypeAll<GameObject>().ToList().ForEach(obj =>
                        {
                            if (obj.name.Contains(objectName))
                            {
                                UW.SetEventOwner(obj.gameObject, PU.GetPlayer());
                                GameObject RoomObject = obj.gameObject.TryCast<GameObject>();
                                UdonBehaviour PrivateRoom = obj.transform.gameObject.GetComponentInChildren<UdonBehaviour>(RoomObject);
                                PrivateRoom.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonEvent);
                            }
                        }); 
                    }
                    break;
            }
        }
        public static void TargetedEvent(string udonevent, VRCPlayer player)
        {
            WorldWrapper.udonBehaviours.ToList().ForEach(UdonObject =>
            {
                if (UdonObject._eventTable.ContainsKey(udonevent))
                {
                    UW.SetEventOwner(UdonObject.gameObject, player.prop_Player_0);
                    UdonObject.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, udonevent);
                }
            }); 
        }
        public static VRC.Player GrabOwner(this GameObject gameObject)
        {
            VRC.Player gay = null;
            PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList().ForEach(player =>
            {
                if (player.field_Private_VRCPlayerApi_0.IsOwner(gameObject))
                {
                    gay =  player;
                }
            });
            return gay; 
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