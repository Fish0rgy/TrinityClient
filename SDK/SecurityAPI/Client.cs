using Trinity.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Trinity.WebAPI;
using VRC.Core;

namespace Trinity.SDK.Security
{
    class SecurityCheck
    {
        /// <summary>
        /// // Project Name: API
        /// // Description: API  For Trinity-Client {Server AUTH: api.outerspace.store}
        /// // Project Developer's: Josh(UrFingPoor), Fish(Fish0rgy)
        /// </summary>
        /// 
        #region Public declarations  - Public variables that will be used acrossed the project
        public static string Response { get; set; }
        public static string[] ResponseSplit { get; set; }
        public static string key = $"{AppDomain.CurrentDomain.BaseDirectory}\\Trinity\\Auth.json";
        public static readonly string ClientFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\Trinity";
        public static readonly string API = "https://api.basementgames.us/";
        //photon
        public static ServerInfoResp ExploitData { get; set; }

        #endregion

        public static bool GetServerInfo(string key)
        {
            using (var wc = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; // SSL Windows 10 Issue Fix
                #region Hidden - ( Resonse API + PATH + KEY )
                Response = wc.DownloadString(API + "api/v1/serverinfo?key=" + key);
                #endregion               

                if (Response.Contains("451")) return false;

                ExploitData = JsonConvert.DeserializeObject<ServerInfoResp>(Response);

                return true;
            }
        }

        public static bool CleanOnExit(string key)
        {
            using (var wc = new WebClient())
            {
                /*
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };  // SSL Windows 10 Issue Fix
                #region Hidden - ( Resonse API + PATH + KEY )
                Response = wc.DownloadString(API + "api/v1/logout?key=" + key);

                #endregion
                ResponseSplit = Response.Split('|');
                if (ResponseSplit[0].Contains("GoodBye"))
                {               
                    return true;
                }
                */
                return true;
            }
        }

    }
}
