using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.SDK.ButtonAPI;
using Trinity.Utilities;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;

namespace Trinity.Module.TargetMenu
{
    internal class TagPlayer : BaseModule
    {
        public TagPlayer() : base("Tag Player", "Client Tag", Main.Instance.Targetbutton, QMButtonIcons.LoadSpriteFromFile(Serpent.copyPath), false, false) { }

        public override void OnEnable()
        {
            MenuUI.Log($"PLAYER: <color=green>You Tagged {PU.SelectedVRCPlayer().prop_APIUser_0.displayName}</color>"); 
            if (PU.SelectedVRCPlayer().prop_APIUser_0.id != "")
            {
                VRC_EventHandler.VrcEvent vrcEvent = new VRC_EventHandler.VrcEvent
                {
                    EventType = VRC_EventHandler.VrcEventType.SendRPC,
                    ParameterObject = Networking.SceneEventHandler.gameObject,
                    ParameterInt = 1,
                    ParameterFloat = 0f,
                    ParameterString = $"[Client Tag] Tagger: {Trinity.Utilities.PU.GetPlayer().prop_APIUser_0.displayName} Tagged: {Trinity.Utilities.PU.SelectedVRCPlayer().prop_APIUser_0.displayName} Client: Trinity",
                };
                Networking.SceneEventHandler.TriggerEvent(vrcEvent, VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered, Trinity.Utilities.PU.GetPlayer().gameObject, 0f);

                if (PU.SelectedVRCPlayer() == null) return;
                foreach (Renderer renderer in PU.SelectedVRCPlayer()._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
                {
                    renderer.sharedMaterial.SetColor("_Color", Color.red);
                }
            }
        }
    }
}
