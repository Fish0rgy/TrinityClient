using Trinity.Utilities;
using Trinity.SDK;
using VRC.Core;
using Trinity.Events;
using UnityEngine;
using VRC.SDKBase;
using VRCSDK2;
using System.Collections;


namespace Trinity.Module.TargetMenu
{
    internal class TargetOrbitch : BaseModule
    {
         
        public TargetOrbitch() : base("Items orbit", "Teleports items to selected user.", Main.Instance.Targetbutton, null, true, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("ITEMS: <color=green>Items Orbiting Target On</color>");
            PI playerInfo = PU.GetPlayerInformation(PU.SelectedVRCPlayer()); 
            Config.itemOrbit = true;
            LogHandler.LogDebug($"[Info] -> Items Orbitting -> {PU.SelectedVRCPlayer().prop_APIUser_0.displayName}");
        }

        public override void OnDisable()
        {
            MenuUI.Log("ITEMS: <color=red>Items Orbiting Target Off</color>");
            Config.itemOrbit = false;
            LogHandler.LogDebug($"[Info] -> Items removed -> {PU.SelectedVRCPlayer().prop_APIUser_0.displayName}");
        }
    } 
}
