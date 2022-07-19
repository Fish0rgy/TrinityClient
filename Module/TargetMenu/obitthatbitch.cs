using Trinity.Utilities;
using Trinity.SDK;
using VRC.Core;
using Trinity.Events;
using UnityEngine;
using VRC.SDKBase;
using VRCSDK2;
using System.Collections;
using System.Linq;

namespace Trinity.Module.TargetMenu
{
    internal class TargetOrbitch : BaseModule, OnUpdateEvent
    {
        private GameObject puppet;
        VRC.Player player = null;
        public TargetOrbitch() : base("Items orbit", "Teleports items to selected user.", Main.Instance.Targetbutton, null, true, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("ITEMS: <color=green>Items Orbiting Target On</color>");
            player = PU.SelectedVRCPlayer();  
            LogHandler.LogDebug($"[Info] -> Items Orbitting -> {PU.SelectedVRCPlayer().prop_APIUser_0.displayName}");
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("ITEMS: <color=red>Items Orbiting Target Off</color>");
            Config.itemOrbit = false;
            LogHandler.LogDebug($"[Info] -> Items removed -> {PU.SelectedVRCPlayer().prop_APIUser_0.displayName}");
            Main.Instance.OnUpdateEvents.Remove(this);
        }

        public void OnUpdate()
        {
            if (this.puppet == null || player == null)
            {
                this.puppet = new GameObject();
                player = PU.SelectedVRCPlayer();
            } 
            this.puppet.transform.position = player.GetVRCPlayerApi().GetBonePosition(HumanBodyBones.Head); //change these vectors to adjust orbit distance
            WorldWrapper.vrc_Pickups.ToList().ForEach(item =>
            {
                if (Networking.GetOwner(item.gameObject) != Networking.LocalPlayer)
                {
                    Networking.SetOwner(Networking.LocalPlayer, item.gameObject); //think we can change this to allow for targeted orbit, would like to get orbit head working lol
                } 
                item.transform.position = player.GetVRCPlayerApi().GetBonePosition(HumanBodyBones.Head) + this.puppet.transform.forward * 0.2f;
                this.puppet.transform.Rotate(new Vector3(0f, (float)(360 / WorldWrapper.vrc_Pickups.Length), 0f));
            });
        }
    } 
}
