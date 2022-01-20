using Area51.Events;
using Area51.SDK;
using Area51.SDK.ButtonAPI;
using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Area51.Module.Settings.Render
{
    internal class PlayerList : BaseModule, OnPlayerJoinEvent, OnPlayerLeaveEvent
    {
        QMLable playerList;
        public PlayerList() : base("PlayerList", "PlayerList on the side", Main.Instance.SettingsButtonrender, null, true, true)
        {
        }

        public override void OnEnable()
        {
            playerList.lable.SetActive(true);
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            if (GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer").activeSelf)
                playerList.lable.transform.localPosition = new Vector3(-526.6402f, -341.6801f, 0);
            else
                playerList.lable.transform.localPosition = new Vector3(-106.6402f, -341.6801f, 0);
            playerList.text.color = Color.white;
            Main.Instance.OnPlayerJoinEvents.Add(this);
            Main.Instance.OnPlayerLeaveEvents.Add(this);
            MelonCoroutines.Start(OnUpdate());
        }

        public override void OnDisable()
        {
            playerList.lable.SetActive(false);
            Main.Instance.OnPlayerJoinEvents.Remove(this);
            Main.Instance.OnPlayerLeaveEvents.Remove(this);
            MelonCoroutines.Stop(OnUpdate());
        }

        public override void OnUIInit()
        {
            playerList = new QMLable(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Left").transform, -207.1015f, -2.14f, "PlayerList");
            base.OnUIInit();
        }

        public IEnumerator OnUpdate()
        {
            while (this.toggled)
            {
                try
                {
                    string info = "";

                    for (int i = 0; i < PlayerWrapper.GetAllPlayers().Length; i++)
                    {
                        VRC.Player player = PlayerWrapper.GetAllPlayers()[i];
                        if (player.GetIsMaster())
                            info += "[<color=yellow>H</color>]";
                       
                        info += " [" + player.GetPlatform() + "]";                      
                        info += " [<color=#FFB300>P</color>] " + player.GetPingColord();
                        info += " [<color=#FFB300>F</color>] " + player.GetFramesColord();                       
                        info += " <color=#" + ColorUtility.ToHtmlStringRGB(player.GetTrustColor()) + ">" + player.GetAPIUser().displayName + "</color>\n";
                    }
                    playerList.text.text = info;
                }
                catch { }
                yield return new WaitForSeconds(0.25f);
            }
            yield break;
        }

        public void OnPlayerJoin(VRC.Player player)
        {
            playerList.text.enableWordWrapping = false;
            playerList.text.fontSizeMin = 30;
            playerList.text.fontSizeMax = 30;
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            playerList.text.color = Color.white;
        }

        public void PlayerLeave(VRC.Player player)
        {
            playerList.text.enableWordWrapping = false;
            playerList.text.fontSizeMin = 30;
            playerList.text.fontSizeMax = 30;
            playerList.text.alignment = TMPro.TextAlignmentOptions.Right;
            playerList.text.color = Color.white;
        }
    }
} 
