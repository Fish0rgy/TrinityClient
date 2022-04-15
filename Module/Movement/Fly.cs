using Trinity.Utilities;
using Trinity.Events;
using Trinity.SDK;
using System;
using UnityEngine;
using UnityEngine.XR;
using VRC.Animation;

namespace Trinity.Module.Movement
{
    class Fly : BaseModule, OnUpdateEvent
    {
        //https://github.com/PhoenixAceVFX/evolve-leak/blob/main/Movements/Movements.cs#L150 <- fly from this client

        private VRCMotionState vrcMotionState;
        private static VRC.Player LocalPlayer;
        private static Transform CameraTransform;
        public static float FlySpeed = 7f;
        public static bool IsFly, IsRunning = false;

        public static Vector3 origGrav = new();


        public Fly() : base("Fly", "Fly high", Main.Instance.MovementButton, null, true, false)
        {
        }

        public override void OnEnable()
        {
            IsFly = true;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = false;
            origGrav = Physics.gravity;
            Physics.gravity = Vector3.zero;
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            IsFly = false;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = true;
            Physics.gravity = origGrav;
            Main.Instance.OnUpdateEvents.Remove(this);
        }

        public void OnUpdate()
        {

           if (IsFly)
            {
                if (RoomManager.field_Internal_Static_ApiWorld_0 == null) return;

                if (LocalPlayer == null || CameraTransform == null)
                {
                    LocalPlayer = PU.GetPlayer();
                    CameraTransform = Camera.main.transform;
                }

                if (Input.GetAxis("Vertical") != 0f) LocalPlayer.transform.position += CameraTransform.forward * 7f * Time.deltaTime * Input.GetAxis("Vertical");
                if (Input.GetAxis("Horizontal") != 0f) LocalPlayer.transform.position += CameraTransform.right * 5f * Time.deltaTime * Input.GetAxis("Horizontal");
            }
        }
    }
}