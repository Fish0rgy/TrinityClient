using Area51.Events;
using Area51.SDK;
using VRC.SDKBase;

namespace Area51.Module.Settings.Logging
{
    class RpcLogger : BaseModule, OnRPCEvent
    {
        public RpcLogger() : base("RPCLogger", "Logs RPCs", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnRPCEvents.Add(this);
        }

        public override void OnDisable()
        {
            Main.Instance.OnRPCEvents.Remove(this);
        }

        public bool OnRPC(VRC.Player sender, VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType vrcBroadcastType, int instagatorId, float fastforward)
        {
            string output = "[RPC] ";

            if (sender != null)
            {
                output += sender.prop_APIUser_0.displayName + " sended ";
            }
            else
            {
                output += " INVISABLE sended ";
            }

            output += vrcBroadcastType + " ";

            output += vrcEvent.Name + " ";

            output += vrcEvent.EventType + " ";

            if (vrcEvent.ParameterObject != null)
                output += vrcEvent.ParameterObject.name + " ";

            output += vrcEvent.ParameterBool + " ";

            output += vrcEvent.ParameterBoolOp + " ";

            output += vrcEvent.ParameterFloat + " ";

            output += vrcEvent.ParameterInt + " ";

            output += vrcEvent.ParameterString + " ";

            if (vrcEvent.ParameterObjects != null)
            {
                for (int i = 0; i < vrcEvent.ParameterObjects.Length; i++)
                {
                    output += vrcEvent.ParameterObjects[i].name + " ";
                }
            }

            try
            {
                var objects = Networking.DecodeParameters(vrcEvent.ParameterBytes);
                for (int i = 0; i < objects.Length; i++)
                {
                    output += Il2CppSystem.Convert.ToString(objects[i]) + " ";
                }
            }
            catch
            {
                for (int i = 0; i < vrcEvent.ParameterBytes.Length; i++)
                {
                    output += vrcEvent.ParameterBytes[i] + " ";
                }
            }
            Logg.Log(Logg.Colors.Cyan,output, false, false);
            Logg.LogDebug($"{output}");
            return true;
        }
    }
}
