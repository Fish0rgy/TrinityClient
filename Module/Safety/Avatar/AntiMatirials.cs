using Area51.Events;
using Area51.SDK;
using Area51.SDK.PatchAPI.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Area51.Module.Safety.Avatar
{
    class AntiMatirials : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiMatirials() : base("Anti Materials", "Limit Material Recources", Main.Instance.Avatarbutton, null, true, false)
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
            Animator[] animators = potentialAvatar.GetComponentsInChildren<Animator>(instantiatedGameObject);
            if (containsPrefabName == true)
            {
                int avatarIdStart = potentialAvatar.name.IndexOf('_') + 1;
                int avatarIdEnd = potentialAvatar.name.LastIndexOf('_');
                string avatarId = potentialAvatar.name.Substring(avatarIdStart, avatarIdEnd - avatarIdStart);
                List<Renderer> renderers = MunchenAntiCrash.FindAllComponentsInGameObject<Renderer>(instantiatedGameObject);
                AntiCrashRendererPostProcess postProcessReport = new AntiCrashRendererPostProcess();

                for (int i = 0; i < renderers.Count; i++)
                {
                    if (renderers[i] == null)
                    {
                        continue;
                    }

                    MunchenAntiCrash.ProcessRenderer(renderers[i], false, true, false, ref postProcessReport);
                }
                if (postProcessReport.nukedMaterials > 0)
                {
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postProcessReport.nukedMaterials} Bad Materials");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postProcessReport.nukedMaterials} Bad Materials");
                }
            }
            return true;
        }
    }
}
