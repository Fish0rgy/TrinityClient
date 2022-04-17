using Trinity.Utilities;
using Trinity.Events;
using VRC.SDKBase;
using Trinity.SDK;

namespace Trinity.Module.World
{
    internal class InfinityJump : BaseModule, OnUpdateEvent
    {
        public InfinityJump() : base("Jet Pack", "InfinityJump go brrrr", Main.Instance.MovementButton, null, true, true) { }

        public void OnUpdate()
        {
            {
                if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f)
                {
                    var velocity = Networking.LocalPlayer.GetVelocity();
                    velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                    Networking.LocalPlayer.SetVelocity(velocity);
                }
            }
        }

        public override void OnEnable()
        {
            MenuUI.Log("JUMP: <color=green>Infinite Jump On</color>");
            Main.Instance.OnUpdateEvents.Add(this);
        }

        public override void OnDisable()
        {
            MenuUI.Log("JUMP: <color=red>Infinite Jump Off</color>");
            Main.Instance.OnUpdateEvents.Remove(this);
        }
    }
}