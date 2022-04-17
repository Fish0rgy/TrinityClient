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
    class AntiAnimators : BaseModule, OnObjectInstantiatedEvent
    {
        private int maxAnimators = 25;
        public AntiAnimators() : base("Anti Animators", "Limit Animator Recources", Main.Instance.Avatarbutton, null, true, false)
        { 
        }
        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Animators Enabled</color>");
            Main.Instance.OnObjectInstantiatedEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Animators Disabled</color>");
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
                if (animators.Length >= maxAnimators)
                {
                    for (int i = 0; i < maxAnimators; i++)
                    {
                        UnityEngine.Object.DestroyImmediate(animators[i].gameObject, true);
                    }
                    LogHandler.Log(LogHandler.Colors.Red, "[AnitCrash] deleted " + animators.Length + " Light Sources", false, false);
                    LogHandler.LogDebug($"[Anti AviCrash] Deleted {animators.Length} Light Sources");
                }
            }
            return true;
        }
    }
}
