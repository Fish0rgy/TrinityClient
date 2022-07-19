using Trinity.Utilities;
using UnityEngine;

namespace Trinity.SDK.ButtonAPI
{
    public class QMNestedButton
    {
        public QMMenu menu;
        public Transform menuTransform;

        public QMSingleButton button;

        public QMNestedButton(Transform perant, string name, Sprite icon = null)
        {
            menu = new QMMenu(name, name, false, true);
            menuTransform = menu.menuContents;

            button = new QMSingleButton(perant, name, name, icon, delegate
            {
                menu.OpenMenu();
            });
        }
    }
}
 