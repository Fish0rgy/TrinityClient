using Trinity.Utilities;
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
    class AntiShader : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiShader() : base("Anti Shader", "Destroys Blacklisted Shaders", Main.Instance.Avatarbutton, null, true, false)
        { 
        }
        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Shader Enabled</color>");
            Main.Instance.OnObjectInstantiatedEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Shader Disabled</color>");
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

                    MunchenAntiCrash.ProcessRenderer(renderers[i], false, false, true, ref postProcessReport);
                }  
                if (postProcessReport.nukedShaders > 0)
                {
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postProcessReport.nukedShaders} Bad Shaders");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postProcessReport.nukedShaders} Bad Shaders");
                }
            }
            return true;
        }
    }
}
 
