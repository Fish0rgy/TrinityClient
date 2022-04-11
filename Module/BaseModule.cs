using Area51.SDK.ButtonAPI;
using System;
using UnityEngine;

namespace Area51.Module
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
                QMToggleButton qMToggleButton = new QMToggleButton(category.menuTransform, name, discription, new Action<bool>((bool state) =>
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
                    Main.Instance.OnEventEventArray = Main.Instance.OnEventEvents.ToArray();
                    Main.Instance.OnPlayerJoinEventArray = Main.Instance.OnPlayerJoinEvents.ToArray();
                    Main.Instance.OnPlayerLeaveEventArray = Main.Instance.OnPlayerLeaveEvents.ToArray();
                    Main.Instance.OnRPCEventArray = Main.Instance.OnRPCEvents.ToArray();
                    Main.Instance.OnSendOPEventArray = Main.Instance.OnSendOPEvents.ToArray();
                    Main.Instance.OnUdonEventArray = Main.Instance.OnUdonEvents.ToArray();
                    Main.Instance.OnUpdateEventArray = Main.Instance.OnUpdateEvents.ToArray();
                    Main.Instance.OnAssetBundleLoadEventArray = Main.Instance.OnAssetBundleLoadEvents.ToArray();
                    Main.Instance.OnNetworkSanityArray = Main.Instance.OnNetworkSanityEvents.ToArray();
               
                }));
                if (save)
                {
                    if (Main.Instance.Config.getConfigBool(name))
                    {
                    
                    }
                }
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
