 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;

namespace Area51.Module.Safety.Avatar
{
    //thanks killer Munchen Server: https://discord.gg/DysbzmWTcV
    internal class AntiCrashRendererPostProcess
    {
        internal int nukedMeshes;
        internal int polygonCount;

        internal int nukedMaterials;
        internal int materialCount;

        internal int nukedShaders;
        internal int shaderCount;
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
        public static string[] BlackListedShaders = File.ReadAllLines("Area51/BlackList/Avatar/Shader.txt");
        public static int maxAudio = 150;
        public static int maxLight = 8;
        public static int maxMaterials = 300;
        public static int maxMesh = 250;
        public static int maxDynamicBonesCollider = 50;
        public static int maxDynamicBones = 75;
        public static int maxPoly = 2500000; 
        public static int maxCloth = 75;
        public static int maxClothVertices = 15000;
        public static int maxColliders = 20;
        public static int maxParticleLimit = 10000;
        public static int maxParticleMeshVertices = 1000000;
        public static int maxParticleTrails = 64;
        public static int maxParticleCollisonShapes = 1024;
        public static int maxParticleEmissionRate = 500;
        public static int maxParticleSystems = 1;
        public static int maxAnimators = 25;
        public static int maxRegidBodies = 25; 
        public static Shader defaultShader;
        internal static void ProcessRenderer(Renderer renderer, bool limitPolygons, bool limitMaterials, bool limitShaders, ref AntiCrashRendererPostProcess previousProcess)
        {
            if (limitPolygons == true)
            {
                ProcessMeshPolygons(renderer, ref previousProcess.nukedMeshes, ref previousProcess.polygonCount);
            }

            if (limitMaterials == true)
            {
                AntiCrashMaterialPostProcess postReport = ProcessMaterials(renderer, previousProcess.nukedMaterials, previousProcess.materialCount);

                previousProcess.nukedMaterials = postReport.nukedMaterials;
                previousProcess.materialCount = postReport.materialCount;
            }

            if (limitShaders == true)
            {
                AntiCrashShaderPostProcess postReport = ProcessShaders(renderer, previousProcess.nukedShaders, previousProcess.shaderCount);

                previousProcess.nukedShaders = postReport.nukedShaders;
                previousProcess.shaderCount = postReport.shaderCount;
            }
        }

        internal static void ProcessMeshPolygons(Renderer renderer, ref int currentNukedMeshes, ref int currentPolygonCount)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = renderer.TryCast<SkinnedMeshRenderer>();
            MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();

            Mesh mesh = skinnedMeshRenderer?.sharedMesh ?? meshFilter?.sharedMesh;

            if (mesh == null)
            {
                return;
            }

            if (mesh.isReadable == false)
            {
                currentNukedMeshes++;

                Object.DestroyImmediate(mesh, true);

                return;
            }

            if (mesh.name.ToLower().Equals("body"))
            {
                bool hasRevertedBlendShape = false;
                bool hasPoseToRestBlendShape = false;
                bool hasKey22BlendShape = false;
                bool hasKey56BlendShape = false;
                bool hasSlantBlendShape = false;

                for (int i = 0; i < mesh.blendShapeCount; i++)
                {
                    string blendShapeName = mesh.GetBlendShapeName(i).ToLower();

                    if (blendShapeName.Contains("reverted") == true)
                    {
                        hasRevertedBlendShape = true;
                    }

                    if (blendShapeName.Contains("posetorest") == true)
                    {
                        hasPoseToRestBlendShape = true;
                    }
                    else if (blendShapeName.Contains("key 22") == true)
                    {
                        hasKey22BlendShape = true;
                    }
                    else if (blendShapeName.Contains("key 56") == true)
                    {
                        hasKey56BlendShape = true;
                    }
                    else if (blendShapeName.Contains("slant") == true)
                    {
                        hasSlantBlendShape = true;
                    }
                }

                //"Cyber Popular" avatar detected
                if (hasRevertedBlendShape == true && hasPoseToRestBlendShape == true && hasKey22BlendShape == true && hasKey56BlendShape == true && hasSlantBlendShape == true)
                {
                    //This is the crashing BlendShape key
                    /*int chosenBlendShapeKey = 7;
                    ConsoleUtils.Info("AntiCrash", $"BlendShapes: {mesh.blendShapeCount}");
                    ConsoleUtils.Info("AntiCrash", $"Chosen BlendShape: {mesh.GetBlendShapeName(chosenBlendShapeKey)}");
                    ConsoleUtils.Info("AntiCrash", $"VertexCount: {mesh.vertexCount}");
                    Vector3[] verticies = new Vector3[mesh.vertexCount];
                    Vector3[] normals = new Vector3[mesh.vertexCount];
                    Vector3[] tangents = new Vector3[mesh.vertexCount];
                    MelonLoader.MelonLogger.Msg("1");
                    mesh.GetBlendShapeFrameVertices(chosenBlendShapeKey, 0, verticies, normals, tangents);
                    MelonLoader.MelonLogger.Msg("2");
                    for (int i = 0; i < mesh.vertexCount; i++)
                    {
                        ConsoleUtils.Info("Frame", $"Index: {i} | Vertice: {verticies[i].ToString()} | Normal: {normals[i].ToString()} | Tangent: {tangents[i].ToString()}");
                    }*/

                    mesh.ClearBlendShapes();
                }
            }

