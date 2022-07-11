using Trinity.Utilities;
using Trinity.Events;
using VRC.SDKBase;
using Trinity.SDK;

namespace Trinity.Module.Movement
{
    internal class Speed : BaseModule
    {
        public int speed = 3;
        public Speed() : base("Speed", "go brrrrrrrrrrrrrrrrrr", Main.Instance.MovementButton, null, true, false)
        {
        }

        public override void OnEnable()
        {
            MenuUI.Log("SPEED: <color=green>Increased Speed By 3x</color>");
            Networking.LocalPlayer.SetWalkSpeed(Networking.LocalPlayer.GetWalkSpeed() + speed);
            Networking.LocalPlayer.SetRunSpeed(Networking.LocalPlayer.GetRunSpeed() + speed);
            Networking.LocalPlayer.SetStrafeSpeed(Networking.LocalPlayer.GetStrafeSpeed() + speed);
        }

        public override void OnDisable()
        {
            MenuUI.Log("SPEED: <color=red>Reset Speed</color>");
            //i set the vavlue higher so when we minus it's close to default(Kanna#2950)
            Networking.LocalPlayer.SetWalkSpeed(2);
            Networking.LocalPlayer.SetRunSpeed(4);
            Networking.LocalPlayer.SetStrafeSpeed(2);
        }

    }
}
