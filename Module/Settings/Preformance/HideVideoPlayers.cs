using Trinity.Utilities;
using Trinity.SDK;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDK3.Video.Components;
using VRCSDK2;

namespace Trinity.Module.Settings.Preformance
{
    class HideVideoPlayers : BaseModule
    {
        public HideVideoPlayers() : base("Hide VideoPlayers", "Hides Video Player local", Main.Instance.SettingsButtonpreformance, null, true, false)
        {
        }

        public override void OnEnable()
        {
            SetAllObjectsOfTypeVideoPlayers(false);
            LogHandler.LogDebug($"Video Players Hidden");
        }

        public override void OnDisable()
        {
            SetAllObjectsOfTypeVideoPlayers(true);
            LogHandler.LogDebug($"Video Players UnHidden");
        }
        internal static void SetAllObjectsOfTypeVideoPlayers(bool state)
        {
            Il2CppArrayBase<VRC_SyncVideoPlayer> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();
            for (int i = 0; i < il2CppArrayBase.Count; i++)
            {
                VRC_SyncVideoPlayer vrc_SyncVideoPlayer = il2CppArrayBase[i];
                bool sync = !(vrc_SyncVideoPlayer == null);
                if (sync)
                {
                    bool check = vrc_SyncVideoPlayer.gameObject.active == !state;
                    if (check)
                    {
                        vrc_SyncVideoPlayer.gameObject.SetActive(state);
                    }
                }
            }
            Il2CppArrayBase<VRCUnityVideoPlayer> il2CppArrayBase2 = Resources.FindObjectsOfTypeAll<VRCUnityVideoPlayer>();
            for (int j = 0; j < il2CppArrayBase2.Count; j++)
            {
                VRCUnityVideoPlayer vrcunityVideoPlayer = il2CppArrayBase2[j];
                bool sync2 = !(vrcunityVideoPlayer == null);
                if (sync2)
                {
                    bool check2 = vrcunityVideoPlayer.gameObject.active == !state;
                    if (check2)
                    {
                        vrcunityVideoPlayer.gameObject.SetActive(state);
                    }
                }
            }
        }
    }
}
