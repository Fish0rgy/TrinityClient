using Trinity.SDK.PatchAPI.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.Events
{
    public interface OnObjectInstantiatedEvent
    {
        bool OnObjectInstantiatedPatch(IntPtr assetPtr, Vector3 pos, Quaternion rot, byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer, _AssetManagement.ObjectInstantiateDelegate originalInstantiateDelegate);
    }
}
 