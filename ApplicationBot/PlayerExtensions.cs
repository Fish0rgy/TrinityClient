
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;

namespace Trinity.Bot
{
	public static class PlayerExtensions
	{
		public static float DefaultGain = 1f;
		public static float MaxGain = float.MaxValue;
		private static VRC_EventHandler handler;
		public static VRCPlayer LocalVRCPlayer => VRCPlayer.field_Internal_Static_VRCPlayer_0; 
		public static Player LocalPlayer => Player.prop_Player_0; 
		public static APIUser LocalAPIUser => APIUser.CurrentUser; 
		public static string DisplayName(this Player Instance) => Instance.GetAPIUser().displayName; 
		public static USpeaker LocalUSpeaker => PlayerExtensions.LocalVRCPlayer.field_Private_USpeaker_0; 
		public static VRCPlayerApi LocalVRCPlayerAPI => PlayerExtensions.LocalVRCPlayer.field_Private_VRCPlayerApi_0; 
		public static PlayerManager PManager => PlayerManager.field_Private_Static_PlayerManager_0; 
		public static List<Player> AllPlayers => PlayerExtensions.PManager.field_Private_List_1_Player_0.ToArray().ToList<Player>(); 
		public static ApiAvatar GetAPIAvatar(this Player player) => player.prop_ApiAvatar_0; 
		public static PlayerNet GetPlayerNet(this Player player) => player._playerNet; 
		public static USpeaker GetUSpeaker(this Player player) => player.prop_USpeaker_0; 
		public static VRCPlayerApi GetVRCPlayerApi(this Player player) => player.field_Private_VRCPlayerApi_0; 
		public static APIUser GetAPIUser(this Player player) => player.prop_APIUser_0;
		public static bool IsQuest(this Player player) => player.GetAPIUser().IsOnMobile;
		public static GameObject InstantiatePrefab(string PrefabNAME, Vector3 position, Quaternion rotation) => Networking.Instantiate(0, PrefabNAME, position, rotation);
		public static void Mute(bool toggle) => USpeaker.field_Private_Static_Boolean_0 = toggle;
		public static void SetVolume(this Player player, float vol) => player.GetUSpeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0 = vol;
		public static float GetVolume(this Player player) => player.GetUSpeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0;
		public static bool IsLocalMuted(this Player player) => player.GetVolume() == 0f;
		public static void LocalMute(this Player player) => player.SetVolume(0f);
		public static void LocalUnMute(this Player player) => player.SetVolume(1f);
		public static bool IsInVR(this Player player) => player.GetVRCPlayerApi().IsUserInVR();
		public static void Teleport(this Player player) => PlayerExtensions.LocalVRCPlayer.transform.position = player.GetVRCPlayer().transform.position;
		public static void ReloadAvatar(this Player Instance) => VRCPlayer.Method_Public_Static_Void_APIUser_0(Instance.GetAPIUser());
		public static QuickMenu GetQuickMenu() => GameObject.Find("UserInterface/QuickMenu").GetComponent<QuickMenu>();
		public static Player GetSelectedPlayer() => PlayerExtensions.GetQuickMenu().field_Private_Player_0;
		public static void ReloadAllAvatars() => PlayerExtensions.LocalVRCPlayer.Method_Public_Void_Boolean_0(false);
		public static VRCPlayer GetVRCPlayer(this Player player) => player.prop_VRCPlayer_1;
		public static VRCAvatarManager GetVRCAvatarManager(this VRCPlayer player) => player.field_Private_VRCAvatarManager_0;
		public static void SetLocalPlayerWalkSpeed(float speed) => PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetWalkSpeed(speed);
		public static void SetLocalPlayerWalkSpeed() => PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetWalkSpeed(0f);
		public static void SetLocalPlayerRunSpeed(float speed) => PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetRunSpeed(speed);
		public static void SetLocalPlayerRunSpeed() => PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetRunSpeed(0f);
		public static void SetLocalPlayerStrafeSpeed(float speed) => PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetStrafeSpeed(speed);
		public static void SetLocalPlayerStrafeSpeed() => PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetStrafeSpeed(0f);
		public static string GetName(this Player player) => player.GetAPIUser().displayName;
		public static bool IsMaster(this Player player) => player.GetVRCPlayerApi().isMaster;
		public static int GetActorNumber(this Player player) => player.GetVRCPlayerApi().playerId;
		public static int GetPlayerPing(this Player player) => (int)player.GetPlayerNet().field_Private_Int16_0;
		public static void SetGain(float Gain) => PlayerExtensions.LocalGain = Gain;
		public static void ResetGain() => USpeaker.field_Internal_Static_Single_1 = PlayerExtensions.DefaultGain;
		public static bool IsInWorld() => RoomManager.field_Internal_Static_ApiWorld_0 != null;
		public static Player[] GetAllPlayers() => PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
		public static string GetSteamID(this VRCPlayer player) => player.field_Private_UInt64_0.ToString();
		public static Player GetPlayer(this VRCPlayer player) => player._player;
		public static bool IsLocalPlayer(this Player player) => player.GetAPIUser().id == APIUser.CurrentUser.id;
		public static void ToggleBlock(this Player player)
		{
			PageUserInfo pageUserInfo = player.GetPageUserInfo();
			if (!player.IsLocalPlayer())
			{
				pageUserInfo.ToggleBlock();
			}
		}
		public static void ToggleMute(this Player player)
		{
			PageUserInfo pageUserInfo = player.GetPageUserInfo();
			if (!player.IsLocalPlayer())
			{
				pageUserInfo.ToggleMute();
			}
		}
		private static PageUserInfo GetPageUserInfo(this Player player)
		{
			PageUserInfo component = GameObject.Find("Screens").transform.Find("UserInfo").GetComponent<PageUserInfo>();
			component.field_Private_APIUser_0 = new APIUser
			{
				id = player.GetAPIUser().id
			};
			return component;
		}
		public static void SendVRCEvent(VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType type, GameObject instagator)
		{
			if (PlayerExtensions.handler == null)
			{
				PlayerExtensions.handler = Resources.FindObjectsOfTypeAll<VRC_EventHandler>().FirstOrDefault<VRC_EventHandler>();
			}
			vrcEvent.ParameterObject = Player.prop_Player_0.prop_USpeaker_0.gameObject;
			PlayerExtensions.handler.TriggerEvent(vrcEvent, type, instagator, 0f);
		}
		 
