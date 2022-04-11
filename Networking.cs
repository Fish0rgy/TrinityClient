using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Area51;
using System.Diagnostics;
using Area51.SDK;
namespace Area51
{
    public static class Patched_Vars
    {
        public static string PhotonIP = "null";
        public static SocketUdp UDPSocket = null;

        public static string ProxyServerAdress { get; internal set; }
        public static string ApplicationName { get; internal set; }
        public static string CustomConnectionObject { get; internal set; }
        public static TPeer TPeer { get; internal set; }
        public static SocketTcp TCPSocket { get; internal set; }  
        public static IPhotonSocket IPhotonSocket;
        public static IPhotonPeerListener IPhotonPeerListener;
        public static EnetPeer EnetPeer;
        public static EnetChannel EnetChannel;
    }


    internal class NetworkingS
    {
        internal class PhotonPatches
        {
            internal static void Start()
            {
               try
                {
                    new Astronyia_Patches(typeof(IPhotonSocket).GetMethod("Connect"), "IPhotonSocketConnect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(IPhotonPeerListener).GetMethod("OnEvent"), "IPhotonPeerListener_OnEvent", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(IPhotonPeerListener).GetMethod("OnOperationResponse"), "IPhotonPeerListener_OnOperationResponse", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(IPhotonPeerListener).GetMethod("OnStatusChanged"), "IPhotonPeerListener_OnStatusChanged", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(EnetPeer).GetMethod("Connect"), "EnetPeer_Connect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(EnetChannel).GetMethod("QueueIncomingReliableUnsequenced"), "EnetChannel_QueueIncomingReliableUnsequenced", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(TPeer).GetMethod("Connect"), "TPeerConnect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(PhotonPeer).GetMethod("Connect"), "PhotonPeerConnect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("Send"), "TunnelSend", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("Connect"), "TunnelConnect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("Disconnect"), "TunnelDisconnect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("Dispose"), "TunnelDispose", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("DnsAndConnect"), "TunnelDnsAndConnect", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("Finalize"), "TunnelFinalize", Astronyia_Patches.PatchType.postfix);
                    new Astronyia_Patches(typeof(SocketUdp).GetMethod("ReceiveLoop"), "TunnelReceiveLoop", Astronyia_Patches.PatchType.postfix);

                    Astronyia_Patches.PatchAll(delegate (Astronyia_Patches a, Exception e)
                    {
                        Logg.Log(Logg.Colors.Red, $"Failed Patching {a.Id} Reason: {e.Message}", false, false);
                    }, delegate (Astronyia_Patches a, Exception e)
                    {
                        Logg.Log(Logg.Colors.Green, $"SucccessFully Patched {a.Id}", false, false);
                    }).GetAwaiter().GetResult();

                    Console.WriteLine("[ForceKick] Patched!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("[ForceKick] Failed!");
                }
                
          
            }
        }


        internal class Patched_Methods
        {
            internal static void EnetChannel_QueueIncomingReliableUnsequenced(NCommand command, EnetChannel __instance)
            {
                try
                {
                    Console.WriteLine("EnetChannel EnetChannel_QueueIncomingReliableUnsequenced: " + Helper.TrySerialize(command));
                    Patched_Vars.EnetChannel = __instance;
                }
                catch (Exception e)
                {
                }
            }

            internal static void IPhotonSocketConnect(IPhotonSocket __instance)
            {
                try
                {
                   
                    Console.WriteLine("Connected IPhotonSocket");
                    Patched_Vars.IPhotonSocket = __instance;
                }
                catch (Exception e)
                {
                }
            }
            internal static void EnetPeer_Connect(EnetPeer __instance)
            {
                try
                {
                    Console.WriteLine("Connected EnetPeer Socket");
                    Patched_Vars.EnetPeer = __instance;
                }
                catch (Exception e)
                {
                }
            }
            internal static void IPhotonPeerListener_OnEvent(EventData eventData, IPhotonPeerListener __instance)
            {
                try
                {
                    Console.WriteLine("IPhotonPeerListener: " + Helper.TrySerialize(eventData));
                    Patched_Vars.IPhotonPeerListener = __instance;
                }
                catch (Exception e)
                {
                }
            }
            internal static void IPhotonPeerListener_OnOperationResponse(OperationResponse operationResponse, IPhotonPeerListener __instance)
            {
                try
                {
                    Console.WriteLine("IPhotonPeerListener: " + Helper.TrySerialize(operationResponse));
                    Patched_Vars.IPhotonPeerListener = __instance;
                }
                catch (Exception e)
                {
                }
            }
            internal static void IPhotonPeerListener_OnStatusChanged(StatusCode statusCode, IPhotonPeerListener __instance)
            {
                try
                {
                    Console.WriteLine("IPhotonPeerListener: " + statusCode);
                    Patched_Vars.IPhotonPeerListener = __instance;
                }
                catch (Exception e)
                {
                }
            }
            public static void PhotonPeerConnect(string serverAddress, string proxyServerAddress, string applicationName, Il2CppSystem.Object custom)
            {
                try
                {
                    Patched_Vars.PhotonIP = serverAddress ?? "null";
                    Patched_Vars.ProxyServerAdress = proxyServerAddress ?? "null";
                    Patched_Vars.ApplicationName = applicationName ?? "null";
                    Patched_Vars.CustomConnectionObject = Newtonsoft.Json.JsonConvert.SerializeObject(custom ?? "{\"null\": \"null\"}");
                }
                catch (Exception e)
                {
                }
            }

