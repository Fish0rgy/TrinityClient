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
    class AntiAudio : BaseModule, OnObjectInstantiatedEvent
    { 
        public AntiAudio() : base("Anti Audio", "Limit Audio Recources", Main.Instance.Avatarbutton, null, true, false)
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
                List<AudioSource> audioSources = MunchenAntiCrash.FindAllComponentsInGameObject<AudioSource>(instantiatedGameObject);

                int nukedAudioSources = 0;

                for (int i = MunchenAntiCrash.maxAudio; i < audioSources.Count; i++)
                {
                    if (audioSources[i] == null)
                    {
                        continue;
                    } 
                    if (audioSources[i].name.Contains("USpeak") == true)
                    {
                        continue;
                    }

                    UnityEngine.Object.DestroyImmediate(audioSources[i], true);

                    nukedAudioSources++;
                } 
                if (nukedAudioSources > 0)
                {
                    LogHandler.Log(LogHandler.Colors.Green,$"[AntiCrash] Removed {nukedAudioSources} From Avatar",false,false);
                }
            }
            return true;
        }
    }
}
