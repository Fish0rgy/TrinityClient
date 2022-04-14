using Trinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Area51.SDK.ButtonAPI
{
    class QMPanel
    {
        public GameObject panel = null;
        public QMPanel(string Contents)
        {
            //panel = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMNotificationsArea/DebugInfoPanel").gameObject, Main.Instance.quickMenuStuff.quickMenu.transform.Find("/Container/Window/QMNotificationsArea"));
            //panel.name = "Panel_" + Contents;
            //panel.SetActive(true);
            //panel.GetComponentInChildren<TextBinding>().field_Public_String_0 = Contents;
        }
    }
    //Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/DebugInfoPanel/
}
