using Trinity.Utilities;
using Trinity.Events;
using Photon.Realtime;
using Trinity.SDK;

namespace Trinity.Module.Exploit
{
    class InstanceLock : BaseModule, OnSendOPEvent
    {
        public InstanceLock() : base("Instance Lock", "Needs to be Master Client", Main.Instance.WorldButton, null, true, false)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnSendOPEvents.Add(this);
            MenuUI.Log("WORLD: <color=green>Instance Is Locked</color>");
        }

        public override void OnDisable()
        {
            Main.Instance.OnSendOPEvents.Remove(this);
            MenuUI.Log("WORLD: <color=red>Instance Is Unlocked</color>");
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
