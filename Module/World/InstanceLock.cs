using Area51.Events;
using Photon.Realtime;

namespace Area51.Module.Exploit
{
    class InstanceLock : BaseModule, OnSendOPEvent
    {
        public InstanceLock() : base("Instance Lock", "Needs to be Master Client", Main.Instance.WorldButton, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnSendOPEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnSendOPEvents.Remove(this);
        }

        public bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions)
        {
            switch (opCode)
            {
                case 4:
                    return false;
                case 5:
                    return false;
                case 6:
                    return false;
                default:
                    return false;
            }
        }
    }
}
