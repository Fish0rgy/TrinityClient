using System.Drawing;
using TMPro;
using UnityEngine;

namespace Area51.SDK.ButtonAPI
{
    public class QMTEST
    {
        public QMTEST(Transform parent)
        {
            GameObject Spacer = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);
            Spacer.transform.parent = parent;
            Spacer.name = "_Spacer";
            Spacer.transform.Find("Text_H4").gameObject.active = false;
            Spacer.transform.Find("Background").gameObject.active = false;
            Spacer.transform.Find("Badge_MMJump").gameObject.active = false;
            Spacer.transform.Find("Icon").gameObject.active = false;
            Spacer.SetActive(true);
        }
    }
}
