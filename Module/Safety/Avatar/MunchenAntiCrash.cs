using Trinity.Utilities;
 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using System.Text.RegularExpressions; 

namespace Trinity.Module.Safety.Avatar
{
    //thanks killer Munchen Server: https://discord.gg/DysbzmWTcV
    internal class AntiCrashRendererPostProcess
    {
        internal int nukedMeshes;
        internal int meshCount;
        internal uint polygonCount;
        internal int nukedMaterials;
        internal int materialCount;
        internal int nukedShaders;
        internal int shaderCount;
        internal bool removedBlendshapeKeys;
    }

    internal class AntiCrashMaterialPostProcess
    {
        internal int nukedMaterials;
        internal int materialCount;
    }

    internal class AntiCrashShaderPostProcess
    {
        internal int nukedShaders;
        internal int shaderCount;
    }

    internal class AntiCrashParticleSystemPostProcess
    {
        internal int nukedParticleSystems;
        internal int currentParticleCount;
    }

    internal class AntiCrashClothPostProcess
    {
        internal int nukedCloths;
        internal int clothCount;
        internal int currentVertexCount;
    }

    internal class AntiCrashDynamicBonePostProcess
    {
        internal int nukedDynamicBones;
        internal int dynamicBoneCount;
    }

    internal class AntiCrashDynamicBoneColliderPostProcess
    {
        internal int nukedDynamicBoneColliders;
        internal int dynamicBoneColiderCount;
    }