		public static float LocalGain
		{
			get
			{
				return USpeaker.field_Internal_Static_Single_1;
			}
			set
			{
				USpeaker.field_Internal_Static_Single_1 = value;
			}
		}
		public static float AllGain
		{
			get
			{
				return USpeaker.field_Internal_Static_Single_0;
			}
			set
			{
				USpeaker.field_Internal_Static_Single_0 = value;
			}
		} 
		 
		public static int GetPlayerFrames(this Player player)
		{
			if (player.GetPlayerNet().field_Private_Byte_0 == 0)
			{
				return 0;
			}
			return (int)(1000f / (float)player.GetPlayerNet().field_Private_Byte_0);
		} 
		public static void ChangeAvatar(string AvatarID)
		{
			new PageAvatar
			{
				field_Public_SimpleAvatarPedestal_0 = new SimpleAvatarPedestal
				{
					field_Internal_ApiAvatar_0 = new ApiAvatar
					{
						id = AvatarID
					}
				}
			}.ChangeToSelectedAvatar();
		}
		 
		public static Player GetPlayer(int ActorNumber)
		{
			return (from p in PlayerExtensions.AllPlayers
					where p.GetActorNumber() == ActorNumber
					select p).FirstOrDefault<Player>();
		} 
		public static Player GetPlayer(string Displayname)
		{
			return (from p in PlayerExtensions.AllPlayers
					where p.GetName() == Displayname
					select p).FirstOrDefault<Player>();
		} 
		public static Player GetPlayerByID(string UserID)
		{
			return (from p in PlayerExtensions.AllPlayers
					where p.GetAPIUser().id == UserID
					select p).FirstOrDefault<Player>();
		}
		public static IEnumerator PlayFromURL(string url)
		{
			AudioClip AudioClip = new AudioClip();
			WWW www = WWW.LoadFromCacheOrDownload(url, 0);
			yield return www;
			AudioClip = www.GetAudioClip();
			AudioSource source = CreateAudioSource(AudioClip, LocalPlayer.gameObject);;
			source.Play();
            UnityEngine.Object.Destroy(source, AudioClip.length);
			yield break;
		}
		public static AudioSource CreateAudioSource(AudioClip audio, GameObject obj)
		{
			AudioSource Source = obj.AddComponent<AudioSource>();
			Source.clip = audio;
			Source.spatialize = false;
			Source.volume = 1f;
			Source.loop = false;
			Source.playOnAwake = false;
			Source.outputAudioMixerGroup = VRCAudioManager.field_Private_Static_VRCAudioManager_0.field_Public_AudioMixerGroup_0;
			return Source;
		}
		public static void CheckBotStatus()
        {
			if (Environment.GetCommandLineArgs().Contains("--trinity-bot"))
			{
				Console.Clear();
				Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Yellow, @"
                ████████╗██████╗░██╗███╗░░██╗██╗████████╗██╗░░░██╗  ██████╗░░█████╗░████████╗
                ╚══██╔══╝██╔══██╗██║████╗░██║██║╚══██╔══╝╚██╗░██╔╝  ██╔══██╗██╔══██╗╚══██╔══╝
                ░░░██║░░░██████╔╝██║██╔██╗██║██║░░░██║░░░░╚████╔╝░  ██████╦╝██║░░██║░░░██║░░░
                ░░░██║░░░██╔══██╗██║██║╚████║██║░░░██║░░░░░╚██╔╝░░  ██╔══██╗██║░░██║░░░██║░░░
                ░░░██║░░░██║░░██║██║██║░╚███║██║░░░██║░░░░░░██║░░░  ██████╦╝╚█████╔╝░░░██║░░░
                ░░░╚═╝░░░╚═╝░░╚═╝╚═╝╚═╝░░╚══╝╚═╝░░░╚═╝░░░░░░╚═╝░░░  ╚═════╝░░╚════╝░░░░╚═╝░░░
");
				Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Bot Client Started!");
				Main.IsApplicationBot = true;
				Main.wse.Connect();
				Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.Green, "Connected to Server!");
				Main.wse.OnMessage += delegate (object s, WebSocketSharp.MessageEventArgs e)
				{
					Trinity.SDK.LogHandler.Log(Trinity.SDK.LogHandler.Colors.DarkBlue, "[Bot Client] Received " + e.Data.ToString());
					int num = e.Data.ToString().IndexOf(" ");
					string CMD = e.Data.ToString().Substring(0, num);
					string Parameters = e.Data.ToString().Substring(num + 1);
					Main.HandleActionOnMainThread(delegate
					{
						Trinity.Bot.Commands.Commands.Cmd[CMD](Parameters);
					});
				};
				return;
			}
		}
	}
}
