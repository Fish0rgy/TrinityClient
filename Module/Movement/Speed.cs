using Area51.Events;
using VRC.SDKBase;

namespace Area51.Module.Movement
{
    internal class Speed : BaseModule
    {
        public Speed() : base("Speed", "go brrrrrrrrrrrrrrrrrr", Main.Instance.MovementButton, null, true)
        {
        }

        public override void OnEnable()
        {
            Networking.LocalPlayer.SetWalkSpeed(Networking.LocalPlayer.GetWalkSpeed() + 2f);
            Networking.LocalPlayer.SetRunSpeed(Networking.LocalPlayer.GetRunSpeed() + 2f);
            Networking.LocalPlayer.SetStrafeSpeed(Networking.LocalPlayer.GetStrafeSpeed() + 3f);
        }

        public override void OnDisable()
        {
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
