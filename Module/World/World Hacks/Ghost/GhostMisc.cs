using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using Trinity.Utilities;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trinity.Module.World.World_Hacks.Ghost
{
    internal class GhostMisc
    {
		//Reward_M107 = 50 cal
		//Reward_RiotShield = riot shield
		//RewardB_Shotgun = shotgun
		//Reward_Vector = vector
		public static void KillHumans()
		{ 
			SDK.UW.ObjectEvent("DamageSync", "BackStabDamage", EventTarget.Everyone);
		}
		public static void KillTarget()
		{
			SDK.UW.ObjectEvent("DamageSync", "BackStabDamage", EventTarget.Targeted);
		}
		public static void startgame()
        {
			SDK.UW.udonsend("Local_ReadyStartGame", EventTarget.Everyone);
		}
		public static void resetClues()
        {
			SDK.UW.ObjectEvent("NameClueSystem", "OnGameEnd", EventTarget.Everyone);
		}
		public static void UnlockBoxs()
        {
			UW.ObjectEvent("", "Local_RegisterReward", EventTarget.Everyone);
			UW.ObjectEvent("", "Local_Unlock", EventTarget.Everyone);
        }
		public static void GiveSelfMaxCurrency()
		{
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Local);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Local);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Local);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Local);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Local);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Local);
		}
		public static void GiveTargetMaxCurrency()
        {
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Targeted);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Targeted);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Targeted);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Targeted);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Targeted);
			SDK.UW.udonsend("OnSuspiciousKill", EventTarget.Targeted);
		}
		public static void ClaimObject(string clue, int target)
        {
            switch (target) 
			{
				case 0:
                    {
						GameObject Clue = GameObject.Find(clue);
						if (Clue)
						{
							Clue.active = true;
							Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Clue);
							Clue.transform.position = new Vector3(-3.1868f, 10.5996f, 1031.892f);
						}
					}
					break;
				case 1:
                    {
						GameObject Clue = GameObject.Find(clue);
						if (Clue)
						{
							Clue.active = true;
							Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Clue);
							Clue.transform.position = PU.GetPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
						}
					}
					break;
			}
		}
		public static void GiveSniper()
        {
			GameObject sniper = GameObject.Find("/Functions/LockBox/LootBoxCloset (9)/LootBoxSystem/Rewards/Reward_M107/Reward_M107");
			GameObject lootbox = GameObject.Find("House/Functions/LockBox/LootBoxCloset (9)/LootBoxSystem");
			UW.PathEvent(sniper,"Net_SpawnObject",EventTarget.Local); 
			lootbox.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OpenLockBox");
			PU.GetPlayer().transform.position = lootbox.transform.position;
		}
    }
}
