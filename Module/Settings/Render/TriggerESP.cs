using Trinity.SDK;
using Trinity.Utilities;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace Trinity.Module.Settings.Render
{
    class TriggerESP : BaseModule
    {
        public TriggerESP() : base("Trigger ESP", "World Trigger ESP", Main.Instance.SettingsButtonrender, null, true, true) { }

        public override void OnEnable()
        {
            MenuUI.Log("TRIGGERS: <color=green>Trigger ESP On</color>");
            triggeresp(true);
        }

        public override void OnDisable()
        {
            MenuUI.Log("TRIGGERS: <color=red>Trigger ESP Off</color>");
            triggeresp(false);
        }

        internal static void triggeresp(bool state)
        {
            Il2CppArrayBase<VRC_Trigger> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRC_Trigger>();
            foreach (VRC_Trigger vrc_Trigger in il2CppArrayBase)
            {
                bool TriggerObject = !(vrc_Trigger == null) && !(vrc_Trigger.gameObject == null) && vrc_Trigger.gameObject.active && !vrc_Trigger.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                if (TriggerObject)
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(vrc_Trigger.GetComponentInChildren<MeshRenderer>(), state);
                }
            }
        }
    }
}
