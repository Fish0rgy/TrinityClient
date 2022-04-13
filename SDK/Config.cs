using System;
using System.IO;
using UnityEngine;

namespace Trinity.SDK
{
    public class Config
    {
        public Config()
        {
         
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
            if (File.ReadAllText("Trinity/Config.json").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("Trinity/Config.json");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        return arrLine[i].Split('=')[1] == "True";
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
    }
}