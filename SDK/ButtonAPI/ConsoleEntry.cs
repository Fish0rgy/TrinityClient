using TMPro;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Trinity.SDK.ButtonAPI
{
    public class ConsoleEntry
    {
        public GameObject mainObj;
        public TextMeshProUGUI txtCom;
         
        public ConsoleEntry(string txt)
        {
            Init(txt);
        }

        private void Init(string txt)
        {
            mainObj = new($"NewEntry");
            txtCom = mainObj.AddComponent<TextMeshProUGUI>();
            txtCom.fontSize = 26;
            txtCom.text = txt;
            txtCom.autoSizeTextContainer = false;
            mainObj.GetComponent<RectTransform>().sizeDelta = new(890, 32.5f);
        }

    }
}
