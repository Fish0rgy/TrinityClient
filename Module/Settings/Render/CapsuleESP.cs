using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using Photon.Realtime;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using VRC;

namespace Trinity.Module.Settings.Render
{
    class CapsuleEsp : BaseModule, OnPlayerJoinEvent, OnUpdateEvent
    {
        public CapsuleEsp() : base("Player ESP", "See Players n shit", Main.Instance.SettingsButtonrender, null, true, false) { }
        public override void OnEnable()
        {
            MenuUI.Log("ESP: <color=green>Player ESP On</color>");
            for (int i = 0; i < PU.GetAllPlayers().Length; i++)
            {
                VRC.Player player = PU.GetAllPlayers()[i];
                if (PU.GetIsFriend(player.prop_APIUser_0))
                    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
                else
                    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
                Renderer renderer;
                if (player == null) return;
                Transform transform = player.transform.Find("SelectRegion");
                renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
                Renderer renderer2 = renderer;
                if (renderer2) HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, true);
            }
            Main.Instance.OnPlayerJoinEvents.Add(this);
            Main.Instance.OnUpdateEvents.Add(this);
        }
        public override void OnDisable()
        {
            MenuUI.Log("ESP: <color=red>Player ESP Off</color>");
            for (int i = 0; i < PU.GetAllPlayers().Length; i++)
            {
                VRC.Player player = PU.GetAllPlayers()[i];
                Renderer renderer;
                if (player == null) return;
                Transform transform = player.transform.Find("SelectRegion");
                renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
                Renderer renderer2 = renderer;
                if (renderer2) HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, false);
            }
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            Main.Instance.OnUpdateEvents.Remove(this);
        } 
        public void OnPlayerJoin(VRC.Player __0)
        {
            if (PU.GetIsFriend(__0.prop_APIUser_0))
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
            else
                GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
            Renderer renderer;
            if (__0 == null) return;
            Transform transform = __0.transform.Find("SelectRegion");
            renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
            Renderer renderer2 = renderer;
            if (renderer2) HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, true);

        }
        public static void HighlightColor(Color highlightcolor){
            if (Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().Count != 0)
                Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().FirstOrDefault().highlightColor = highlightcolor;
        }
        public void OnUpdate()
        {
            for (int i = 0; i < PU.GetAllPlayers().Length; i++)
            {
                VRC.Player player = PU.GetAllPlayers()[i];
                if (PU.GetIsFriend(player.prop_APIUser_0))
                    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
                else
                    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
                Renderer renderer;
                if (player == null) return;
                Transform transform = player.transform.Find("SelectRegion");
                renderer = ((transform != null) ? transform.GetComponent<Renderer>() : null);
                Renderer renderer2 = renderer;
                if (renderer2) HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer2, true);
            }
        }
    }
}