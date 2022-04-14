using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.PatchAPI.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.Safety.Avatar
{
    class AntiColliders : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiColliders() : base("Anti Colliders", "Limit Colliders Recources", Main.Instance.Avatarbutton, null, true, false)
        { 
        }
        public override void OnEnable()
        {
            Main.Instance.OnObjectInstantiatedEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnObjectInstantiatedEvents.Remove(this);
        }

        public bool OnObjectInstantiatedPatch(IntPtr assetPtr, Vector3 pos, Quaternion rot, byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer, _AssetManagement.ObjectInstantiateDelegate originalInstantiateDelegate)
        {
            GameObject potentialAvatar = new UnityEngine.Object(assetPtr).TryCast<GameObject>();
            bool containsPrefabName = potentialAvatar.name.StartsWith("prefab");
            IntPtr result = originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            GameObject instantiatedGameObject = new GameObject(result); 
            List<Collider> colliders = MunchenAntiCrash.FindAllComponentsInGameObject<Collider>(instantiatedGameObject);
            if (containsPrefabName == true)
            {
                int avatarIdStart = potentialAvatar.name.IndexOf('_') + 1;
                int avatarIdEnd = potentialAvatar.name.LastIndexOf('_');
                string avatarId = potentialAvatar.name.Substring(avatarIdStart, avatarIdEnd - avatarIdStart);
                int nukedColliders = 0;

                for (int i = 0; i < colliders.Count; i++)
                {
                    if (colliders[i] == null)
                    {
                        continue;
                    }

                    if (MunchenAntiCrash.ProcessCollider(colliders[i]) == true)
                    {
                        nukedColliders++;
                    }
                }
                if (nukedColliders > 0)
                { 
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {nukedColliders} Bad Colliders");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {nukedColliders} Bad Colliders");
                }
            }
            return true;
        }
    }
}
