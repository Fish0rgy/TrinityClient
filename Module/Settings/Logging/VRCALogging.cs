using Trinity.Events;
using Trinity.SDK;
using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using VRC;

namespace Trinity.Module.Settings.Logging
{
    class VCRALogger : BaseModule, OnPlayerJoinEvent
    {

        public VCRALogger() : base("VRCA Logger", "[Used For Reuploading] Logs VRCA's", Main.Instance.SettingsButtonLoggging, null, true, true)
        {
        }

        public override void OnEnable()
        {
            Main.Instance.OnPlayerJoinEvents.Add(this);         
        }

        public override void OnDisable()
        {
            Main.Instance.OnPlayerJoinEvents.Remove(this);           
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"Username: {player.prop_VRCPlayerApi_0.displayName}\nAvatarID: {player.prop_ApiAvatar_0.id}", true, false);
            SDK.LogHandler.LogDebug($"[Avatar Logger] User: {player.prop_VRCPlayerApi_0.displayName}");
            Task.Run(() =>
            {
                MelonLoader.MelonCoroutines.Start(LogVRCA(player));
            });
        }

        public IEnumerator LogVRCA(VRC.Player player)
        {
            using (var wc = new WebClient { Headers = { "User-Agent: Other" } })
            {
                wc.DownloadFileAsync(new Uri(player.prop_ApiAvatar_0.assetUrl), "Trinity/VRCA/" + player.prop_ApiAvatar_0.name);
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Grey, $"Yeeted -> {player.prop_VRCPlayerApi_0.displayName}'s Avatar.", false, false);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}