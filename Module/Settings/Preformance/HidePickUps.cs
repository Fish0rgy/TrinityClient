using Trinity.SDK;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Trinity.Module.Settings.Preformance
{
    class HidePickUps : BaseModule
    {
        public HidePickUps() : base("Hide Items", "Hides pick ups local", Main.Instance.SettingsButtonpreformance, null, true, false)
        {
        }

        public override void OnEnable()
        {
            pickupHIDE(false);
            LogHandler.LogDebug($"Pickups Hidden");
        }

        public override void OnDisable()
        {
            pickupHIDE(true);
            LogHandler.LogDebug($"Pickups UnHidden");
        }
        internal static void pickupHIDE(bool a)
        {
            VRC_Pickup[] D = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
            for (int i = 0; i < D.Length; i++)
            {
                bool L = D[i].gameObject.layer == 13;
                if (L)
                {
                    D[i].gameObject.SetActive(a);
                }
            }
            VRC_Pickup[] Y = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
            for (int j = 0; j < Y.Length; j++)
            {
                bool E = Y[j].gameObject.layer == 13;
                if (E)
                {
                    Y[j].gameObject.SetActive(a);
                }
            }
            VRCPickup[] C = Resources.FindObjectsOfTypeAll<VRCPickup>().ToArray<VRCPickup>();
            for (int k = 0; k < C.Length; k++)
            {
                bool G = C[k].gameObject.layer == 13;
                if (G)
                {
                    C[k].gameObject.SetActive(a);
                }
            }
        }
    }
}
