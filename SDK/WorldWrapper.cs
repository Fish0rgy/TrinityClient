﻿using Trinity.Module.World.World_Hacks.Just_B;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib.XrefScans;
using VRC.Animation;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;

namespace Trinity.SDK
{
    class WorldWrapper
    {
        public static VRC_Pickup[] vrc_Pickups;
        public static UdonBehaviour[] udonBehaviours;
        public static VRC_Trigger[] vrc_Triggers; 
        public static string GetInstance() => PlayerWrapper.GetAPIUser(PlayerWrapper.LocalPlayer).instanceId;//  CurrentWorldInstance().instanceId;
        public static string GetID() => CurrentWorld().id;
        public static string GetLocation() => PlayerWrapper.LocalPlayer.GetAPIUser().location;
        public static ApiWorld CurrentWorld() => RoomManager.field_Internal_Static_ApiWorld_0;
        public static ApiWorldInstance CurrentWorldInstance() => RoomManager.field_Internal_Static_ApiWorldInstance_0;
        public static string GetWorldID => PlayerWrapper.GetAPIUser(PlayerWrapper.LocalPlayer).location;
        public static void Init()
        {
            vrc_Pickups = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
            udonBehaviours = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>();
            vrc_Triggers = UnityEngine.Object.FindObjectsOfType<VRC_Trigger>();
            PlayerWrapper.PlayersActorID = new Dictionary<int, VRC.Player>();
            for (int i = 0; i < Main.Instance.OnWorldInitEventArray.Length; i++) { Main.Instance.OnWorldInitEventArray[i].OnWorldInit(); }
            
        }
    }
}
