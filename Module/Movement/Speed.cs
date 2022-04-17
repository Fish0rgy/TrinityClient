using Trinity.Utilities;
using Trinity.Events;
using VRC.SDKBase;
using Trinity.SDK;

namespace Trinity.Module.Movement
{
    internal class Speed : BaseModule
    {
        public Speed() : base("Speed", "go brrrrrrrrrrrrrrrrrr", Main.Instance.MovementButton, null, true, false)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("JUMP: <color=green>Increased Speed By 3x</color>");
            Networking.LocalPlayer.SetWalkSpeed(Networking.LocalPlayer.GetWalkSpeed() + 3f);
            Networking.LocalPlayer.SetRunSpeed(Networking.LocalPlayer.GetRunSpeed() + 3f);
            Networking.LocalPlayer.SetStrafeSpeed(Networking.LocalPlayer.GetStrafeSpeed() + 3f);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SPEED: <color=red>Reset Speed</color>");
            //i set the vavlue higher so when we minus it's close to default(Kanna#2950)
            Networking.LocalPlayer.SetWalkSpeed(6f);
            Networking.LocalPlayer.SetRunSpeed(7f);
            Networking.LocalPlayer.SetStrafeSpeed(6f);

            Networking.LocalPlayer.SetWalkSpeed(Networking.LocalPlayer.GetWalkSpeed() - 2f);
            Networking.LocalPlayer.SetRunSpeed(Networking.LocalPlayer.GetRunSpeed() - 2.5f);
            Networking.LocalPlayer.SetStrafeSpeed(Networking.LocalPlayer.GetStrafeSpeed() - 3f);
        }

    }
}
