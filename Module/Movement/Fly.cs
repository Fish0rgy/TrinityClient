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

        private VRCMotionState vrcMotionState;
        private static VRC.Player LocalPlayer;
        private static Transform CameraTransform; 
        public static float FlySpeed = 2f;
        public static bool IsFly, IsRunning = false;

        public static Vector3 origGrav = new();


        public Fly() : base("Fly", "Fly high", Main.Instance.MovementButton, null, true, false)
        {
        }

        public override void OnEnable()
        {
            IsFly = true;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = false;
            Main.Instance.OnUpdateEvents.Add(this);
            origGrav = Physics.gravity;
            Physics.gravity = Vector3.zero;
            MenuUI.Log("Fly: <color=green>ON</color>");
        }

        public override void OnDisable()
        {
            IsFly = false;
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = true; 
            Main.Instance.OnUpdateEvents.Remove(this);
            Physics.gravity = origGrav; 
            MenuUI.Log("Fly: <color=red>OFF</color>");
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
                if (Input.GetAxis("Horizontal") != 0f) LocalPlayer.transform.position += CameraTransform.right * 7f * Time.deltaTime * Input.GetAxis("Horizontal");
                if (Input.GetKey(KeyCode.E)) LocalPlayer.transform.position += CameraTransform.up * 7f * Time.deltaTime;
                if (Input.GetKey(KeyCode.Q)) LocalPlayer.transform.position += CameraTransform.up * -FlySpeed *2f * Time.deltaTime;
                if (Input.GetKey(KeyCode.A)) LocalPlayer.transform.position += CameraTransform.right * -FlySpeed * 2f * Time.deltaTime;
                if (Input.GetKey(KeyCode.D)) LocalPlayer.transform.position += CameraTransform.right * 7f * Time.deltaTime;
            }
        }
    }
}