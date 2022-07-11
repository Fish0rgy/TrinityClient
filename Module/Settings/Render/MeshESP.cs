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

namespace Trinity.Module.Settings.Render
{
    internal class MeshESP : BaseModule, OnPlayerJoinEvent, OnUpdateEvent
    { 
        public MeshESP() : base("Mesh ESP", "See Players n shit", Main.Instance.SettingsButtonrender, null, true, false) { }

        public override void OnEnable()
        {
            MenuUI.Log("ESP: <color=green>Player Mesh ESP On</color>");
            for (int i = 0; i < PU.GetAllPlayers().Length; i++)
            {
                VRC.Player player = PU.GetAllPlayers()[i];
                if (player != PU.GetPlayer())
                {
                    if (PU.GetIsFriend(player.prop_APIUser_0))
                        GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
                    else
                        GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
                }
                foreach (Renderer renderer in player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, true);
                }
            }
            Main.Instance.OnPlayerJoinEvents.Add(this);
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        { 
            MenuUI.Log("ESP: <color=red>Player Mesh ESP Off</color>");
            for (int i = 0; i < PU.GetAllPlayers().Length; i++)
            {
                VRC.Player player = PU.GetAllPlayers()[i];
                //if (PU.GetIsFriend(player.prop_APIUser_0))
                //    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
                //else
                //    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
                foreach (Renderer renderer in player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
                {
                    HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, false);
                }
            }
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            Main.Instance.OnUpdateEvents.Remove(this);
        }
        public static void PlayerMeshEsp(VRC.Player player, bool State)
        { 
            //if (PU.GetIsFriend(player.prop_APIUser_0))
            //    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
            //else
            //    GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
            foreach (Renderer renderer in player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
            {
                HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, State); 
            } 
        }
        public void OnPlayerJoin(VRC.Player __0)
        {
            foreach (Renderer renderer in __0._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
            {
                HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, true);
            }
        }

        public void OnUpdate()
        {
            try
            {
                for (int i = 0; i < PU.GetAllPlayers().Length; i++)
                {
                    VRC.Player player = PU.GetAllPlayers()[i];

                    if(player != PU.GetPlayer())
                    {
                        if (PU.GetIsFriend(player.prop_APIUser_0))
                            GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.yellow;
                        else
                            GameObject.Find("Camera (eye)").GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
                    } 
                    foreach (Renderer renderer in player._vrcplayer.field_Internal_GameObject_0.GetComponentsInChildren<Renderer>())
                    {
                        HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(renderer, true);
                    }
                }
            }
            catch (Exception ex)
            { 
            }
             
        }
        public static void HighlightColor(Color highlightcolor)
        {
            if (Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().Count != 0)
                Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>().FirstOrDefault().highlightColor = highlightcolor;
        }
    }
}
