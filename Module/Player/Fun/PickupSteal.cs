using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Trinity.Module.Player.Fun
{
    internal class PickupSteal : BaseModule
    {

        public PickupSteal() : base("Item Steal", "Fuck your Items", Main.Instance.MiscButton, null, false, false) { }

		public override void OnEnable()
		{
			VRC_Pickup[] ItemArray = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
			foreach (VRC_Pickup Items in ItemArray)
			{
				bool Check = Items.gameObject;
				if (Check)
				{
					Items.DisallowTheft = false;
				}
			}
			VRC_Pickup[] ItemArray1 = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
			foreach (VRC_Pickup Items1 in ItemArray1)
			{
				bool Check1 = Items1.gameObject;
				if (Check1)
				{
					Items1.DisallowTheft = false;
				}
			}
			VRCPickup[] ItemArray2 = Resources.FindObjectsOfTypeAll<VRCPickup>().ToArray<VRCPickup>();
			foreach (VRCPickup Items3 in ItemArray2)
			{
				bool Check2 = Items3.gameObject;
				if (Check2)
				{
					Items3.DisallowTheft = false;
				}
			}
		}
    }
}
