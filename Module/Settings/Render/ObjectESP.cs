using Trinity.Events;
using Trinity.SDK;
using Trinity.Utilities;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace Trinity.Module.Settings.Render
{
    class ObjectESP : BaseModule, OnUpdateEvent
    {
        public static bool check = false;
        public ObjectESP() : base("Item ESP", "Draws Items threw walls", Main.Instance.SettingsButtonrender, null, true, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("OBJECTS: <color=green>Item ESP On</color>");
            check = true;
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("OBJECTS: <color=red>Item ESP Off</color>"); 
            Main.Instance.OnUpdateEvents.Remove(this);
            Il2CppArrayBase<VRC_Pickup> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
            foreach (VRC_Pickup vrc_Pickup in il2CppArrayBase)
            {
                bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                if (Object)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Pickup.GetComponentInChildren<MeshRenderer>(), false);
                }
            }
        } 

        public void OnUpdate()
        { 
            Il2CppArrayBase<VRC_Pickup> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
            foreach (VRC_Pickup vrc_Pickup in il2CppArrayBase)
            {
                bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                if (Object)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Pickup.GetComponentInChildren<MeshRenderer>(), check);
                }
            }
        }
    }
}
