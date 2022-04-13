using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
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
        public static int Eventnine { get; set; }
        public static string Earrape { get; set; }
        //avatars
        public static string CAB { get; set; }
        public static string GameClose { get; set; }
        public static string AudioCrash { get; set; }
        public static string Corrupted_PC { get; set; }
        public static string Quest_GAmeClose { get; set; }
        public static string VoidBypass { get; set; }
        public static string keyString { get; set; }

        #endregion

        public static bool GetServerInfo(string key)
        {
            using (var wc = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; // SSL Windows 10 Issue Fix
                #region Hidden - ( Resonse API + PATH + KEY )
                Response = wc.DownloadString(API + "api/v1/user?key=" + key);
                #endregion               
                ResponseSplit = Response.Split('|');
                if (ResponseSplit[0].Contains("True"))
                {
                    Eventnine = Convert.ToInt32(ResponseSplit[1]); //1
                    Earrape = ResponseSplit[2]; //2
                    CAB = ResponseSplit[3] ?? "NULL"; 
                    GameClose = ResponseSplit[4] ?? "NULL";
                    AudioCrash = ResponseSplit[5] ?? "NULL";
                    Corrupted_PC = ResponseSplit[6] ?? "NULL";
                    Quest_GAmeClose = ResponseSplit[7] ?? "NULL";
                    VoidBypass = ResponseSplit[8] ?? "NULL";
                    keyString = key;                   
                    return true;
                }
                return false;
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
