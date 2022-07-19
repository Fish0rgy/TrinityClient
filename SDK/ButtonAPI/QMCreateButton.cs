using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trinity.SDK.ButtonAPI
{
    internal class QMCreateButton
    {
        public static GameObject Createbutton(string text, GameObject parent, Action action)
        {
            var toinst = GameObject.Find("UserInterface").transform.Find("MenuContent/Backdrop/Header/Tabs/ViewPort/Content/SafetyPageTab");
            var instanciated = GameObject.Instantiate(toinst, parent.transform);
            reset(instanciated.gameObject);
            instanciated.transform.rotation = new Quaternion(0, 0, 0, 0); 
            instanciated.name = $"BTN_{text}";
            Component.Destroy(instanciated.GetComponent<VRCUiPageTab>());
            var button = instanciated.transform.Find("Button").GetComponent<UnityEngine.UI.Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(new Action(() => { action.Invoke();})); // add listener
            instanciated.transform.Find("Button/Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = text;
            return instanciated.gameObject;
        }
        public static void reset(GameObject gameobject)
        {
            gameobject.transform.localPosition = new Vector3(0, 0, 0);
            gameobject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gameobject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
