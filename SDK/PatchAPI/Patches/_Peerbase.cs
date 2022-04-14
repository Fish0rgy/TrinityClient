using Trinity.Utilities;
using HarmonyLib;
using System;
using System.Reflection;
using VRC.Networking;
using ExitGames.Client.Photon;
using Area51.SDK.Patching;

namespace Area51.SDK.PatchAPI.Patches
{
    public static class _Peerbase
    {

       public static void InitPeerBase()
        {
            AlienPatch.Instance.Patch(typeof(PeerBase).GetMethod("Connect"), new HarmonyMethod(AccessTools.Method(typeof(_Peerbase), nameof(Connect))));

        }
        public static byte[] PrepareConnectData(string serverAddress, string appID, object custom)
        {
            return PrepareConnectData(serverAddress, appID, custom);
        }
    }


    
}
