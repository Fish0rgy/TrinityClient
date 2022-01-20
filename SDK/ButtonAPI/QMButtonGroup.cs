using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Area51.SDK.ButtonAPI
{
    public class QMButtonGroup
    {
        public GameObject buttonGroup;
        public int buttonamount = 0;
        public QMButtonGroup(Transform parent, string name)
        {
            buttonGroup = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.QuickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks").gameObject, parent);
            buttonGroup.name = "Buttons_" + name;
            buttonGroup.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
            for (int i = 0; i < buttonGroup.transform.childCount; i++)
                GameObject.Destroy(buttonGroup.transform.GetChild(i).gameObject);
        }
    }
}