            ProcessMesh(mesh, ref currentNukedMeshes, ref currentPolygonCount);
        }

        internal static AntiCrashMaterialPostProcess ProcessMaterials(Renderer renderer, int currentNukedMaterials, int currentMaterialCount)
        {
            int newMaterialCount = currentMaterialCount + renderer.GetMaterialCount();

            if (newMaterialCount > maxMaterials)
            {
                //Grab the current amount of materials
                Il2CppSystem.Collections.Generic.List<Material> materialList = new Il2CppSystem.Collections.Generic.List<Material>();
                renderer.GetSharedMaterials(materialList);

                //Calculate the amount of materials we need to remove
                int startRemoveIndex = (currentMaterialCount < maxMaterials) ? maxMaterials : 0;
                int removeMaterialCount = (startRemoveIndex == 0) ? materialList.Count : (newMaterialCount - maxMaterials);

                //Make sure we don't get out of bounds errors
                if (startRemoveIndex > materialList.Count)
                {
                    startRemoveIndex = materialList.Count;
                }

                //Make sure we don't get out of bounds errors
                int maxRemoveableCount = materialList.Count - startRemoveIndex;

                if (removeMaterialCount > maxRemoveableCount)
                {
                    removeMaterialCount = maxRemoveableCount;
                }

                //Set new values accordingly
                currentNukedMaterials += removeMaterialCount;
                newMaterialCount -= removeMaterialCount;

                //Remove all materials deemed unneccesary
                materialList.RemoveRange(startRemoveIndex, removeMaterialCount);
                renderer.materials = (Il2CppReferenceArray<Material>)materialList.ToArray();
            }

            currentMaterialCount = newMaterialCount;

            return new AntiCrashMaterialPostProcess
            {
                nukedMaterials = currentNukedMaterials,
                materialCount = currentMaterialCount
            };
        }

        internal static AntiCrashShaderPostProcess ProcessShaders(Renderer renderer, int currentNukedShaders, int currentShaderCount)
        {
            for (int j = 0; j < renderer.materials.Length; j++)
            {
                //Error Check
                if (renderer.materials[j] == null)
                {
                    continue;
                }

                currentShaderCount++;

                //Blacklist Check
                if (BlackListedShaders.Contains(renderer.materials[j].shader.name.ToLower()))
                {
                    renderer.materials[j].shader = defaultShader;

                    currentNukedShaders++;

                    continue;
                }

                //Engine Check
                if (renderer.materials[j].shader.isSupported == false)
                {
                    renderer.materials[j].shader = defaultShader;

                    currentNukedShaders++;

                    continue;
                }

                //Sanity Check
                switch (renderer.materials[j].shader.name)
                {
                    case "Standard":
                        {
                            renderer.materials[j].shader = defaultShader;

                            break;
                        }

                    case "Diffuse":
                        {
                            renderer.materials[j].shader = defaultShader;

                            break;
                        }
                }
            }

            return new AntiCrashShaderPostProcess
            {
                nukedShaders = currentNukedShaders,
                shaderCount = currentShaderCount
            };
        }

        internal static AntiCrashClothPostProcess ProcessCloth(Cloth cloth, int nukedCloths, int currentVertexCount)
        {
            cloth.clothSolverFrequency = Mathf.Max(cloth.clothSolverFrequency, 300f);

            Mesh skinnedMesh = cloth.GetComponent<SkinnedMeshRenderer>()?.sharedMesh;

            if (skinnedMesh == null)
            {
                nukedCloths++;

                Object.DestroyImmediate(cloth, true);

                return new AntiCrashClothPostProcess
                {
                    nukedCloths = nukedCloths,
                    currentVertexCount = currentVertexCount
                };
            }

            currentVertexCount += skinnedMesh.vertexCount;

            if (currentVertexCount >= maxClothVertices)
            {
                currentVertexCount -= skinnedMesh.vertexCount;

                nukedCloths++;

                Object.DestroyImmediate(cloth, true);
            }

            return new AntiCrashClothPostProcess
            {
                nukedCloths = nukedCloths,
                currentVertexCount = currentVertexCount
            };
        }

