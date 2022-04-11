using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using VRC.Core;
using static Area51.NetworkingS;

namespace Area51
{
    internal class Astronyia_Patches
    {
        internal static List<Astronyia_Patches> Created_Patches = new List<Astronyia_Patches>();
        internal string Id { get; set; }
        internal MethodBase basis { get; set; }
        internal MethodInfo PatchingMethod { get; set; }
        internal PatchType type { get; set; }
        internal enum PatchType
        {
            prefix = 0,
            postfix = 1,
            transpiler = 2,
            ilmanipulator = 3,
            finalizer = 4
        }
        internal Astronyia_Patches(MethodBase basis, string PatchingMethod, PatchType type, string id = null)
        {
            this.basis = basis;
            this.PatchingMethod = typeof(Patched_Methods).GetMethod(PatchingMethod);
            this.type = type;
            this.Id = PatchingMethod;
            if (id != null)
            {
                this.Id = id;
            }
            Created_Patches.Add(this);
        }
        internal static async Task<Exception> UnPatchSingle(string ID)
        {
            foreach (Astronyia_Patches a in Created_Patches)
            {
                if (a.Id == ID)
                {
                    return await UnPatchSingle(a);
                }
            }
            return new Exception($"Coudnt find a Patch which Matches to the following ID: {ID}");
        }
        internal static async Task<Exception> UnPatchSingle(Astronyia_Patches Patch)
        {
            if (Patch.Id == null) return new Exception("Patching ID is null!");
            if (Patch.basis == null) return new Exception("Patching Basis is null!");
            if (Patch.PatchingMethod == null) return new Exception("PatchingMethod is null!");
            //if (Patch.type == null) return new Exception("Patching type is null!");
            try
            {
                new HarmonyLib.Harmony(Patch.Id).Unpatch(Patch.basis, Patch.PatchingMethod);
            }
            catch (Exception e)
            {
                return e;
            }
            return new Exception("Patch type doesnt match to Any PatchType");
        }
        internal static async Task<Dictionary<Astronyia_Patches, Exception>> UnPatchAll(Action<Astronyia_Patches, Exception> Action_on_Patch_Fail = null, Action<Astronyia_Patches, Exception> Action_on_Patch_Success = null)
        {
            var result = new Dictionary<Astronyia_Patches, Exception>();
            foreach (Astronyia_Patches Patch in Created_Patches)
            {
                var ex = await UnPatchSingle(Patch);
                result.Add(Patch, ex);
                if (ex == null)
                {
                    if (Action_on_Patch_Success != null)
                    {
                        try
                        {
                            Action_on_Patch_Success.Invoke(Patch, ex);
                        }
                        catch { }
                    }
                }
                if (ex != null)
                {
                    if (Action_on_Patch_Fail != null)
                    {
                        try
                        {
                            Action_on_Patch_Fail.Invoke(Patch, ex);
                        }
                        catch { }
                    }
                }
            }
            return result;
        }
        internal static Exception PatchSingle(Astronyia_Patches Patch)
        {
            if (Patch.Id == null) return new Exception("Patching ID is null!");
            if (Patch.basis == null) return new Exception("Patching Basis is null!");
            if (Patch.PatchingMethod == null) return new Exception("PatchingMethod is null!");
       //     if (Patch.type == null) return new Exception("Patching type is null!");
            switch (Patch.type)
            {
                case PatchType.prefix:
                    {
                        try
                        {
                            var result = new HarmonyLib.Harmony(Patch.Id).Patch(Patch.basis, prefix: new HarmonyMethod(Patch.PatchingMethod));
                            if (result == null) return new Exception("Patch is Null!");
                            return null;
                        }
                        catch (Exception e)
                        {
                            return e;
                        }
                    }
                case PatchType.postfix:
                    {
                        try
                        {
                            var result = new HarmonyLib.Harmony(Patch.Id).Patch(Patch.basis, postfix: new HarmonyMethod(Patch.PatchingMethod));
                            if (result == null) return new Exception("Patch is Null!");
                            return null;
                        }
                        catch (Exception e)
                        {
                            return e;
                        }
                    }
                case PatchType.transpiler:
                    {
                        try
                        {
                            var result = new HarmonyLib.Harmony(Patch.Id).Patch(Patch.basis, transpiler: new HarmonyMethod(Patch.PatchingMethod));
                            if (result == null) return new Exception("Patch is Null!");
                            return null;
                        }
                        catch (Exception e)
                        {
                            return e;
                        }
                    }
                case PatchType.finalizer:
                    {
                        try
                        {
                            var result = new HarmonyLib.Harmony(Patch.Id).Patch(Patch.basis, finalizer: new HarmonyMethod(Patch.PatchingMethod));
                            if (result == null) return new Exception("Patch is Null!");
                            return null;
                        }
                        catch (Exception e)
                        {
                            return e;
                        }
                    }
                case PatchType.ilmanipulator:
                    {
                        try
                        {
                            var result = new HarmonyLib.Harmony(Patch.Id).Patch(Patch.basis, ilmanipulator: new HarmonyMethod(Patch.PatchingMethod));
                            if (result == null) return new Exception("Patch is Null!");
                            return null;
                        }
                        catch (Exception e)
                        {
                            return e;
                        }
                    }
            }
            return new Exception("Patch type doesnt match to Any PatchType");
        }
        internal static async Task<Dictionary<Astronyia_Patches, Exception>> PatchAll(Action<Astronyia_Patches, Exception> Action_on_Patch_Fail = null, Action<Astronyia_Patches, Exception> Action_on_Patch_Success = null)
        {
            var result = new Dictionary<Astronyia_Patches, Exception>();
            foreach (Astronyia_Patches Patch in Created_Patches)
            {
                var ex = PatchSingle(Patch);
                result.Add(Patch, ex);
                if (ex == null)
                {
                    if (Action_on_Patch_Success != null)
                    {
                        try
                        {
                            Action_on_Patch_Success.Invoke(Patch, ex);
                        }
                        catch { }
                    }
                }
                if (ex != null)
                {
                    if (Action_on_Patch_Fail != null)
                    {
                        try
                        {
                            Action_on_Patch_Fail.Invoke(Patch, ex);
                        }
                        catch { }
                    }
                }
            }
            return result;
        }
    }
}
