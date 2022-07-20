using System;
using UnityEngine;
using MelonLoader;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

using Trinity.SDK;
using Trinity.Events;
using Trinity.Utilities;
using Trinity.SDK.ButtonAPI;


namespace Trinity.Module.Settings.Render
{
    internal class PlayerList : BaseModule, OnPlayerJoinEvent, OnPlayerLeaveEvent
    {
        QMLable playerList;
        public PlayerList() : base("PlayerList", "PlayerList on the side", Main.Instance.SettingsButtonrender, null, true, true)
        {
        }

        public override void OnEnable()
        {

            try
            { 
                MenuUI.Log("DEBUG: <color=green>Player Room List On</color>");
                playerList.lable.SetActive(true);
                playerList.text.alignment = TMPro.TextAlignmentOptions.Right;

                if (UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer").gameObject.activeSelf)
                    playerList.lable.transform.localPosition = new Vector3(-526.6402f, -341.6801f, 0);

                else
                    playerList.lable.transform.localPosition = new Vector3(-106.6402f, -341.6801f, 0);
                playerList.text.color = Color.white;
                Main.Instance.OnPlayerJoinEvents.Add(this);
                Main.Instance.OnPlayerLeaveEvents.Add(this);
                MelonCoroutines.Start(OnUpdate());
            }
            catch (ArgumentNullException ERROR)
            {

            }
            catch (Exception ex)
            {

            }
           
        }

        public override void OnDisable()
        {

            try
            {
                MenuUI.Log("DEBUG: <color=red>Player Room List Off</color>");
                MelonCoroutines.Stop(OnUpdate());
                playerList.lable.SetActive(false);
                Main.Instance.OnPlayerJoinEvents.Remove(this);
                Main.Instance.OnPlayerLeaveEvents.Remove(this);
            }
            catch (ArgumentNullException ERROR)
            {
            }
            catch (Exception ERROR)
            {

            }

        }

        public override void OnUIInit()
        {
            playerList = new QMLable(UIU.UserInterface.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Left"), -207.1015f, -2.14f, "PlayerList");
            base.OnUIInit();
        }
        
        public IEnumerator OnUpdate()
        {
            while (this.toggled)
            {
                try
                {
                    string info = "";         
                    for (int i = 0; i < PU.GetAllPlayers().Length; i++)
                    {
                        VRC.Player player = PU.GetAllPlayers()[i]; 
                        if (player.GetIsMaster() == true)
                        {
                            info += " [<color=#FFB300>H</color>]";
                        }
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

        public void OnPlayerEnteredRoom(Photon.Realtime.Player player)
        {
            throw new System.NotImplementedException();
        }
    }
} 
