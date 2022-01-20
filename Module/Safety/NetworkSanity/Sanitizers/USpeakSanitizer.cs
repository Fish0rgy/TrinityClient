using System;
using System.Linq;
using Area51.Events;
using Area51.Module.Safety.Photon.NetworkSanity.Core;
using ExitGames.Client.Photon;
using MoPhoGames.USpeak.Core;
using Photon.Realtime;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib.XrefScans;

namespace Area51.Module.Safety.Photon.Sanitizers
{
    class USpeakSanitizer : ISanitizer
    {
        public static readonly RateLimiter _rateLimiter = new RateLimiter();

        public delegate int LoadFrameDelegate(USpeakFrameContainer container, Il2CppStructArray<byte> source, int sourceOffset);
        public readonly LoadFrameDelegate _loadFrame;
        public USpeakSanitizer()
        {
            _loadFrame = (LoadFrameDelegate)Delegate.CreateDelegate(typeof(LoadFrameDelegate), typeof(USpeakFrameContainer).GetMethods().Single(x =>
            {
                if (!x.Name.StartsWith("Method_Public_Int32_ArrayOf_Byte_Int32_") || x.Name.Contains("_PDM_"))
                    return false;

                return XrefScanner.XrefScan(x).Count(y => y.Type == XrefType.Method && y.TryResolve() != null) == 4;
            }));
        }
        public bool OnPhotonEvent(LoadBalancingClient loadBalancingClient, EventData eventData)
        {
            return eventData.Code == 1 && IsVoicePacketBad(eventData);
        }
        public bool VRCNetworkingClientOnPhotonEvent(EventData eventData)
        {
            return eventData.Code == 1 && _rateLimiter.IsRateLimited(eventData.Sender);
        }

        public bool IsVoicePacketBad(EventData eventData)
        {
            if (_rateLimiter.IsRateLimited(eventData.Sender))
                return true;

            byte[] bytes = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(eventData.CustomData.Pointer);
            if (bytes.Length <= 8)
            {
                _rateLimiter.BlacklistUser(eventData.Sender);
                return true;
            }

            var sender = BitConverter.ToInt32(bytes, 0);
            if (sender != eventData.Sender)
            {
                _rateLimiter.BlacklistUser(eventData.Sender);
                return true;
            }

            var sourceOffset = 4;
            var source = bytes.Skip(4).ToArray();
            while (sourceOffset < source.Length)
            {
                var container = new USpeakFrameContainer();
                var offset = _loadFrame(container, source, sourceOffset);
                if (offset == -1)
                {
                    _rateLimiter.BlacklistUser(eventData.Sender);
                    return true;
                }

                container.Method_Public_Void_0();
                sourceOffset += offset;
            }

            return false;
        }
    }
}
