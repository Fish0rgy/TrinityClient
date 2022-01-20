using Photon.Realtime;

namespace Area51.Events
{
    public interface OnSendOPEvent
    {
        bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions);
    }
}
