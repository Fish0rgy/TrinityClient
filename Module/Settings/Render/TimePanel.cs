using Area51.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Area51.Module.Settings.Render
{
    class TimePanel : BaseModule
    {
        public static VrConsoleLog TimeLog;
        public TimePanel() : base("Time", "Shows Current Time", Main.Instance.SettingsButtonrender, null, true, false)
        {
        }

        public override void OnEnable()
        {
          //  TimeLog.panel.SetActive(true);
        }

        public override void OnDisable()
        {//
          //  TimeLog.panel.SetActive(false);
        }
        public override void OnUIInit()
        {
           // TimeLog = new QMPanel($"{DateTime.Now.ToString("hh:mm tt")}");
            base.OnUIInit();
        }
    }
}
