using Trinity.Utilities;
using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Trinity.SDK
{
    public class Config
    {
        private static string XpPfprL5Fn = string.Empty;
        private static string t5afLIB1A2 = string.Empty;
        public static Config Instance;
        public Config()
        {
         
        }
        internal void SaveConfig()
        {
            string value = JsonConvert.SerializeObject(this, (Formatting)1);
            File.WriteAllText("Trinity/Config2.json", value);
        }
        internal static Config Load()
        {
            //string fileName = Process.GetCurrentProcess().MainModule.FileName;
            //int length = fileName.LastIndexOf('\\');
            //Config.XpPfprL5Fn = fileName.Substring(0, length) + "\\" + Config.bJmcmpUa48() + "\\";
            //Config.t5afLIB1A2 = Config.XpPfprL5Fn + "\\Config\\";

            bool exist = !File.Exists("Trinity/Config2.json");
            Config contentresult;
            if (exist)
            {
                contentresult = new Config();
            }
            else
            {
                Config.Instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Trinity/Config2.json"));
                contentresult = Config.Instance;
            }
            return contentresult;
        }
        public int getConfigInt(string key, int defaultVal)
        {
         
            if (File.ReadAllText("Trinity/Config.json").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("Trinity/Config.json");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        return int.Parse(arrLine[i].Split('=')[1]);
                    }
                }
                return 0;
            }
            else
            {

                File.AppendAllText("Trinity/Config.json", "\n" + key + "=" + defaultVal);
                return defaultVal;
            }
        }

        public void setConfigBool(string key, bool state)
        {
            string[] arrLine = File.ReadAllLines("Trinity/Config.json");
            for (int i = 0; i < arrLine.Length; i++)
            {
                if (arrLine[i].Contains(key))
                {
                    arrLine[i] = key + "=" + state;
                    break;
                }
            }
            File.WriteAllLines("Trinity/Config.json", arrLine);
        }

        public bool getConfigBool(string key)
        {
            if (File.ReadAllText("Trinity/Config2.json").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("Trinity/Config2.json");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        if (arrLine[i].Split('=')[1] == "True")
                            return true;
                        else
                            return false;
                    }
                }

                return false;
            }
            else
            {
               
                File.AppendAllText("Trinity/Config.json", "\n" + key + "=False");
                return false;
            }
        }

        public Color getConfigColor(string key, Color defaultVal)
        {
            if (File.ReadAllText("Trinity/Config.json").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("Trinity/Config.json");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        string[] rgb = arrLine[i].Split('=')[1].Split(',');
                        try
                        {
                            return new Color(float.Parse(rgb[0]), float.Parse(rgb[1]), float.Parse(rgb[2]), float.Parse(rgb[3]));
                        }
                        catch
                        {
                            MelonLoader.MelonLogger.Msg(ConsoleColor.Red, "[Config] [Error] colors not saved as nummbers");
                            return defaultVal;
                        }

                    }
                }
                return defaultVal;
            }
            else
            {
                File.AppendAllText("Trinity/Config.json", "\n" + key + "=" + defaultVal.r + "," + defaultVal.g + "," + defaultVal.b + "," + defaultVal.a);
                MelonLoader.MelonLogger.Msg("[Config] created color " + key);
                return defaultVal;
            }
        }

        private string[] meshList = new string[]
        {
            "125k",
            "medusa",
            "inside",
            "outside",
            "mill"
        };
        private string[] shaderList = new string[]
        {
            "dbtc",
            "crash",
            "nigger",
            "nigga",
            "n1gga",
            "n1gg@",
            "nigg@",
            "go home",
            "byebye",
            "distance based",
            "tess",
            "tessellation",
            "cr4sh",
            "die",
            "get fucked",
            "spryth",
            "hidden/",
            ".hidden/",
            "nigg",
            "distancebased",
            "fuck:screen:fuckery:fuckyou:vilar",
            "bluescreen",
            "butterfly:vrchat:particle",
            "bluescream",
            "custom/hyu",
            "ebola",
            "yeet",
            "kill:xiexe",
            "lag ",
            "/die",
            " die ",
            "tessel:fur:poiyomi:standard:noise",
            "thot",
            "eatingmy",
            "undetected",
            "retard",
            "retrd",
            "standard on",
            "kyuzu",
            "oof ",
            ".Star/Bacon",
            ".Woofaa/Medusa",
            "Custom/Custom",
            "DistanceBased",
            "Knuckles_Gang/Free Crash",
            "Kyran/E  G  G",
            "Medusa Crash/Standard",
            "onion",
            "Pretty",
            "Sprythu/Why is my screen black",
            "custom/oof",
            "kys",
            "kos",
            "??",
            "yeeet",
            "got em",
            "medusa",
            "nigs",
            "sfere",
            " rip ",
            "/rip:/ripple",
            "sanity",
            "school",
            "shooter",
            "bacon",
            ".star:metal",
            "umbrella",
            "_bpu",
            "clap",
            "cooch:mochie",
            "sprythu",
            "bpu",
            "atençao",
            "izzyfrag",
            "izzy",
            "fragm",
            "shinigami:vhs:eye:vision",
            "clap shader",
            "clapped",
            "clapper",
            " clap ",
            "/clap",
            "world clap",
            "killer",
            ".blaze",
            "gang:troll:doppel",
            "makochill",
            "dead:sins",
            "death",
            "coffin",
            "onion",
            "suspicious",
            "darkwing",
            "keylime",
            "efrag",
            "yfrag",
            "brr",
            "temmie",
            "basically standard",
            "rampag",
            "reap",
            "uber shader 1.2",
            "C4",
            "2edgy",
            "lag:plague",
            "thotkyuzu",
            "loops",
            "overwatch:shader",
            "slay",
            "90hz:glasses",
            "autism",
            "penis",
            "randomname",
            "careful",
            "hurts",
            "truepink",
            "aнти",
            "Уфф",
            "рендер",
            "Это",
            "Ñoño",
            "nuke:almgp",
            "login",
            "go home",
           "ban:band",
           "buddy",
           "üõõüõ",
           "卍",
           "no sharing",
           "luka/megaae10",
           ".NEK0/Screen/Radial Blurr Zoom",
            "DocMe/BeautifulShaderv0.21",
            "Huyami/Ultrashader",
            "Leviant's Shaders/ScreenSpace Ubershader v2.7",
            "Leviant's Shaders/ScreenSpace Ubershader v2.7.3",
            "Leviant's Shaders/UberShader v2.9",
            "Magic3000/ScreenSpace",
            "Magic3000/ScreenSpacePub",
            "Magic3000/RGB-Glitch",
            "NEK0/Screen/Fade Screen v1",
            "VoidyShaders/Cave"
        }; 
        public bool GB_FeetColliders = true;
        public bool GB_HandColliders = true;
        public bool GB_HipBones = true;
        public bool GB_ChestBones = true;
        public bool GB_HeadBones = true;
        public bool GB_Friends = true;
        public bool GB_Spine = false;
        public bool GB_line = false;
        public static bool ItemOrbit = false;
        public static bool LowBotFPS = true;
        public static List<DynamicBone> currentWorldDynamicBones = new List<DynamicBone>();
        public static List<DynamicBoneCollider> currentWorldDynamicBoneColliders = new List<DynamicBoneCollider>();
        public static bool itemOrbit;
        public static bool SpoofPing;
        public static bool SpoofFps;
        public static float FPSSpoof = 144f;
        public static float FPSSpoof1 = 144f;
        public static float FPSSpoof2 = 144f;
        public static float FPSSpoof3 = 144f;
        public static float FPSSpoof4 = 144f;
        public static float PingSpoof = 2f;
        public static float PingSpoof1 = 2f;
        public static float PingSpoof2 = 2f;
        public static float PingSpoof3 = 2f;
        public static float PingSpoof4 = 2f;
        internal static bool AntiE1;

        public static bool Munchen { get; internal set; }
    }
}