using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using VRC.Networking;

namespace Trinity.Module.World.World_Hacks.Murder_4
{
    internal class ReflectDamage : BaseModule, OnUdonEvent
    {
        public ReflectDamage() : base("Reflect Damage", "", Main.Instance.Murderbutton, null, true, false)
        {
        }
        public override void OnEnable()
        {
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Activated GodMode", false, false);
            Main.Instance.OnUdonEvents.Add(this);
        }
        public override void OnDisable()
        {
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, "Deactivated GodMode", false, false);
            Main.Instance.OnUdonEvents.Remove(this);
        }

        public bool OnUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            bool godmode = __0 == "SyncKill";
            if (godmode)
            {
                Trinity.SDK.UW.TargetedEvent(__0, __1.prop_VRCPlayer_0);
                Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, $"Reflected Kill Event And Killed {__1.prop_APIUser_0.displayName}", false, false);
                Trinity.SDK.MenuUI.Log($"MURDER 4: <color=green>Reflected Damage From {__1.prop_APIUser_0.displayName}</color>");
                return false;
            }
            return true;
        }
    }
}