            public static void TunnelDisconnect(ref bool __result)
            {
                try
                {
                    Logg.Log(Logg.Colors.Red, $"{new StackTrace().GetFrame(-2).GetMethod().Name + "Called Disconnecting"}", false, false);
                    __result = Patched_Vars.TCPSocket.Disconnect();
                    if (!__result)
                    {
                        Logg.Log(Logg.Colors.Red, "Failed", false, false);
                    }
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
                }
            }
            public static bool TunnelReceiveLoop()
            {
                try
                {
                    Patched_Vars.TCPSocket.ReceiveLoop();
                    return false;
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, "Failed To Receive Loop UDP", false, false);
                    return true;
                }
            }
            public static bool TunnelConnect(SocketUdp __instance, ref bool __result)
            {
                try
                {
                    Patched_Vars.UDPSocket = __instance;
                    Patched_Vars.TCPSocket = new SocketTcp(Patched_Vars.UDPSocket.peerBase);
                    Logg.Log(Logg.Colors.Yellow, $"{Patched_Vars.TCPSocket.Pointer}", false, false);
                    __result = Patched_Vars.TCPSocket.Connect();
                    Logg.Log(Logg.Colors.Yellow, $"{new StackTrace().GetFrame(-2).GetMethod().Name + "Result: " + __result}", false, false);
                    if (!__result)
                    {
                        Logg.Log(Logg.Colors.Red, "Failed Connecting", false, false);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, "Failed Using UDP", false, false);
                    return true;
                }
            }
            public static bool TunnelFinalize()
            {
                try
                {
                    Logg.Log(Logg.Colors.Yellow, $"{new StackTrace().GetFrame(0).GetMethod().Name}", false, false);
                    Patched_Vars.TCPSocket.Finalize();
                    return false;
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, "Failed To Finalize UDP", false, false);
                    return true;
                }
            }
            public static bool TunnelDnsAndConnect()
            {
                try
                {
                    Logg.Log(Logg.Colors.Yellow, $"{new StackTrace().GetFrame(0).GetMethod().Name}", false, false);
                    Patched_Vars.TCPSocket.DnsAndConnect();
                    return false;
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, "Failed DNS AND CONNECT UDP", false, false);
                    return true;
                }
            }
            public static bool TunnelDispose()
            {
                try
                {
                    Logg.Log(Logg.Colors.Yellow, $"{new StackTrace().GetFrame(0).GetMethod().Name}", false, false);
                    Console.WriteLine(new StackTrace().GetFrame(-2).GetMethod().Name);
                    Patched_Vars.TCPSocket.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, "Failed To Dispose UDP", false, false);
                    return true;
                }
            }
            public static void TunnelSend(UnhollowerBaseLib.Il2CppStructArray<byte> data, int lenght, ref ExitGames.Client.Photon.PhotonSocketError __result, SocketUdp __instance)
            {
                try
                {
                    Logg.Log(Logg.Colors.Yellow, $"{new StackTrace().GetFrame(0).GetMethod().Name}", false, false);
                    Patched_Vars.UDPSocket = __instance;
                    var a = new byte[] { 92 };
                    Patched_Vars.UDPSocket.Send(a, a.Length);
                    if (__result != PhotonSocketError.Success)
                    {

                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    Logg.Log(Logg.Colors.Red, ex.ToString(), false, false);
                }
            }



            public static void TPeerConnect(string serverAddress, string proxyServerAddress, string appID, Il2CppSystem.Object customData, TPeer __instance)
            {
                try
                {
                    Patched_Vars.TPeer = __instance;
                    Console.WriteLine($"TPeer is Connecting to {serverAddress ?? "null"}");
                }
                catch (Exception e)
                {
                }
            }
            public class Helper
            {
                public static string TrySerialize(object value)
                {
                    try
                    {
                        if (value == null)
                        {
                            return "null";
                        }
                        return Newtonsoft.Json.JsonConvert.SerializeObject(value);
                    }
                    catch
                    {
                        return "Failed Serialize";
                    }
                }
            }
        }
    }
}
