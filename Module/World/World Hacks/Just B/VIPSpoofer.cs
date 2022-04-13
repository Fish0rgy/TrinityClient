using System;
using System.Collections;
using Trinity.Events;
using Trinity.SDK;
using MelonLoader;
using UnityEngine;
using VRC;
using VRC.SDKBase; 

namespace Trinity.Module.World.World_Hacks.Just_B
{
    class VIPSpoofer : BaseModule, OnUpdateEvent
    {
        private static bool state = false; 
        public VIPSpoofer() : base("VIP Spoofer", "Not Local : EVERYONE CAN SEE THIS !", Main.Instance.Justbbutton, null, true)
        {

        }
        public override void OnEnable()
        {
            state = true;
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            state = false;
            Main.Instance.OnUpdateEvents.Remove(this);
        }

        public void OnUpdate()
        {
            try
            {
                if (!state) return;
                if (PlayerWrapper.LocalPlayer.field_Private_APIUser_0._displayName_k__BackingField != "Blue-kun")
                {
                    MelonLoader.MelonCoroutines.Start(JustBMisc.spoofVIP());
                    PlayerWrapper.SpoofDisplayname("Blue-kun");
                }
            } catch{}
        }
    }
}
