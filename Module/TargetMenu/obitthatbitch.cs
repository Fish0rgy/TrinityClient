using Trinity.SDK;
using VRC.Core;
using Trinity.Events;
using UnityEngine;
using VRC.SDKBase;
using VRCSDK2;
using System.Collections;


namespace Trinity.Module.TargetMenu
{
    internal class TargetOrbitch : BaseModule
    {

        private GameObject puppet;
        public VRC.Player GetSelectedUser() => PlayerWrapper.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0);

        public TargetOrbitch() : base("Items orbit", "Teleports items to selected user.", Main.Instance.Targetbutton, null, true, false) { }

        public override void OnEnable()
        {
            MelonLoader.MelonCoroutines.Start(ItemRotate());
            LogHandler.LogDebug($"[Info] -> Items Orbitting -> {PlayerWrapper.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0.displayName}");
        }

        public override void OnDisable()
        {
            MelonLoader.MelonCoroutines.Stop(ItemRotate());
     
            LogHandler.LogDebug($"[Info] -> Items removed -> {PlayerWrapper.GetByUsrID( Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0).prop_APIUser_0.displayName}");
        }



        public IEnumerator ItemRotate()
        {
            var setplay = PlayerWrapper.GetByUsrID(Main.Instance.QuickMenuStuff.selectedUserMenuQM.GetSelectedUser().prop_String_0);
            if (this.puppet == null)
            {
                this.puppet = new GameObject();
            }
            this.puppet.transform.position = setplay.transform.position + new Vector3(0f, 0.2f, 0f);
            this.puppet.transform.Rotate(new Vector3(0f, 360f * Time.time * 1f, 0f));
            for (int i = 0; i < WorldWrapper.vrc_Pickups.Length; i++)
            {
                VRC.SDKBase.VRC_Pickup vrc_Pickup = WorldWrapper.vrc_Pickups[i];
                if (Networking.GetOwner(vrc_Pickup.gameObject) != Networking.LocalPlayer)
                {
                    Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
                }
                vrc_Pickup.transform.position = this.puppet.transform.position + this.puppet.transform.forward * 1f;
                this.puppet.transform.Rotate(new Vector3(0f, (float)(360 / WorldWrapper.vrc_Pickups.Length), 0f));
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }

    


    }
}
