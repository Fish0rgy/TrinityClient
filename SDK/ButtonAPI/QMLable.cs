using TMPro;
using UnityEngine;

namespace Trinity.SDK.ButtonAPI
{
    internal class QMLable
    {
        public TextMeshProUGUI text;
        public GameObject lable;
        public QMLable(Transform menu, float x, float y, string contents)
        {
            
            lable = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
            lable.name = "Lable_" + contents;
            
            lable.transform.localPosition = new Vector3(x, y, 0);
            text = lable.GetComponentInChildren<TextMeshProUGUI>();
            text.text = contents;
            text.enableAutoSizing = true;
            text.color = Color.white;
            text.m_fontColor = Color.white;
            lable.gameObject.SetActive(false);
        }
    }
}
