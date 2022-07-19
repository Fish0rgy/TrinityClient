using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using Trinity.SDK;
using UnityEngine;

namespace Trinity.Module.Settings.Preformance
{
    internal class CoolKeyBinds : BaseModule, OnUpdateEvent
    {
        public CoolKeyBinds() : base("Key Binds", "Enable Key Binds That F/A-18E Super Hornet#8450 made", Main.Instance.SettingsButtonpreformance, null, true, true)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("KEYBINDS: <color=green>Key Binds On</color>");
            LogHandler.LogDebug($"Key Binds On");
            LogHandler.Log(LogHandler.Colors.Yellow,$"[KEYBINDS]\nFly = Ctrl + F\nESP = Ctrl + E\nSpeed = Ctrl + T\nLoudMic = Ctrl + G\n",false,false);
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("KEYBINDS: <color=red>Key Binds Off</color>"); 
            LogHandler.LogDebug($"Key Binds Off");
            Main.Instance.OnUpdateEvents.Remove(this);
        }

        public void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            {
                Misc.EnableFunction(TargetClass.FlyModule);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
            {
                Misc.EnableFunction(TargetClass.EspModule);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
            {
                Misc.EnableFunction(TargetClass.SpeedModule);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
            {
                Misc.EnableFunction(TargetClass.LoudMicModule);
            }
        }
         
    }
     
}
