using Photon.Realtime;

namespace Trinity.Events
{
    public interface OnSendOPEvent
    {
        bool OnSendOP(byte opCode, ref Il2CppSystem.Object parameters, ref RaiseEventOptions raiseEventOptions);
    }
}
 