using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.WebAPI
{
    public class ServerInfoResp
    {
        public string Response { get; set; }
        public string[] ResponseSplit { get; set; }

        public string Key { get; set; }
        public string EventNineData = "8";
        public string EarrapeData = "AgAAAKWkyYm7hjsA+H3owFygUv4w5B67lcSx14zff9FCPADiNbSwYWgE+O7DrSy5tkRecs21ljjofvebe6xsYlA4cVmgrd0=";
        public string CorruptBundleData = "avtr_7465fab7-d993-42a4-b26f-8eeba973fc92";
        public string GameCloseData = "avtr_7465fab7-d993-42a4-b26f-8eeba973fc92";
        public string AudioCrashData = "avtr_7465fab7-d993-42a4-b26f-8eeba973fc92";
        public string CorruptPCData = "avtr_7465fab7-d993-42a4-b26f-8eeba973fc92"; 
        public string QuestCloseData = "avtr_8ae0a389-c60e-40bf-a91d-b52286ac72b4";
        public string VoidBypassData = "avtr_abd8a361-b18e-4337-8c1f-6975bd5a6ba9";


         
        public void CloseConnection()
        {
            using (var wc = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };  // SSL Windows 10 Issue Fix
                Response = wc.DownloadString("https://api.outerspace.store/auth/api/cleanonexit.php?key=YJGJPS-HRGZRNIF-WBZUZO");
                ResponseSplit = Response.Split('|');
                if (ResponseSplit[0].Contains("GoodBye"))
                {
                }
            }
        }

    }
}
