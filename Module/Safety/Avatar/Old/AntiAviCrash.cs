using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Trinity.Module.Safety
{
    class AntiAviCrash : BaseModule, OnAssetBundleLoadEvent
    {
        string[] blacklistShaders;
        string[] blacklistMesh;
        private int maxAudio;
        private int maxLight;
        private int maxDynamicBonesCollider;
        private int maxPoly;
        private int maxMatirial;
        private int maxCloth;
        private int maxColliders;
        private int maxParticles;
        private int maxParticleSystems;
        private int maxAnimators;
        private int ParticleLimiter;
        private Shader defaultShader;
        public AntiAviCrash() : base("Anti All", "Remove Crashers from Avatars", Main.Instance.Avatarbutton, null, true, false)
        {
            this.maxAudio = Main.Instance.Config.getConfigInt("MaxAudioSources", 10);
            this.maxLight = Main.Instance.Config.getConfigInt("MaxLightSources", 0);
            this.maxAnimators = Main.Instance.Config.getConfigInt("MaxAnimators", 1);
            this.maxParticleSystems = Main.Instance.Config.getConfigInt("MaxParticleSystems", 1);
            this.maxParticles = Main.Instance.Config.getConfigInt("MaxParticles", 5000);
            this.maxDynamicBonesCollider = Main.Instance.Config.getConfigInt("MaxDynamicBoneColliders", 5);
            this.maxMatirial = Main.Instance.Config.getConfigInt("MaxMatirials", 20);
            this.maxCloth = Main.Instance.Config.getConfigInt("MaxCloth", 1);
            this.maxColliders = Main.Instance.Config.getConfigInt("MaxColliders", 0);
            this.maxPoly = Main.Instance.Config.getConfigInt("MaxPolys", 200000);
            defaultShader = Shader.Find("VRChat/PC/Toon Lit Cutout");
            blacklistShaders = File.ReadAllLines("Trinity/BlackList/Avatar/Shader.txt");
            blacklistMesh = File.ReadAllLines("Trinity/BlackList/Avatar/Mesh.txt");
        }

        public override void OnEnable()
        {
            Main.Instance.OnAssetBundleLoadEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnAssetBundleLoadEvents.Remove(this);
        }

        public bool OnAvatarAssetBundleLoad(GameObject avatar, string avatarID)
        {
             
            SkinnedMeshRenderer[] skinnedMeshRenderers = avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            MeshFilter[] meshFilters = avatar.GetComponentsInChildren<MeshFilter>(true);

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRenderers[i];
                bool destroyed = false;
                if (!skinnedMeshRenderer.sharedMesh.isReadable)
                {
                    UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                    LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted unreadable Mesh", false, false);
                    LogHandler.LogDebug($"[Anti AviCrash] Deleted Unreadable Mesh ");
                    continue;
                }

                for (int j = 0; j < blacklistMesh.Length; j++)
                {
                    if (skinnedMeshRenderer.name.ToLower().Contains(blacklistMesh[j]))
                    {
                        LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted blackListed Mesh " + skinnedMeshRenderer.name, false, false);
                        LogHandler.LogDebug($"[Anti AviCrash] Deleted BlackListed Mesh ");
                        UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                int polyCount = 0;
                for (int j = 0; j < skinnedMeshRenderer.sharedMesh.subMeshCount; j++)
                {
                    polyCount += skinnedMeshRenderer.sharedMesh.GetTriangles(j).Length / 3;
                    if (polyCount >= this.maxPoly)
                    {
                        UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                        LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted Mesh with too many polys", false, false);
                        LogHandler.LogDebug($"[Anti AviCrash] Deleted Mesh With Too Many Polys ");
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                Material[] materials = skinnedMeshRenderer.materials;
                if (materials.Length >= maxMatirial)
                {
                    UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer, true);
                    LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted Mesh with " + materials.Length + " materials", false, false);
                    LogHandler.LogDebug($"[Anti AviCrash] Deleted Mesh With {materials.Length} Materials ");
                    continue;
                }

                for (int j = 0; j < materials.Length; j++)
                {
                    Shader shader = materials[j].shader;
                    for (int k = 0; k < blacklistShaders.Length; k++)
                    {
                        if (shader.name.ToLower().Contains(blacklistShaders[k]))
                        {
                            LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] replaced Shader " + shader.name, false, false);
                            LogHandler.LogDebug($"[Anti AviCrash] Replaced Shader {shader.name} ");
                            shader = defaultShader;
                            continue;
                        }
                    }
                }
            }

            for (int i = 0; i < meshFilters.Length; i++)
            {
                MeshFilter meshFilter = meshFilters[i];
                if (!meshFilter.sharedMesh.isReadable)
                {
                    UnityEngine.Object.DestroyImmediate(meshFilter, true);
                    LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted unreadable Mesh", false, false);
                    LogHandler.LogDebug($"[Anti AviCrash] Deleted Unreadable Mesh ");
                    continue;
                }

                bool destroyed = false;

                for (int j = 0; j < blacklistMesh.Length; j++)
                {
                    if (meshFilter.name.ToLower().Contains(blacklistMesh[j]))
                    {
                        LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted blackListed Mesh " + meshFilter.name, false, false);
                        LogHandler.LogDebug($"[Anti AviCrash] Deleted BlackListed Mesh {meshFilter.name} ");
                        UnityEngine.Object.DestroyImmediate(meshFilter, true);
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                int polyCount = 0;
                for (int j = 0; j < meshFilter.sharedMesh.subMeshCount; j++)
                {
                    polyCount += meshFilter.sharedMesh.GetTriangles(j).Length / 3;
                    if (polyCount >= this.maxPoly)
                    {
                        UnityEngine.Object.DestroyImmediate(meshFilter, true);
                        LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted Mesh with too many polys", false, false);
                        LogHandler.LogDebug($"[Anti AviCrash] Deleted Mesh With Too Many Polys ");
                        destroyed = true;
                        break;
                    }
                }
                if (destroyed)
                    continue;

                MeshRenderer meshRenderer = meshFilter.gameObject.GetComponent<MeshRenderer>();
                Material[] materials = meshRenderer.materials;
                if (materials.Length >= maxMatirial)
                {
                    UnityEngine.Object.DestroyImmediate(meshFilter, true);
                    LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted Mesh with " + materials.Length + " materials", false, false);
                    LogHandler.LogDebug($"[Anti AviCrash] Deleted Mesh With {materials.Length} Materials");
                    continue;
                }
                for (int j = 0; j < materials.Length; j++)
                {
                    Shader shader = materials[j].shader;
                    for (int k = 0; k < blacklistShaders.Length; k++)
                    {
                        if (shader.name.ToLower().Contains(blacklistShaders[k]))
                        {
                            LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] replaced Shader " + shader.name, false, false);
                            LogHandler.LogDebug($"[Anti AviCrash] Replaced Shader {shader.name} ");
                            shader = defaultShader;
                            continue;
                        }
                    }
                }
            }
            ParticleSystem particalSystems = avatar.GetComponent<ParticleSystem>();
            ParticleLimiter++;
            if (particalSystems)
            {
                if (ParticleLimiter > maxParticleSystems)
                {
                    UnityEngine.Object.DestroyImmediate(particalSystems.gameObject, true);
                    LogHandler.Log(LogHandler.Colors.Red, "[AnitCrash] deleted " + ParticleLimiter + " ParticleSystems", false, false);
                    LogHandler.LogDebug($"[Anti AviCrash] Deleted {ParticleLimiter} ParticleSystems ");
                }
            }
            if (particalSystems.maxParticles <= maxParticles)
            {
                for (int i = 0; i < maxParticles; i++)
                {
                    UnityEngine.Object.DestroyImmediate(particalSystems.gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red, "[AnitCrash] deleted " + maxParticles + " Particles", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxParticles} Particles ");
            }
            AudioSource[] audioSources = avatar.GetComponentsInChildren<AudioSource>();
            if (audioSources.Length >= maxAudio)
            {
                for (int i = 0; i < maxAudio; i++)
                {
                    UnityEngine.Object.DestroyImmediate(audioSources[i].gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted " + maxAudio + " AudioSources", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxAudio} AudioSources ");
            }
            Light[] lights = avatar.GetComponentsInChildren<Light>();
            if (lights.Length >= maxLight)
            {
                for (int i = 0; i < maxLight; i++)
                {
                    UnityEngine.Object.DestroyImmediate(lights[i].gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted " + maxLight + " Lights", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxLight} Lights ");
            }
            Cloth[] cloths = avatar.GetComponentsInChildren<Cloth>();
            if (cloths.Length >= maxCloth)
            {
                for (int i = 0; i < maxCloth; i++)
                {
                    UnityEngine.Object.DestroyImmediate(cloths[i].gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted " + maxCloth + " Cloth", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxCloth} Cloth ");
            }
            Animator[] animators = avatar.GetComponentsInChildren<Animator>();
            if (animators.Length >= maxAnimators)
            {
                for (int i = 0; i < maxAnimators; i++)
                {
                    UnityEngine.Object.DestroyImmediate(animators[i].gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red, "[AnitCrash] deleted " + maxAnimators + " Lights", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxAnimators} Lights ");
            }
            Collider[] collider = avatar.GetComponentsInChildren<Collider>();
            if (collider.Length >= maxColliders)
            {
                for (int i = 0; i < maxColliders; i++)
                {
                    UnityEngine.Object.DestroyImmediate(collider[i].gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted " + maxColliders + " Colliders", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxColliders} Colliders");
            }
            DynamicBoneCollider[] dynamicBoneColliders = avatar.GetComponentsInChildren<DynamicBoneCollider>();
            if (dynamicBoneColliders.Length >= maxDynamicBonesCollider)
            {
                for (int i = 0; i < maxDynamicBonesCollider; i++)
                {
                    UnityEngine.Object.DestroyImmediate(dynamicBoneColliders[i].gameObject, true);
                }
                LogHandler.Log(LogHandler.Colors.Red,"[AnitCrash] deleted " + maxDynamicBonesCollider + " DynamicBoneColliders", false, false);
                LogHandler.LogDebug($"[Anti AviCrash] Deleted {maxDynamicBonesCollider} DynamicBoneColliders");
            }
            return true;
        }
    }
}
