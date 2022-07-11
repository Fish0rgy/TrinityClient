using System;
using UnityEngine;
using VRC;
using System.Collections;
using System.Collections.Generic;
using VRCSDK2;
using VRC.SDKBase;
using System.Linq;
using static VRC.SDKBase.VRC_EventHandler; 
using VRC.Core;
using VRC.SDK3.Components;
using MelonLoader; 

namespace Trinity.SDK
{
    internal class WSK
    {
		 
		public static void FloorDropper()
		{
			System.Collections.Generic.List<VRCSDK2.VRC_Pickup> AllPickups = UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Pickup>().ToList<VRCSDK2.VRC_Pickup>();
			System.Collections.Generic.List<VRCPickup> AllUdonPickups = UnityEngine.Object.FindObjectsOfType<VRCPickup>().ToList<VRCPickup>();
			System.Collections.Generic.List<VRC_ObjectSync> AllSyncPickups = UnityEngine.Object.FindObjectsOfType<VRC_ObjectSync>().ToList<VRC_ObjectSync>();
			{
				for (int i = 0; i < AllPickups.Count; i++)
				{
					MelonCoroutines.Start(FreeFallPickup(AllPickups[i], 10));
				}
				for (int j = 0; j < AllUdonPickups.Count; j++)
				{
					MelonCoroutines.Start(FreeFallPickup(AllUdonPickups[j], 10));
				}
				for (int k = 0; k < AllSyncPickups.Count; k++)
				{
					VRC_ObjectSync vrc_ObjectSync = AllSyncPickups[k];
					vrc_ObjectSync.GetComponent<Rigidbody>().useGravity = true;
					vrc_ObjectSync.isKinematic = false;
					vrc_ObjectSync.useGravity = true;
					MelonCoroutines.Start(FreeFallPickup(vrc_ObjectSync, 10));
				}
			}
		}


		 

		public static IEnumerator FreeFallPickup(VRCPickup Pickup, int HowMuchTime)
		{
			TakeOwnershipIfNecessary(Pickup.gameObject);
			Pickup.GetComponent<Rigidbody>().mass = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().useGravity = true;
			Pickup.GetComponent<Rigidbody>().velocity = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
			Pickup.GetComponent<Rigidbody>().maxAngularVelocity = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().maxDepenetrationVelocity = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().isKinematic = false;
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Acceleration);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Force);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Impulse);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.VelocityChange);
			Pickup.GetComponent<Rigidbody>().angularVelocity = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
			float TimeToStop = Time.time + (float)HowMuchTime;
			while (Time.time < TimeToStop)
			{
				if (!Pickup.name.ToLower().Contains("viewdinder") && !Pickup.name.ToLower().Contains("avatardebugconsole"))
					Pickup.gameObject.SetActive(true);

				Pickup.gameObject.transform.position = new Vector3(0f, 0f, 0f);
				yield return new WaitForSeconds(1f);
				Pickup.gameObject.transform.position = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
				yield return new WaitForSeconds(2f);
			}
			Pickup.gameObject.transform.position = new Vector3(0f, 0f, 0f);
			yield break;
		}

		public static IEnumerator FreeFallPickup(VRCSDK2.VRC_Pickup Pickup, int HowMuchTime)
		{
			TakeOwnershipIfNecessary(Pickup.gameObject);
			Pickup.GetComponent<Rigidbody>().mass = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().useGravity = true;
			Pickup.GetComponent<Rigidbody>().velocity = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
			Pickup.GetComponent<Rigidbody>().maxAngularVelocity = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().maxDepenetrationVelocity = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().isKinematic = false;
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Acceleration);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Force);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Impulse);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.VelocityChange);
			Pickup.GetComponent<Rigidbody>().angularVelocity = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
			float TimeToStop = Time.time + (float)HowMuchTime;
			while (Time.time < TimeToStop)
			{
				if (!Pickup.name.ToLower().Contains("viewdinder") && !Pickup.name.ToLower().Contains("avatardebugconsole"))
					Pickup.gameObject.SetActive(true);

				Pickup.gameObject.transform.position = new Vector3(0f, 0f, 0f);
				yield return new WaitForSeconds(1f);
				Pickup.gameObject.transform.position = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
				yield return new WaitForSeconds(2f);
			}
			Pickup.gameObject.transform.position = new Vector3(0f, 0f, 0f);
			yield break;
		}

		public static IEnumerator FreeFallPickup(VRC_ObjectSync Pickup, int HowMuchTime)
		{
			TakeOwnershipIfNecessary(Pickup.gameObject);
			Pickup.GetComponent<Rigidbody>().mass = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().useGravity = true;
			Pickup.GetComponent<Rigidbody>().velocity = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
			Pickup.GetComponent<Rigidbody>().maxAngularVelocity = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().maxDepenetrationVelocity = 2.1474836E+09f;
			Pickup.GetComponent<Rigidbody>().isKinematic = false;
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Acceleration);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Force);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.Impulse);
			Pickup.GetComponent<Rigidbody>().AddForce(new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f), ForceMode.VelocityChange);
			Pickup.GetComponent<Rigidbody>().angularVelocity = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
			float TimeToStop = Time.time + (float)HowMuchTime;
			while (Time.time < TimeToStop)
			{
				if (!Pickup.name.ToLower().Contains("viewdinder") && !Pickup.name.ToLower().Contains("avatardebugconsole"))
					Pickup.gameObject.SetActive(true);

				Pickup.gameObject.transform.position = new Vector3(0f, 0f, 0f);
				yield return new WaitForSeconds(1f);
				Pickup.gameObject.transform.position = new Vector3(2.1474836E+09f, 2.1474836E+09f, 2.1474836E+09f);
				yield return new WaitForSeconds(2f);
			}
			Pickup.gameObject.transform.position = new Vector3(0f, 0f, 0f);
			yield break;
		}

		public static void TakeOwnershipIfNecessary(GameObject gameObject)
		{
			if (getOwnerOfGameObject(gameObject) != Trinity.Utilities.PU.GetPlayer())
				Networking.SetOwner(Trinity.Utilities.PU.GetPlayer()._vrcplayer.field_Private_VRCPlayerApi_0, gameObject);

		}
		public static Player getOwnerOfGameObject(GameObject gameObject)
		{
			foreach (Player player in Trinity.Utilities.PU.GetAllPlayers().ToList<Player>())
				if (player.field_Private_VRCPlayerApi_0.IsOwner(gameObject))
					return player;

			return null;
		}
	}  
}
