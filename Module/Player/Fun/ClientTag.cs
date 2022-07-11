using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using Trinity.SDK;
using Trinity.Utilities;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace Trinity.Module.Player.Fun
{
    internal class ClientTag : BaseModule, OnRPCEvent
	{

		public ClientTag() : base("Client Tag", "Enables Global Tag For All Clients", Main.Instance.MiscButton, null, true, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("TAG: <color=green>Client Tag On</color>");
            Main.Instance.OnRPCEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("TAG: <color=red>Client Tag Off</color>");
            Main.Instance.OnRPCEvents.Remove(this);
        }

        public bool OnRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            string output = PU.LogTagRPC(sender, vrcEvent, vrcBroadcastType);
            if (output.Contains(PU.GetPlayer().prop_APIUser_0.displayName))
            {
                if (output.Contains("Munchen"))
                {
                    SDK.LogHandler.Log(SDK.LogHandler.Colors.Blue, $"{output}", false, false);
                    TagLocal();
                }
                if (output.Contains("Arctic"))
                {
                    SDK.LogHandler.Log(SDK.LogHandler.Colors.Blue, $"{output}", false, false);
                    TagLocal();
                } 
                if (output.Contains("Notorious"))
                {
                    SDK.LogHandler.Log(SDK.LogHandler.Colors.Blue, $"{output}", false, false);
                    TagLocal();
                } 
                if (output.Contains("Trinity"))
                {
                    SDK.LogHandler.Log(SDK.LogHandler.Colors.Blue, $"{output}", false, false);
                    TagLocal();
                } 
            }
            return true;
        }
        public void TagLocal()
        {
            foreach (Renderer renderer in PU.GetVRCPlayer().field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
            {
                renderer.sharedMaterial.SetColor("_Color", Color.red);
            }
        }
    }
}
