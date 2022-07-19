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
    class AntiCloth : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiCloth() : base("Anti Cloth", "Limit Cloth Recources", Main.Instance.Avatarbutton, null, true, false)
        { 
        }
        public override void OnEnable()
        {
            MenuUI.Log("SAFETY: <color=green>Anti Cloth Enabled</color>");
            Main.Instance.OnObjectInstantiatedEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SAFETY: <color=red>Anti Cloth Disabled</color>");
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
                List<Cloth> clothes = MunchenAntiCrash.FindAllComponentsInGameObject<Cloth>(instantiatedGameObject);
                AntiCrashClothPostProcess postProcessReport = new AntiCrashClothPostProcess();

                for (int i = 0; i < clothes.Count; i++)
                {
                    if (clothes[i] == null)
                    {
                        continue;
                    }

                    postProcessReport = MunchenAntiCrash.ProcessCloth(clothes[i], postProcessReport);
                } 
                if (postProcessReport.nukedCloths > 0)
                { 
                    LogHandler.Log(LogHandler.Colors.Green, $"[AntiCrash] Removed {postProcessReport.nukedCloths} Bad Cloth");
                    LogHandler.LogDebug($"[Anti AviCrash] Removed {postProcessReport.nukedCloths} Bad Cloth");
                }
            }
            return true;
        }
    }
}
