using Area51.SDK;
using UnityEngine;

namespace Area51.Module.Player
{
    class FPSUnlocker : BaseModule
    {
        public FPSUnlocker() : base("FPS Unlocker", "140 Fps", Main.Instance.SettingsButtonpreformance, null, true) { }

        public override void OnEnable()
        {
            Logg.Log(Logg.Colors.Green, "Application Framerate Set To 140", false, false);
            Application.targetFrameRate = 140;
        }

        public override void OnDisable()
        {
            Logg.Log(Logg.Colors.Green, "Application Framerate Set To 90", false, false);
            Application.targetFrameRate = 90;
        }
    }
}
