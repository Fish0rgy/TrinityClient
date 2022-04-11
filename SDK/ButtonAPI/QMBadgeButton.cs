using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Area51.SDK.ButtonAPI
{
    public class QMBadgeButton
    {
        public QMBadgeButton(Transform parent, int x, int y )
        {
            GameObject BadgeButton = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Background").gameObject, parent);          
            BadgeButton.transform.localPosition = new Vector3(x, y, -1);
            BadgeButton.name = "_Area51_ConsoleBackground";
            BadgeButton.transform.parent = parent;
        }
    }
}
