using Area51.SDK;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRC;

namespace Area51.Module.World.World_Hacks.Just_B
{
    class JustBMisc
    {
        public static void SetButtonText(string btntext) => GameObject.Find("Lobby/New Part/Udon/Spawn Settings/Buttons/Own Flair - BlueButtonWide/Start/Text (TMP)").gameObject.GetComponent<TMPro.TextMeshPro>().text = btntext;
        public static IEnumerator spoofVIP()
        {
            GameObject[] gameobjects = Resources.FindObjectsOfTypeAll<GameObject>();
            for (int i = 0; i < gameobjects.Length; i++)
            {

                if (gameobjects[i].gameObject.name.Contains($"Own Flair - BlueButtonWide"))
                {
                    SetButtonText("Enable Ellte");
                    gameobjects[i].gameObject.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
        public static void ForceJoin(int roomNum)
        {
            GameObject[] gameobjects = Resources.FindObjectsOfTypeAll<GameObject>();
            for (int i = 0; i < gameobjects.Length; i++)
            {
                switch (roomNum)
                {
                    case 1:
                        if (gameobjects[i].gameObject.name.Contains($"Bedroom {roomNum}"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(-223.5937f, -11.2542f, 150.3761f);
                        }
                        break;
                    case 2:
                        if (gameobjects[i].gameObject.name.Contains($"Bedroom {roomNum}"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(-211.4728f, 55.7458f, -90.8525f);
                        }
                        break;
                    case 3:
                        if (gameobjects[i].gameObject.name.Contains($"Bedroom {roomNum}"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(-124.5334f, -11.2542f, 149.8945f);
                        }
                        break;
                    case 4:
                        if (gameobjects[i].gameObject.name.Contains($"Bedroom {roomNum}"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(-111.3594f, 55.7385f, -90.4229f);
                        }
                        break;
                    case 5:
                        if (gameobjects[i].gameObject.name.Contains($"Bedroom {roomNum}"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(-24.5131f, -11.2542f, 149.9625f);
                        }
                        break;
                    case 6:
                        if (gameobjects[i].gameObject.name.Contains($"Bedroom {roomNum}"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(-11.2326f, 55.7458f, -90.5274f);
                        }
                        break;
                    case 7: //vip
                        if (gameobjects[i].gameObject.name.Contains("Bedroom VIP"))
                        {
                            gameobjects[i].gameObject.SetActive(true);
                            PlayerWrapper.TeleportLocation(56.8751f, 62.8633f, -0.0431f);
                        }
                        break;
                }
            }
        } 
    }
}
