using Trinity.Utilities;
using Trinity.SDK.ButtonAPI;
using System;
using UnityEngine;

namespace Trinity.Module
{
    abstract class BaseModule
    {
        public string name;
        public bool toggled;
        public bool save;

        string discription;
        QMNestedButton category;
        bool isToggle;
        Sprite image;

        public BaseModule(string name, string discription, QMNestedButton category, Sprite image = null, bool isToggle = false, bool save = false)
        {
            this.name = name;
            this.discription = discription;
            this.category = category;
            this.isToggle = isToggle;
            this.save = save;
            this.image = image;
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        

        public virtual void OnPreferencesSaved()
        {

        }

        public virtual void OnUIInit()
        {
            if (isToggle)
            {
                new QMToggleButton(category.menuTransform, name, discription, new Action<bool>((bool state) =>
                {
                    this.toggled = state;
                    if (state)
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
               
                }));
            }
            else
            {
                new QMSingleButton(category.menuTransform, name, discription, image, delegate
                {
                    OnEnable();
                });
            }
        }
    }
}
