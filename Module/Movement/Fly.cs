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


        public Fly() : base("Fly", "Fly high", Main.Instance.MovementButton, null, true, false)
        {
        }

        public override void OnEnable()
        {
            IsFly = true;
            vrcMotionState = VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<VRCMotionState>();
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = false;
            Main.Instance.OnUpdateEvents.Add(this);         
        }

        public override void OnDisable()
        {
            IsFly = false;
            vrcMotionState.Method_Public_Void_0();
            VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<CharacterController>().enabled = true;
            
        }


       public static bool ToggleFly()
        {
            if (IsFly != true)
            {
                IsFly = true;
                return IsFly;
            }
            IsFly = false;
            return IsFly;
        }

        public void OnUpdate()
        {
           if (IsFly == true)
            {
                if (RoomManager.field_Internal_Static_ApiWorld_0 == null) return;

                if (LocalPlayer == null || CameraTransform == null)
                {
                    LocalPlayer = PlayerWrapper.LocalPlayer;
                    CameraTransform = Camera.main.transform;
                }
                if (XRDevice.isPresent)
                {

                    if (Math.Abs(Input.GetAxis("Vertical")) != 0f) LocalPlayer.transform.position += CameraTransform.forward * 7f * Time.deltaTime * Input.GetAxis("Vertical");
                    if (Math.Abs(Input.GetAxis("Horizontal")) != 0f) LocalPlayer.transform.position += CameraTransform.right * 5f * Time.deltaTime * Input.GetAxis("Horizontal");
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f) LocalPlayer.transform.position += CameraTransform.up * 5f * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f) LocalPlayer.transform.position += CameraTransform.up * 5f * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift)) FlySpeed *= FlySpeed  ;
                    if (Input.GetKeyUp(KeyCode.LeftShift)) FlySpeed /= FlySpeed;
                    if (Input.GetKey(KeyCode.E)) LocalPlayer.transform.position += CameraTransform.up * 5f * Time.deltaTime;
                    if (Input.GetKey(KeyCode.Q)) LocalPlayer.transform.position += CameraTransform.up * -1f * 5f * Time.deltaTime;
                    if (Input.GetKey(KeyCode.W)) LocalPlayer.transform.position += CameraTransform.forward * 5f * Time.deltaTime;
                    if (Input.GetKey(KeyCode.A)) LocalPlayer.transform.position += CameraTransform.right * -1f * 5f * Time.deltaTime;
                    if (Input.GetKey(KeyCode.D)) LocalPlayer.transform.position += CameraTransform.right * 5f * Time.deltaTime;
                    if (Input.GetKey(KeyCode.S)) LocalPlayer.transform.position += CameraTransform.forward * -1f * 7f * Time.deltaTime;
                }
            }
        }
    }
}