    internal class AntiCrashLightSourcePostProcess
    {
        internal int nukedLightSources;
        internal int lightSourceCount;
    }
    class MunchenAntiCrash
    {
        public static string[] BlackListedShaders = File.ReadAllLines("Trinity/BlackList/Avatar/Shader.txt");
        public static uint MaxPolygons = 2500000U;
        public static int MaxMeshes = 250;
        public static int MaxMaterials = 300;
        public static int MaxDynamicBones = 75;
        public static int MaxDynamicBoneColliders = 50;
        public static int MaxAudioSources = 150;
        public static int MaxAnimators = 25;
        public static int MaxConstraints = 200;
        public static int MaxShaders = 100;
        public static int MaxLightSources = 8;
        public static int MaxColliders = 20;
        public static int MaxSpringJoints = 15;
        public static int MaxTransforms = 3500;
        public static int MaxMonobehaviours = 1500;
        public static int MaxComponentsTotal = 6500;
        public static int MaxDepth = 125;
        public static int MaxChildren = 125;
        public static float MaxHeight = 10f;
        public static int MaxCloth = 75;
        public static int MaxClothVertices = 15000;
        public static float MaxClothSolverFrequency = 180f;
        public static int MaxRigidbodies = 25;
        public static float MaxRigidbodyMass = 10000f;
        public static float MaxRigidbodyAngularVelocity = 100f;
        public static float MaxRigidbodyDepenetrationVelocity = 100f;
        public static int MaxParticleLimit = 10000;
        public static uint MaxParticleMeshVertices = 1000000U;
        public static int MaxParticleCollisionShapes = 1024;
        public static int MaxParticleRibbons = 10000;
        public static float MaxParticleEmissionRate = 500f;
        public static int MaxParticleEmissionBurstCount = 125;
        public static int MaxParticleTrails = 64;
        public static float MaxParticleSimulationSpeed = 5f;
        public static int MaxShaderLoopLimit = 128;
        public static int MaxShaderGeometryLimit = 25;
        public static float MaxShaderTesselationPower = 2.5f;
        public static Shader defaultShader;
        internal static void ProcessRenderer(Renderer renderer, bool limitPolygons, bool limitMaterials, bool limitShaders, ref AntiCrashRendererPostProcess previousProcess)
        {
            bool flag = !ProcessMeshPolygons(renderer, ref previousProcess.meshCount, ref previousProcess.nukedMeshes, ref previousProcess.polygonCount, ref previousProcess.removedBlendshapeKeys);
            if (flag)
            {
                AntiCrashMaterialPostProcess antiCrashMaterialPostProcess = ProcessMaterials(renderer, previousProcess.nukedMaterials, previousProcess.materialCount);
                previousProcess.nukedMaterials = antiCrashMaterialPostProcess.nukedMaterials;
                previousProcess.materialCount = antiCrashMaterialPostProcess.materialCount;

                AntiCrashShaderPostProcess antiCrashShaderPostProcess = ProcessShaders(renderer, previousProcess.nukedShaders, previousProcess.shaderCount);
                previousProcess.nukedShaders = antiCrashShaderPostProcess.nukedShaders;
                previousProcess.shaderCount = antiCrashShaderPostProcess.shaderCount;

            }
        }
        internal static bool ProcessJoint(Joint joint, ref int currentSpringJoints)
        {
            bool overmax = currentSpringJoints >= MaxSpringJoints;
            bool result;
            if (overmax)
            {
                Object.DestroyImmediate(joint.gameObject, true);
                result = true;
            }
            else
            {
                currentSpringJoints++;
                joint.connectedMassScale = Clamp(joint.connectedMassScale, -25f, 25f);
                joint.massScale = Clamp(joint.massScale, -25f, 25f);
                joint.breakTorque = Clamp(joint.breakTorque, -100f, 100f);
                joint.breakForce = Clamp(joint.massScale, -100f, 100f);
                SpringJoint springJoint = joint as SpringJoint;
                bool nullcheck = springJoint != null;
                if (nullcheck)
                {
                    springJoint.damper = Clamp(springJoint.damper, -100f, 100f);
                    springJoint.maxDistance = Clamp(springJoint.maxDistance, -100f, 100f);
                    springJoint.minDistance = Clamp(springJoint.minDistance, -100f, 100f);
                    springJoint.spring = Clamp(springJoint.spring, -100f, 100f);
                    springJoint.tolerance = Clamp(springJoint.tolerance, -100f, 100f);
                }
                result = false;
            }
            return result;
        }
        internal static bool ProcessMeshPolygons(Renderer renderer, ref int currentMeshes, ref int currentNukedMeshes, ref uint currentPolygonCount, ref bool removedBlendshapeKeys)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = renderer.TryCast<SkinnedMeshRenderer>();
            MeshFilter component = renderer.GetComponent<MeshFilter>();
            Mesh mesh = ((skinnedMeshRenderer != null) ? skinnedMeshRenderer.sharedMesh : null) ?? ((component != null) ? component.sharedMesh : null);
            bool checknullmesh = mesh == null;
            bool result;
            if (checknullmesh)
            {
                result = false;
            }
            else
            {
                bool maxMesh = currentMeshes >= MaxMeshes;
                if (maxMesh)
                {
                    currentNukedMeshes++;
                    Object.DestroyImmediate(renderer.gameObject, true);
                    result = true;
                }
                else
                {
                    bool NullCheck = AntiBlendShapeCrash && skinnedMeshRenderer != null;
                    if (NullCheck)
                    {
                        bool BadPoly = false;
                        bool BadRevert = false;
                        bool badKey22 = false;
                        bool badKey56 = false;
                        bool BadSlant = false;
                        for (int i = 0; i < mesh.blendShapeCount; i++)
                        {
                            string text = mesh.GetBlendShapeName(i).ToLower(); 
                            bool reverted = text.Contains("reverted");
                            if (reverted)
                            {
                                BadPoly = true;
                            }
                            bool posetorest = text.Contains("posetorest");
                            if (posetorest)
                            {
                                BadRevert = true;
                            }
                            else
                            {
                                bool key22 = text.Contains("key 22");
                                if (key22)
                                {
                                    badKey22 = true;
                                }
                                else
                                {
                                    bool key56 = text.Contains("key 56");
                                    if (key56)
                                    {
                                        badKey56 = true;
                                    }
                                    else
                                    {
                                        bool slant = text.Contains("slant");
                                        if (slant)
                                        {
                                            BadSlant = true;
                                        }
                                    }
                                }
                            }
                        }
                        bool Bad = BadPoly && BadRevert && badKey22 && badKey56 && BadSlant;
                        if (Bad)
                        {
                            removedBlendshapeKeys = true;
                            mesh.ClearBlendShapes();
                        }
                    }
                    int SubCount;
                    try
                    {
                        SubCount = mesh.subMeshCount;
                    }
                    catch (System.Exception)
                    {
                        SubCount = 0;
                    }
                    try
                    {
                        renderer.GetSharedMaterials(antiCrashTempMaterialsList);
                        int ProcessedIndex = ProcessMesh(mesh, SubCount, ref currentNukedMeshes, ref currentPolygonCount);
                        bool MaxIndexCound = ProcessedIndex != -1;
                        if (MaxIndexCound)
                        {
                            antiCrashTempMaterialsList.RemoveRange(ProcessedIndex, antiCrashTempMaterialsList.Count - ProcessedIndex);
                            renderer.SetMaterialArray((Il2CppReferenceArray<Material>)antiCrashTempMaterialsList.ToArray());
                        }
                        bool maxSub = SubCount + 2 < renderer.GetMaterialCount();
                        if (maxSub)
                        {
                            Object.Destroy(renderer.gameObject);
                            return true;
                        }
                    }
                    catch (System.Exception e)
                    {
                        Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, $"[ERROR] \nClass: AntiCrash\nHandle: ProcessMeshPoly\nError: {e}", false, false);
                    }
                    currentMeshes++;
                    result = false;
                }
            }
            return result;
        }

        internal static AntiCrashMaterialPostProcess ProcessMaterials(Renderer renderer, int currentNukedMaterials, int currentMaterialCount)
        {
            int materialCount = renderer.GetMaterialCount();
            int ActualCount = currentMaterialCount + materialCount;
            bool MaxMaterialCount = ActualCount > MaxMaterials;
            if (MaxMaterialCount)
            {
                int GoodCounter = ((currentMaterialCount < MaxMaterials) ? MaxMaterials : 0);
                int BadCounter = ((GoodCounter == 0) ? materialCount : (ActualCount - MaxMaterials));
                bool Max = GoodCounter > materialCount;
                if (Max)
                {
                    GoodCounter = materialCount;
                }
                int MaxGoodMats = materialCount - GoodCounter;
                bool flag3 = BadCounter > MaxGoodMats;
                if (flag3)
                {
                    BadCounter = MaxGoodMats;
                }
                currentNukedMaterials += BadCounter;
                ActualCount -= BadCounter;
                bool flag4 = materialCount == BadCounter;
                if (flag4)
                {
                    Object.DestroyImmediate(renderer.gameObject, true);
                }
                else
                {
                    Il2CppSystem.Collections.Generic.List<Material> MaterialList = new Il2CppSystem.Collections.Generic.List<Material>();
                    renderer.GetSharedMaterials(MaterialList);
                    MaterialList.RemoveRange(GoodCounter, BadCounter);
                    renderer.materials = (Il2CppReferenceArray<Material>)MaterialList.ToArray();
                }
            }
            currentMaterialCount = ActualCount;
            return new AntiCrashMaterialPostProcess
            {
                nukedMaterials = currentNukedMaterials,
                materialCount = currentMaterialCount
            };
        }

        internal static AntiCrashShaderPostProcess ProcessShaders(Renderer renderer, int currentNukedShaders, int currentShaderCount)
        {
            bool checknull = renderer == null;
            AntiCrashShaderPostProcess result;
            if (checknull)
            {
                result = new AntiCrashShaderPostProcess
                {
                    nukedShaders = currentNukedShaders,
                    shaderCount = currentShaderCount
                };
            }
            else
            {
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    bool nullcheck = !(renderer.materials[i] == null);
                    if (nullcheck)
                    {
                        currentShaderCount++;
                        bool badshadercheck = ProcessShader(renderer.materials[i]);
                        if (badshadercheck)
                        {
                            currentNukedShaders++;
                        }
                    }
                }
                result = new AntiCrashShaderPostProcess
                {
                    nukedShaders = currentNukedShaders,
                    shaderCount = currentShaderCount
                };
            }
            return result;
        }

        internal static AntiCrashClothPostProcess ProcessCloth(Cloth cloth, AntiCrashClothPostProcess previousReport)
        {
            bool flag = previousReport.clothCount >= MaxCloth;
            AntiCrashClothPostProcess result;
            if (flag)
            {
                previousReport.nukedCloths++;
                Object.DestroyImmediate(cloth.gameObject, true);
                result = new AntiCrashClothPostProcess
                {
                    nukedCloths = previousReport.nukedCloths,
                    clothCount = previousReport.clothCount,
                    currentVertexCount = previousReport.currentVertexCount
                };
            }
            else
            {
                SkinnedMeshRenderer component = cloth.GetComponent<SkinnedMeshRenderer>();
                Mesh mesh = ((component != null) ? component.sharedMesh : null);
                bool flag2 = mesh == null;
                if (flag2)
                {
                    previousReport.nukedCloths++;
                    Object.DestroyImmediate(cloth.gameObject, true);
                    result = new AntiCrashClothPostProcess
                    {
                        nukedCloths = previousReport.nukedCloths,
                        clothCount = previousReport.clothCount,
                        currentVertexCount = previousReport.currentVertexCount
                    };
                }
                else
                {
                    int num = previousReport.currentVertexCount + mesh.vertexCount;
                    bool flag3 = num >= MaxClothVertices;
                    if (flag3)
                    {
                        previousReport.nukedCloths++;
                        Object.DestroyImmediate(cloth.gameObject, true);
                        result = new AntiCrashClothPostProcess
                        {
                            nukedCloths = previousReport.nukedCloths,
                            clothCount = previousReport.clothCount,
                            currentVertexCount = previousReport.currentVertexCount
                        };
                    }
                    else
                    {
                        cloth.clothSolverFrequency = Clamp(cloth.clothSolverFrequency, 0f, MaxClothSolverFrequency);
                        previousReport.currentVertexCount = num;
                        previousReport.clothCount++;
                        result = new AntiCrashClothPostProcess
                        {
                            nukedCloths = previousReport.nukedCloths,
                            clothCount = previousReport.clothCount,
                            currentVertexCount = previousReport.currentVertexCount
                        };
                    }
                }
            }
            return result;
        }

        internal static void ProcessParticleSystem(ParticleSystem particleSystem, ref AntiCrashParticleSystemPostProcess post)
        {
            ParticleSystemRenderer component = particleSystem.GetComponent<ParticleSystemRenderer>();
            bool NullComponent = component == null;
            if (NullComponent)
            {
                post.nukedParticleSystems++;
                Object.DestroyImmediate(particleSystem, true);
            }
            else
            {
                particleSystem.main.ringBufferMode = 0;
                particleSystem.main.simulationSpeed = Clamp(particleSystem.main.simulationSpeed, 0f, MaxParticleSimulationSpeed);
                particleSystem.collision.maxCollisionShapes = Clamp(particleSystem.collision.maxCollisionShapes, 0, MaxParticleCollisionShapes);
                particleSystem.trails.ribbonCount = Clamp(particleSystem.trails.ribbonCount, 0, MaxParticleTrails);
                particleSystem.emissionRate = Clamp(particleSystem.emissionRate, 0f, MaxParticleEmissionRate);
                for (int i = 0; i < particleSystem.emission.burstCount; i++)
                {
                    ParticleSystem.Burst burst = particleSystem.emission.GetBurst(i);
                    burst.maxCount = Clamp3(burst.maxCount, 0, (short)MaxParticleEmissionBurstCount);
                    burst.cycleCount = Clamp(burst.cycleCount, 0, MaxParticleEmissionBurstCount);
                    particleSystem.emission.SetBurst(i, burst);
                }
                int ParticleCount = MaxParticleLimit - post.currentParticleCount;
                bool checkP = ParticleCount <= 0 && particleSystem.maxParticles > 100;
                if (checkP)
                {
                    particleSystem.maxParticles = 100;
                }
                else
                {
                    bool CheckMax = particleSystem.maxParticles > ParticleCount;
                    if (CheckMax)
                    {
                        particleSystem.maxParticles = ParticleCount;
                    }
                }
                bool CheckRenderIsMesh = component.renderMode == ParticleSystemRenderMode.Mesh;
                if (CheckRenderIsMesh)
                {
                    Il2CppReferenceArray<Mesh> il2CppReferenceArray = new Il2CppReferenceArray<Mesh>((long)component.meshCount);
                    component.GetMeshes(il2CppReferenceArray);
                    uint polyCount = 0U;
                    int nukedMesh = 0;
                    bool nullAndMatch = component.mesh != null && duplicatedMeshNameRegex.IsMatch(component.mesh.name);
                    if (nullAndMatch)
                    {
                        component.enabled = false;
                        particleSystem.playOnAwake = false;
                        bool isPlaying = particleSystem.isPlaying;
                        if (isPlaying)
                        {
                            particleSystem.Stop();
                        }
                    }
                    foreach (Mesh mesh in il2CppReferenceArray)
                    {
                        int subMeshCount;
                        try
                        {
                            subMeshCount = mesh.subMeshCount;
                        }
                        catch (System.Exception)
                        {
                            subMeshCount = 0;
                        }
                        component.GetSharedMaterials(antiCrashTempMaterialsList);
                        int indexCount = ProcessMesh(mesh, subMeshCount, ref nukedMesh, ref polyCount);
                        bool CheckC = indexCount != -1;
                        if (CheckC)
                        {
                            antiCrashTempMaterialsList.RemoveRange(indexCount, antiCrashTempMaterialsList.Count - indexCount);
                        }
                        foreach (Material material in antiCrashTempMaterialsList)
                        {
                            bool nullcheck = !(material == null);
                            if (nullcheck)
                            {
                                ProcessShader(material);
                            }
                        }
                        bool checkchangec = indexCount != -1;
                        if (checkchangec)
                        {
                            component.SetMaterialArray((Il2CppReferenceArray<Material>)antiCrashTempMaterialsList.ToArray());
                        }
                    }
                    bool badPlyCount = (ulong)polyCount * (ulong)((long)particleSystem.maxParticles) > (ulong)MaxParticleMeshVertices;
                    if (badPlyCount)
                    {
                        int particlesycount = (particleSystem.maxParticles = (int)(MaxParticleMeshVertices / polyCount));
                    }
                }
                bool checksystemcount = particleSystem.maxParticles == 0;
                if (checksystemcount)
                {
                    post.nukedParticleSystems++;
                    UnityEngine.Object.DestroyImmediate(particleSystem, true);
                }
                post.currentParticleCount += particleSystem.maxParticles;
            }
        } 
        internal static AntiCrashDynamicBonePostProcess ProcessDynamicBone(DynamicBone dynamicBone, int currentNukedDynamicBones, int currentDynamicBones)
        {
            if (currentDynamicBones >= MaxDynamicBones)
            {
                currentNukedDynamicBones++;

                Object.DestroyImmediate(dynamicBone, true);

                return new AntiCrashDynamicBonePostProcess
                {
                    nukedDynamicBones = currentNukedDynamicBones,

                    dynamicBoneCount = currentDynamicBones
                };
            }

            currentDynamicBones++;

            //Safety
            dynamicBone.m_UpdateRate = Clamp(dynamicBone.m_UpdateRate, 0f, 60f);
            dynamicBone.m_Radius = Clamp(dynamicBone.m_Radius, 0f, 2f);
            dynamicBone.m_EndLength = Clamp(dynamicBone.m_EndLength, 0f, 10f);
            dynamicBone.m_DistanceToObject = Clamp(dynamicBone.m_DistanceToObject, 0f, 1f);

            //EndOffset Safety - Start
            Vector3 newEndOffset = dynamicBone.m_EndOffset;

            newEndOffset.x = Clamp(newEndOffset.x, -5f, 5f);
            newEndOffset.y = Clamp(newEndOffset.y, -5f, 5f);
            newEndOffset.z = Clamp(newEndOffset.z, -5f, 5f);

            dynamicBone.m_EndOffset = newEndOffset;
            //EndOffset Safety - End

            //Gravity Safety - Start
            Vector3 newGravity = dynamicBone.m_Gravity;

            newGravity.x = Clamp(newGravity.x, -5f, 5f);
            newGravity.y = Clamp(newGravity.y, -5f, 5f);
            newGravity.z = Clamp(newGravity.z, -5f, 5f);

            dynamicBone.m_Gravity = newGravity;
            //Gravity Safety - End

            //Force Safety - Start
            Vector3 newForce = dynamicBone.m_Force;

            newForce.x = Clamp(newForce.x, -5f, 5f);
            newForce.y = Clamp(newForce.y, -5f, 5f);
            newForce.z = Clamp(newForce.z, -5f, 5f);

            dynamicBone.m_Force = newForce;
            //Force Safety - End

            //Colliders Safety - Start
            Il2CppSystem.Collections.Generic.List<DynamicBoneCollider> dynamicBones = new Il2CppSystem.Collections.Generic.List<DynamicBoneCollider>();

            foreach (DynamicBoneCollider collider in dynamicBone.m_Colliders.ToArray())
            {
                if (collider != null && dynamicBones.Contains(collider) == false)
                {
                    dynamicBones.Add(collider);
                }
            }

            dynamicBone.m_Colliders = dynamicBones;
            //Colliders Safety - End

            return new AntiCrashDynamicBonePostProcess
            {
                nukedDynamicBones = currentNukedDynamicBones,

                dynamicBoneCount = currentDynamicBones
            };
        }

        internal static AntiCrashDynamicBoneColliderPostProcess ProcessDynamicBoneCollider(DynamicBoneCollider dynamicBoneCollider, int currentNukedDynamicBoneColliders, int currentDynamicBoneColliders)
        {
            if (currentDynamicBoneColliders >= MaxDynamicBoneColliders)
            {
                currentNukedDynamicBoneColliders++;

                Object.DestroyImmediate(dynamicBoneCollider, true);

                return new AntiCrashDynamicBoneColliderPostProcess
                {
                    nukedDynamicBoneColliders = currentNukedDynamicBoneColliders,

                    dynamicBoneColiderCount = currentDynamicBoneColliders
                };
            }

            currentDynamicBoneColliders++;

            //Safety
            dynamicBoneCollider.m_Radius = Clamp(dynamicBoneCollider.m_Radius, 0f, 50f);
            dynamicBoneCollider.m_Height = Clamp(dynamicBoneCollider.m_Height, 0f, 50f);

            //Center Safety - Start
            Vector3 newCenter = dynamicBoneCollider.m_Center;

            Clamp(newCenter.x, -50f, 50f);
            Clamp(newCenter.y, -50f, 50f);
            Clamp(newCenter.z, -50f, 50f);

            dynamicBoneCollider.m_Center = newCenter;
            //Center Safety - End

            return new AntiCrashDynamicBoneColliderPostProcess
            {
                nukedDynamicBoneColliders = currentNukedDynamicBoneColliders,

                dynamicBoneColiderCount = currentDynamicBoneColliders
            };
        }

        internal static AntiCrashLightSourcePostProcess ProcessLight(Light light, int currentNukedLights, int currentLights)
        {
            if (currentLights >= MaxLightSources)
            {
                currentNukedLights++;

                Object.DestroyImmediate(light, true);

                return new AntiCrashLightSourcePostProcess
                {
                    nukedLightSources = currentNukedLights,

                    lightSourceCount = currentLights
                };
            }

            currentLights++;

            return new AntiCrashLightSourcePostProcess
            {
                nukedLightSources = currentNukedLights,

                lightSourceCount = currentLights
            };
        }

        internal static int ProcessTransform(Transform transform, int limitedTransforms)
        {
            bool limitedTransform = false;

            //Rotation Safety
            Quaternion newLocalRotation = transform.localRotation;

            if (IsInvalid(newLocalRotation) == true)
            {
                //MelonLogger.Msg("Rotation was invalid - lets fix it up");

                newLocalRotation = Quaternion.identity;

                limitedTransform = true;
            }
            else
            {
                newLocalRotation.x = Clamp(newLocalRotation.x, 3.0f, 3.0f);
                newLocalRotation.y = Clamp(newLocalRotation.y, 3.0f, 3.0f);
                newLocalRotation.z = Clamp(newLocalRotation.z, 3.0f, 3.0f);
                newLocalRotation.w = Clamp(newLocalRotation.w, 3.0f, 3.0f);

                if (newLocalRotation != transform.localRotation)
                {
                    //MelonLogger.Msg("Rotation was clamped");

                    limitedTransform = true;
                }
            }

            transform.localRotation = newLocalRotation;

            //Scale Safety
            Vector3 newLocalScale = transform.localScale;

            if (IsInvalid(newLocalScale) == true)
            {
                //MelonLogger.Msg("Scale was invalid - lets fix it up");

                newLocalScale = new Vector3(1f, 1f, 1f);

                limitedTransform = true;
            }
            else
            {
                newLocalScale.x = Clamp(newLocalScale.x, 3.0f, 3.0f);
                newLocalScale.y = Clamp(newLocalScale.y, 3.0f, 3.0f);
                newLocalScale.z = Clamp(newLocalScale.z, 3.0f, 3.0f);

                if (newLocalScale != transform.localScale)
                {
                    //MelonLogger.Msg("Scale was clamped");

                    limitedTransform = true;
                }
            }

            transform.localScale = newLocalScale;

            return limitedTransform ? ++limitedTransforms : limitedTransforms;
        }

        internal static void ProcessRigidbody(Rigidbody rigidbody)
        {
            rigidbody.mass = Clamp(rigidbody.mass, -10000f, 10000f);
            rigidbody.maxAngularVelocity = Clamp(rigidbody.maxAngularVelocity, -100f, 100f);
            rigidbody.maxDepenetrationVelocity = Clamp(rigidbody.maxDepenetrationVelocity, -100f, 100f);
        }

        internal static bool ProcessCollider(Collider collider)
        {
            if ((collider.bounds.center.x < 100f && collider.bounds.center.x > 100f) ||
                (collider.bounds.center.y < 100f && collider.bounds.center.y > 100f) ||
                (collider.bounds.center.z < 100f && collider.bounds.center.z > 100f) ||
                (collider.bounds.extents.x < 100f && collider.bounds.extents.x > 100f) ||
                (collider.bounds.extents.y < 100f && collider.bounds.extents.y > 100f) ||
                (collider.bounds.extents.z < 100f && collider.bounds.extents.z > 100f))
            {
                Object.DestroyImmediate(collider, true);

                return true;
            }

            if (collider is BoxCollider boxCollider)
            {
                //Center Safety
                Vector3 newCenter = boxCollider.center;

                newCenter.x = Clamp(newCenter.x, -100f, 100f);
                newCenter.y = Clamp(newCenter.y, -100f, 100f);
                newCenter.y = Clamp(newCenter.y, -100f, 100f);

                boxCollider.center = newCenter;

                //Extents Safety
                Vector3 newExtents = boxCollider.extents;

                newExtents.x = Clamp(newExtents.x, -100f, 100f);
                newExtents.y = Clamp(newExtents.y, -100f, 100f);
                newExtents.y = Clamp(newExtents.y, -100f, 100f);

                boxCollider.extents = newExtents;

                //Size Safety
                Vector3 newSize = boxCollider.size;

                newSize.x = Clamp(newSize.x, -100f, 100f);
                newSize.y = Clamp(newSize.y, -100f, 100f);
                newSize.y = Clamp(newSize.y, -100f, 100f);

                boxCollider.size = newSize;
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                capsuleCollider.radius = Clamp(capsuleCollider.radius, -25f, 25f);
                capsuleCollider.height = Clamp(capsuleCollider.height, -25f, 25f);

                //Center Safety
                Vector3 newCenter = capsuleCollider.center;

                newCenter.x = Clamp(newCenter.x, -100f, 100f);
                newCenter.y = Clamp(newCenter.y, -100f, 100f);
                newCenter.y = Clamp(newCenter.y, -100f, 100f);

                capsuleCollider.center = newCenter;
            }
            else if (collider is SphereCollider sphereCollider)
            {
                sphereCollider.radius = Clamp(sphereCollider.radius, -25f, 25f);

                //Center Safety
                Vector3 newCenter = sphereCollider.center;

                newCenter.x = Clamp(newCenter.x, -100f, 100f);
                newCenter.y = Clamp(newCenter.y, -100f, 100f);
                newCenter.y = Clamp(newCenter.y, -100f, 100f);

                sphereCollider.center = newCenter;
            }

            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static short Clamp3(short value, short min, short max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static byte Clamp(byte value, byte min, byte max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsBeyondLimit(Vector3 vector, float lowerLimit, float higherLimit)
        {
            if (vector.x < lowerLimit || vector.x > higherLimit)
            {
                return true;
            }

            if (vector.y < lowerLimit || vector.y > higherLimit)
            {
                return true;
            }

            if (vector.z < lowerLimit || vector.z > higherLimit)
            {
                return true;
            }

            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsInvalid(Vector3 vector)
        {
            return float.IsNaN(vector.x) || float.IsInfinity(vector.x) ||
                   float.IsNaN(vector.y) || float.IsInfinity(vector.y) ||
                   float.IsNaN(vector.z) || float.IsInfinity(vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsInvalid(Quaternion quaternion)
        {
            return float.IsNaN(quaternion.x) || float.IsInfinity(quaternion.x) ||
                   float.IsNaN(quaternion.y) || float.IsInfinity(quaternion.y) ||
                   float.IsNaN(quaternion.z) || float.IsInfinity(quaternion.z) ||
                   float.IsNaN(quaternion.w) || float.IsInfinity(quaternion.w);
        }
        internal static List<T> FindAllComponentsInGameObject<T>(GameObject gameObject, bool includeInactive = true, bool searchParent = true, bool searchChildren = true) where T : class
        {
            List<T> components = new List<T>();

            foreach (T component in gameObject.GetComponents<T>())
            {
                components.Add(component);
            }

            if (searchParent == true)
            {
                foreach (T component in gameObject.GetComponentsInParent<T>(includeInactive))
                {
                    components.Add(component);
                }
            }

            if (searchChildren == true)
            {
                foreach (T component in gameObject.GetComponentsInChildren<T>(includeInactive))
                {
                    components.Add(component);
                }
            }

            return components;
        }

        internal static bool ProcessShader(Material material)
        {
            string Shader = material.shader.name.ToLower();
            bool CheckSupport = !material.shader.isSupported;
            bool result;
            if (CheckSupport)
            {
                SanitizeShader(material);
                result = true;
            }
            else
            {
                bool checkEngine = IsFakeEngineShader(material);
                if (checkEngine)
                {
                    SanitizeShader(material);
                    result = true;
                }
                else
                {
                    bool AreYouMatching = isNewPoyomiShader.IsMatch(Shader);
                    if (AreYouMatching)
                    {
                        result = false;
                    }
                    else
                    {
                        bool BlackList = Trinity.SDK.Config.shaderList.Contains(Shader);
                        if (BlackList)
                        {
                            SanitizeShader(material);
                            result = true;
                        }
                        else
                        {
                            int ShaderCount = (Encoding.UTF8.GetByteCount(Shader) - Shader.Length) / 4;
                            bool BadShader = string.IsNullOrEmpty(Shader) || Shader.Length > 100 || material.shader.renderQueue > maximumRenderQueue || ShaderCount > 10 || numberRegex.IsMatch(Shader);
                            if (BadShader)
                            {
                                SanitizeShader(material);
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            return result;
        }
        internal static int ProcessMesh(Mesh mesh, int subMeshCount, ref int currentNukedMeshes, ref uint currentPolygonCount)
        {
            int Count = -1;
            for (int i = 0; i < subMeshCount; i++)
            {
                try
                {
                    uint IndexCount = mesh.GetIndexCount(i);
                    switch (mesh.GetTopology(i))
                    {
                        case MeshTopology.Triangles:
                            IndexCount /= 3U;
                            break;
                        case MeshTopology.Quads:
                            IndexCount /= 4U;
                            break;
                        case MeshTopology.Lines:
                            IndexCount /= 2U;
                            break;
                    }
                    bool checkmax = currentPolygonCount + IndexCount > MaxPolygons;
                    if (checkmax)
                    {
                        currentPolygonCount += IndexCount;
                        currentNukedMeshes++;
                        bool Check = Count == -1;
                        if (Check)
                        {
                            Count = i;
                        }
                        Object.DestroyImmediate(mesh, true);
                        break;
                    }
                    currentPolygonCount += IndexCount;
                }
                catch (System.Exception e)
                {
                    Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red,$"[ERROR] \nClass: AntiCrash\nHandle: SubMesh Processor\nError: {e}",false,false);
                }
            }
            bool nullcheck = mesh == null;
            int result;
            if (nullcheck)
            {
                result = Count;
            }
            else
            {
                bool extents = IsBeyondLimit(mesh.bounds.extents, -100f, 100f);
                if (extents)
                {
                    Object.DestroyImmediate(mesh, true);
                    result = Count;
                }
                else
                {
                    bool size = IsBeyondLimit(mesh.bounds.size, -100f, 100f);
                    if (size)
                    {
                        Object.DestroyImmediate(mesh, true);
                        result = Count;
                    }
                    else
                    {
                        bool center = IsBeyondLimit(mesh.bounds.center, -100f, 100f);
                        if (center)
                        {
                            Object.DestroyImmediate(mesh, true);
                            result = Count;
                        }
                        else
                        {
                            bool min = IsBeyondLimit(mesh.bounds.min, -100f, 100f);
                            if (min)
                            {
                                Object.DestroyImmediate(mesh, true);
                                result = Count;
                            }
                            else
                            {
                                bool max = IsBeyondLimit(mesh.bounds.max, -100f, 100f);
                                if (max)
                                {
                                    Object.DestroyImmediate(mesh, true);
                                    result = Count;
                                }
                                else
                                {
                                    result = Count;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static bool AntiBlendShapeCrash = false;
        private static readonly Regex numberRegex = new Regex("^\\d{5,20}");
        internal static void SanitizeShader(Material material) => material.shader = GetStandardShader();
        private static readonly int maximumRenderQueue = 85899;
        private static readonly List<string> engineShaders = new List<string> { "shader", "diffuse", "particle", "transparent/diffuse", "unlit/texture" };
        internal static bool IsFakeEngineShader(Material material)
        {
            for (int i = 0; i < engineShaders.Count; i++)
            {
                bool CheckShaderName = material.shader.name == engineShaders[i] && material.shaderKeywords.Length == 0;
                if (CheckShaderName)
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly Regex duplicatedMeshNameRegex = new Regex("[a-zA-Z0-9]+(\\s|\\.\\d+)+(\\(\\d+\\)|\\d+|\\.\\d+)");
        private static readonly Regex isNewPoyomiShader = new Regex("hidden\\/(locked\\/|)(\\.|)poiyomi\\/(\\s|•|?|\\?|)+poiyomi (pro|toon|cutout|transparent)(\\s|•|?|\\?|)+\\/[a-z0-9\\s\\.\\d-_\\!\\@\\#\\$\\%\\^\\&\\*\\(\\)=\\]\\[]+");
        internal static Shader GetStandardShader()
        {
            bool nullcheck = standardShader == null;
            if (nullcheck)
                standardShader = Shader.Find("Standard");
            return standardShader;
        }
        private static readonly Il2CppSystem.Collections.Generic.List<Material> antiCrashTempMaterialsList = new Il2CppSystem.Collections.Generic.List<Material>();
        internal static Shader GetDiffuseShader()
        {
            bool nullcheck = diffuseShader == null;
            if (nullcheck)
                diffuseShader = Shader.Find("Diffuse");
            return diffuseShader;
        }
        private static Shader standardShader;
        private static Shader diffuseShader;
    }
}
