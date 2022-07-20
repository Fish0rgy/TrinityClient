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
        public MeshESP() : base("Mesh ESP", "See Players n shit", Main.Instance.SettingsButtonrender, null, true, true) { }

        public override void OnEnable()
        {
            MenuUI.Log("ESP: <color=green>Player Mesh ESP On</color>");
            try { PU.GetAllPlayers().ToList().ForEach(player => { Misc.MeshESP(player, true); }); } catch (Exception ex) { }
            Main.Instance.OnPlayerJoinEvents.Add(this);
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        { 
            MenuUI.Log("ESP: <color=red>Player Mesh ESP Off</color>");
            PU.GetAllPlayers().ToList().ForEach(player =>
            {
                Misc.MeshESP(player, false);
            });
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            Main.Instance.OnUpdateEvents.Remove(this);
        } 
        public void OnPlayerJoin(VRC.Player __0)
        {
            try { Misc.MeshESP(__0, true); } catch { } 
        }

        public void OnUpdate()
        {
            try {
                PU.GetAllPlayers().ToList().ForEach(player =>
                {
                    Misc.MeshESP(player, true);
                });
            } catch (Exception ex) {  }
        }
    }
}
