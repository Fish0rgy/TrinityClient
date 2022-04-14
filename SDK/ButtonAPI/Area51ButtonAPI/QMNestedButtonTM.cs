using Trinity.Utilities;
using UnityEngine;

namespace Area51.SDK.ButtonAPI
{
    public class QMNestedButtonTM
    {
        public QMMenu menu;
        public Transform menuTransform;

        public QMNestedButtonTM(Transform perant, string name, Sprite icon = null)
        {
            menu = new QMMenu(name, name, false, true);
            menuTransform = menu.menuContents;

            new QMSingleButtonTM(perant, name, name, icon, delegate
            {
                menu.OpenMenu();
            });
        }

    }
}
