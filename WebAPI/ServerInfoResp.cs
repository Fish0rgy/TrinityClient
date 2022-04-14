using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity.WebAPI
{
    public class ServerInfoResp
    {
        public string Key { get; set; }
        public string EventNineData { get; set; }
        public string EarrapeData { get; set; }
        public string CorruptBundleData { get; set; }
        public string GameCloseData { get; set; }
        public string AudioCrashData { get; set; }
        public string CorruptPCData { get; set; }
        public string QuestCloseData { get; set; }
        public string VoidBypassData { get; set; }
    }
}
