using Trinity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trinity.SDK.ButtonAPI
{
    class VrConsoleLog
    {
        public static List<TextMeshProUGUI> LogText { get; set; } = new List<TextMeshProUGUI>();
        public VrConsoleLog(Transform parent, Sprite background, float x, float y, float z)
        {
            GameObject Console = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMNotificationsArea/DebugInfoPanel/Panel/Background"), parent);
            Console.name = "Trinity_ConsoleLog";
            Console.transform.parent = parent;
            Console.GetComponent<Image>().overrideSprite = background;      
            Console.transform.localPosition = new Vector3(x, y, z); //-3.0204f, -72.5408f, 0.0908f
            Console.transform.localScale = new Vector3(4.88f, 1.98f, 1);
            Console.transform.TransformPoint(x, y, z);
            Console.AddComponent<RectMask2D>();   
        } 
    }

}
