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
    class AntiBones : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiBones() : base("Anti DynamicBones", "Limit DynamicBones Recources", Main.Instance.Avatarbutton, null, true, false)
        { 
        }
        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Dynamic Bones Enabled</color>");
            Main.Instance.OnObjectInstantiatedEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Dynamic Bones Disabled</color>");
            Main.Instance.OnObjectInstantiatedEvents.Remove(this);
        }

        public bool OnObjectInstantiatedPatch(IntPtr assetPtr, Vector3 pos, Quaternion rot, byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer, _AssetManagement.ObjectInstantiateDelegate originalInstantiateDelegate)
        {
            GameObject potentialAvatar = new UnityEngine.Object(assetPtr).TryCast<GameObject>();
            bool containsPrefabName = potentialAvatar.name.StartsWith("prefab");
            IntPtr result = originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            GameObject instantiatedGameObject = new GameObject(result);
            Animator[] animators = potentialAvatar.GetComponentsInChildren<Animator>(instantiatedGameObject);
            if (containsPrefabName == true)
            {
                int avatarIdStart = potentialAvatar.name.IndexOf('_') + 1;
                int avatarIdEnd = potentialAvatar.name.LastIndexOf('_');
                string avatarId = potentialAvatar.name.Substring(avatarIdStart, avatarIdEnd - avatarIdStart);
                List<DynamicBoneCollider> dynamicBoneColliders = MunchenAntiCrash.FindAllComponentsInGameObject<DynamicBoneCollider>(instantiatedGameObject);
                AntiCrashDynamicBoneColliderPostProcess postDynamicBoneColliderProcessReport = new AntiCrashDynamicBoneColliderPostProcess();

                for (int i = 0; i < dynamicBoneColliders.Count; i++)
                {
                    if (dynamicBoneColliders[i] == null)
                    {
                        continue;
                    }

                    postDynamicBoneColliderProcessReport = MunchenAntiCrash.ProcessDynamicBoneCollider(dynamicBoneColliders[i], postDynamicBoneColliderProcessReport.nukedDynamicBoneColliders, postDynamicBoneColliderProcessReport.dynamicBoneColiderCount);
                }

                //DynamicBone
                List<DynamicBone> dynamicBones = MunchenAntiCrash.FindAllComponentsInGameObject<DynamicBone>(instantiatedGameObject);
                AntiCrashDynamicBonePostProcess postDynamicBoneProcessReport = new AntiCrashDynamicBonePostProcess();

                for (int i = 0; i < dynamicBones.Count; i++)
                {
                    if (dynamicBones[i] == null)
                    {
                        continue;
                    }

                    postDynamicBoneProcessReport = MunchenAntiCrash.ProcessDynamicBone(dynamicBones[i], postDynamicBoneProcessReport.nukedDynamicBones, postDynamicBoneProcessReport.dynamicBoneCount);
                } 
                if (postDynamicBoneProcessReport.nukedDynamicBones > 0)
                { 
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postDynamicBoneProcessReport.nukedDynamicBones} Bad DynamicBones");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postDynamicBoneProcessReport.nukedDynamicBones} Bad DynamicBones");
                }

                if (postDynamicBoneColliderProcessReport.nukedDynamicBoneColliders > 0)
                { 
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postDynamicBoneColliderProcessReport.nukedDynamicBoneColliders} Bad DynamicBoneColliders");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postDynamicBoneColliderProcessReport.nukedDynamicBoneColliders} Bad DynamicBoneColliders");
                }
            }
            return true;
        }
    }
}
