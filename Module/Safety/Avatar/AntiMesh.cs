using Trinity.Events;
using Trinity.SDK;
using Trinity.SDK.PatchAPI.Patches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Module.Safety.Avatar
{
    class AntiMesh : BaseModule, OnObjectInstantiatedEvent
    {
        public AntiMesh() : base("Anti Mesh", "Bad Mesh", Main.Instance.Avatarbutton, null, true, false)
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

                    MunchenAntiCrash.ProcessRenderer(renderers[i], true, false, false, ref postProcessReport);
                }
                if (postProcessReport.nukedMeshes > 0)
                {
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postProcessReport.nukedMeshes} Bad Meshs");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postProcessReport.nukedMeshes} Bad Meshs");
                }
            }
            return true;
        }
    }
}
