using Trinity.Utilities;
using TMPro;
using UnityEngine;

namespace Trinity.SDK.ButtonAPI
{
    internal class QMHeader
    {
        public QMHeader(Transform menu, string contents)
        {
            GameObject header = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
            header.transform.parent = menu;
            header.name = "Header_" + contents;
            header.SetActive(true);
            header.GetComponentInChildren<TextMeshProUGUI>().text = contents;
        }
    }
}
 