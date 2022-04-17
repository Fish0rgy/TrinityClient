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
    class AntiLight : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiLight() : base("Anti Light", "Limit Light Recources", Main.Instance.Avatarbutton, null, true, false)
        { 
        }
        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Light Sources Enabled</color>");
            Main.Instance.OnObjectInstantiatedEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Light Sources Disabled</color>");
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
                List<Light> lightSources = MunchenAntiCrash.FindAllComponentsInGameObject<Light>(instantiatedGameObject);
                AntiCrashLightSourcePostProcess postProcessReport = new AntiCrashLightSourcePostProcess();

                for (int i = 0; i < lightSources.Count; i++)
                {
                    if (lightSources[i] == null)
                    {
                        continue;
                    }

                    postProcessReport = MunchenAntiCrash.ProcessLight(lightSources[i], postProcessReport.nukedLightSources, postProcessReport.lightSourceCount);
                } 
                if (postProcessReport.nukedLightSources > 0)
                { 
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postProcessReport.nukedLightSources} Bad Light Sources");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postProcessReport.nukedLightSources} Bad Light Sources");
                }
            }
            return true;
        }
    }
}
