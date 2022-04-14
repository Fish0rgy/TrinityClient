using Trinity.Utilities;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace Trinity.Module.Settings.Render
{
    class ObjectESP : BaseModule
    {
        public ObjectESP() : base("Item ESP", "Draws Items threw walls", Main.Instance.SettingsButtonrender, null, true, false) { }

        public override void OnEnable()
        {
            PickupESP(true);
        }

        public override void OnDisable()
        {
            PickupESP(false);
        }
        internal static void PickupESP(bool state)
        {
            Il2CppArrayBase<VRC_Pickup> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
            foreach (VRC_Pickup vrc_Pickup in il2CppArrayBase)
            {
                bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                if (Object)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Pickup.GetComponentInChildren<MeshRenderer>(), state);
                }
            }
        }
    }
}
