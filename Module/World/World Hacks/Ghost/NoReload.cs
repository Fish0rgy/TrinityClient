using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.Events;
using Trinity.SDK;
using UnityEngine;
using VRC;
using VRC.Networking;
using VRC.Udon;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class NoReload : BaseModule, OnUdonEvent
    { 
        public NoReload() : base("No Reload", "", Main.Instance.GhostButton, null, true, false)
        {
        }
        public override void OnEnable()
        {
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Activated No Reload", false, false);
            Main.Instance.OnUdonEvents.Add(this);
        }
        public override void OnDisable()
        {
            Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Red, "Deactivated No Reload", false, false);
            Main.Instance.OnUdonEvents.Remove(this);
        } 
        public bool OnUdon(string __0, VRC.Player __1, UdonSync __instance)
        {
            if (__0.Contains("Local_EndFiring") && __1.field_Private_APIUser_0.id.Equals(Trinity.Utilities.PU.GetPlayer().prop_APIUser_0.id))
            {
                switch (__instance.gameObject.name)
                {
                    case "T1-M1911":
                        {
                            UW.PathEvent(__instance.gameObject, "Local_FireOneShot", EventTarget.Local);
                        }
                        break;
                    case "T2-DesertEagle":
                        {
                            UW.PathEvent(__instance.gameObject, "Local_FireOneShot", EventTarget.Local);
                        }
                        break;
                    case "T2-m500":
                        {
                            UW.PathEvent(__instance.gameObject, "Local_FireOneShot", EventTarget.Local);
                        }
                        break;
                    case "T4-M107":
                        {
                            UW.PathEvent(__instance.gameObject, "InitializeWeapon", EventTarget.Local);
                            UW.PathEvent(__instance.gameObject, "Local_StartFiring", EventTarget.Local);
                        }
                        break;
                    case "T2-MP7":
                        {
                            UW.PathEvent(__instance.gameObject, "InitializeWeapon", EventTarget.Local);
                            UW.PathEvent(__instance.gameObject, "Local_StartFiring", EventTarget.Local);
                        }
                        break; 
                    case "T3-P90":
                        {
                            UW.PathEvent(__instance.gameObject, "InitializeWeapon", EventTarget.Local);
                            UW.PathEvent(__instance.gameObject, "Local_StartFiring", EventTarget.Local);
                        }
                        break;
                    case "T4-M249":
                        {
                            UW.PathEvent(__instance.gameObject, "InitializeWeapon", EventTarget.Local);
                            UW.PathEvent(__instance.gameObject, "Local_StartFiring", EventTarget.Local);
                        }
                        break;
                    case "T3-Vector":
                        {
                            UW.PathEvent(__instance.gameObject, "InitializeWeapon", EventTarget.Local);
                            UW.PathEvent(__instance.gameObject, "Local_StartFiring", EventTarget.Local);
                        }
                        break;
                }
            }
            return true;
        }
    }
}
