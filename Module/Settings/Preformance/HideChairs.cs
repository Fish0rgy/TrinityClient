using Trinity.Utilities;
using Trinity.SDK;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace Trinity.Module.Settings.Preformance
{
    class HideChairs : BaseModule
    {
        public HideChairs() : base("Hide Chairs", "Hides Chairs local", Main.Instance.SettingsButtonpreformance, null, true, false)
        {
        }

        public override void OnEnable()
        {
            SetAllObjectsOfTypeChairs(false);
            LogHandler.LogDebug($"Chairs Hidden");
        }

        public override void OnDisable()
        {
            SetAllObjectsOfTypeChairs(true);
            LogHandler.LogDebug($"Chairs UnHidden");
        }
        private void SetAllObjectsOfTypeChairs(bool state)
        {
            Il2CppArrayBase<VRC_StationInternal> TriggerStations = Resources.FindObjectsOfTypeAll<VRC_StationInternal>();
            for (int i = 0; i < TriggerStations.Count; i++)
            {
                VRC_StationInternal Chairs = TriggerStations[i];
                bool station = !(Chairs == null) && Chairs.gameObject.active == !state;
                if (!station) return;
                Chairs.gameObject.SetActive(state);
            }
        }
    }
}
