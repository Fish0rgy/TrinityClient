using Area51.SDK;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace Area51.Module.Settings.Preformance
{
    class HideChairs : BaseModule
    {
        public HideChairs() : base("Hide Chairs", "", Main.Instance.SettingsButtonpreformance, null, true, true)
        {
        }

        public override void OnEnable()
        {
            SetAllObjectsOfTypeChairs(false);
            Logg.LogDebug($"Chairs Hidden");
        }

        public override void OnDisable()
        {
            SetAllObjectsOfTypeChairs(true);
            Logg.LogDebug($"Chairs UnHidden");
        }
        internal static void SetAllObjectsOfTypeChairs(bool state)
        {
            Il2CppArrayBase<VRCStation> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRCStation>();
            for (int i = 0; i < il2CppArrayBase.Count; i++)
            {
                VRCStation vrcstation = il2CppArrayBase[i];
                bool station = !(vrcstation == null) && vrcstation.gameObject.active == !state;
                if (station)
                {
                    vrcstation.gameObject.SetActive(state);
                }
            }
        }
    }
}
