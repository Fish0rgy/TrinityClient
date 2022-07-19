using Trinity.Utilities;
using Trinity.SDK.ButtonAPI;
using System;
using UnityEngine;

namespace Trinity.Module
{
    public abstract class BaseModule
    {
        public string name;
        public bool toggled;
        public bool save;

        string discription; 
        QMNestedButton category;
        bool isToggle;
        Sprite image;

        public QMToggleButton toggleButton;
        public QMSingleButton singleButton;

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
        public virtual void SocialMenuInitialized()
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
                toggleButton = new QMToggleButton(category.menuTransform, name, discription, new Action<bool>((bool state) =>
                {
                    this.toggled = state;
                    switch (state)
                    {
                        case true: 
                            {
                                OnEnable();
                            }
                            break;
                        case false:
                            {
                                OnDisable();
                            }
                            break;
                    }
                }));
                if (save)
                {
                    if (Trinity.SDK.Config.getConfigBool(name) == true)
                    {
                        toggleButton.Toggle(true);
                    }
                }
            }
            else
            {
                singleButton = new QMSingleButton(category.menuTransform, name, discription, image, delegate
                {
                    OnEnable();
                });
            }
        }
         
    }
}
