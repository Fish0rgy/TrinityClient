using Trinity.Utilities;
using Area51.Module.World.World_Hacks.Murder_4;
using Area51.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.SDKBase;

namespace Area51.Module.World
{
    class RespawnItems : BaseModule
    {
        public RespawnItems() : base("Reset Items", "No one besides you can use Pickups", Main.Instance.worldButton, null, false)
        {
        }

        public override void OnEnable()
        {
            try
            {
                Il2CppArrayBase<VRC_Pickup> il2CppArrayBase = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
                foreach (VRC_Pickup vrc_Pickup in il2CppArrayBase)
                {
                    bool Object = !(vrc_Pickup == null) && !(vrc_Pickup.gameObject == null) && vrc_Pickup.gameObject.active && vrc_Pickup.enabled && vrc_Pickup.pickupable && !vrc_Pickup.name.Contains("ViewFinder") && !(HighlightsFX.prop_HighlightsFX_0 == null);
                    if (Object)
                    {
                        vrc_Pickup.transform.position = new Vector3(0f, 0f, 0f);
                    }
                } 
            }
            catch (Exception ex)
            {
                Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
            }
        }
    }
}
 