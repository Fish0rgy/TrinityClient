using Trinity.Utilities;
using Trinity.SDK;
using UnityEngine;

namespace Trinity.Module.Player
{
    class FPSUnlocker : BaseModule
    {
        public FPSUnlocker() : base("FPS Unlocker", "140 Fps", Main.Instance.SettingsButtonpreformance, null, true, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("FPS: <color=green>FPS Cap Unlocked</color>");
            LogHandler.Log(LogHandler.Colors.Green, "Application Framerate Set To 140", false, false);
            Application.targetFrameRate = 140;
        }

        public override void OnDisable()
        {
            MenuUI.Log("FPS: <color=red>FPS Cap Reset</color>");
            LogHandler.Log(LogHandler.Colors.Green, "Application Framerate Set To 90", false, false);
            Application.targetFrameRate = 90;
        }
    }
}