        internal static void ProcessParticleSystem(ParticleSystem particleSystem, ref AntiCrashParticleSystemPostProcess post)
        {
            ParticleSystemRenderer renderer = particleSystem.GetComponent<ParticleSystemRenderer>();

            if (renderer == null)
            {
                post.nukedParticleSystems++;

                Object.DestroyImmediate(particleSystem, true);

                return;
            }

            particleSystem.main.simulationSpeed = Clamp(particleSystem.main.simulationSpeed, 0f, 5.0f);
            particleSystem.collision.maxCollisionShapes = Clamp(particleSystem.collision.maxCollisionShapes, 0, maxParticleCollisonShapes);
            particleSystem.trails.ribbonCount = Clamp(particleSystem.trails.ribbonCount, 0, maxParticleTrails);

            for (int i = 0; i < particleSystem.emission.burstCount; i++)
            {
                ParticleSystem.Burst burst = particleSystem.emission.GetBurst(i);

                burst.maxCount = Clamp(burst.maxCount, (short)0, (short)125);
                burst.cycleCount = Clamp(burst.cycleCount, 0, 125);
            }

            int particleLimit = maxParticleLimit - post.currentParticleCount;

            if (particleSystem.maxParticles > particleLimit)
            {
                particleSystem.maxParticles = particleLimit;
            }

            post.currentParticleCount += particleSystem.maxParticles;

            if (renderer.renderMode == ParticleSystemRenderMode.Mesh)
            {
                Il2CppReferenceArray<Mesh> meshes = new Il2CppReferenceArray<Mesh>(renderer.meshCount);
                renderer.GetMeshes(meshes);

                int polySum = 0;
                int nukedMeshes = 0;

                foreach (Mesh mesh in meshes)
                {
                    ProcessMesh(mesh, ref nukedMeshes, ref polySum);
                }

                if ((polySum * particleSystem.maxParticles) > maxParticleMeshVertices)
                {
                    particleSystem.maxParticles = maxParticleMeshVertices / polySum;
                }
            }

            if (particleSystem.maxParticles == 0)
            {
                post.nukedParticleSystems++;

                Object.DestroyImmediate(renderer, true);
                Object.DestroyImmediate(particleSystem, true);
            }
        }

        internal static void ProcessMesh(Mesh mesh, ref int currentNukedMeshes, ref int currentPolygonCount)
        {
            int subMeshCount;

            try
            {
                subMeshCount = mesh.subMeshCount;
            }
            catch (System.Exception)
            {
                subMeshCount = 1;
            }

            for (int j = 0; j < subMeshCount; j++)
            {
                try
                {
                    uint polygonsInSubmesh = mesh.GetIndexCount(j);

                    switch (mesh.GetTopology(j))
                    {
                        case MeshTopology.Triangles:
                            {
                                polygonsInSubmesh /= 3;

                                break;
                            }

                        case MeshTopology.Quads:
                            {
                                polygonsInSubmesh /= 4;

                                break;
                            }

                        case MeshTopology.Lines:
                            {
                                polygonsInSubmesh /= 2;

                                break;
                            }
                    }

                    if ((currentPolygonCount + polygonsInSubmesh) > maxPoly)
                    {
                        currentNukedMeshes++;

                        Object.DestroyImmediate(mesh, true);

                        continue;
                    }

                    currentPolygonCount += (int)polygonsInSubmesh;
                }
                catch (System.Exception) { /* It's fine to get an exception here - we just want to be sure we don't skip any meshes */ }
            }

            //Sanity check in case we deleted the mesh in the previous stage
            if (mesh == null)
            {
                return;
            }

            //Mesh Safety
            if (IsBeyondLimit(mesh.bounds.extents, -100f, 100f) == true)
            {
                Object.DestroyImmediate(mesh, true);

                return;
            }

            if (IsBeyondLimit(mesh.bounds.size, -100f, 100f) == true)
            {
                Object.DestroyImmediate(mesh, true);

                return;
            }

            if (IsBeyondLimit(mesh.bounds.center, -100f, 100f) == true)
            {
                Object.DestroyImmediate(mesh, true);

                return;
            }

            if (IsBeyondLimit(mesh.bounds.min, -100f, 100f) == true)
            {
                Object.DestroyImmediate(mesh, true);

                return;
            }

            if (IsBeyondLimit(mesh.bounds.max, -100f, 100f) == true)
            {
                Object.DestroyImmediate(mesh, true);

                return;
            }

            return;
        }

        internal static AntiCrashDynamicBonePostProcess ProcessDynamicBone(DynamicBone dynamicBone, int currentNukedDynamicBones, int currentDynamicBones)
        {
            if (currentDynamicBones >= maxDynamicBones)
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
            if (currentDynamicBoneColliders >= maxDynamicBonesCollider)
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
            if (currentLights >= maxLight)
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
        internal static short Clamp(short value, short min, short max)
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
    }
}
