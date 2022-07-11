
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;

namespace Trinity.Bot
{
	// Token: 0x0200003A RID: 58
	internal static class PlayerExtensions
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000096B1 File Offset: 0x000078B1
		public static VRCPlayer LocalVRCPlayer
		{
			get
			{
				return VRCPlayer.field_Internal_Static_VRCPlayer_0;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000096B8 File Offset: 0x000078B8
		public static Player LocalPlayer
		{
			get
			{
				return Player.prop_Player_0;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000096BF File Offset: 0x000078BF
		public static APIUser LocalAPIUser
		{
			get
			{
				return APIUser.CurrentUser;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000096C6 File Offset: 0x000078C6
		public static string DisplayName(this Player Instance)
		{
			return Instance.GetAPIUser().displayName;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000096D3 File Offset: 0x000078D3
		public static USpeaker LocalUSpeaker
		{
			get
			{
				return PlayerExtensions.LocalVRCPlayer.field_Private_USpeaker_0;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000096DF File Offset: 0x000078DF
		public static VRCPlayerApi LocalVRCPlayerAPI
		{
			get
			{
				return PlayerExtensions.LocalVRCPlayer.field_Private_VRCPlayerApi_0;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000096EB File Offset: 0x000078EB
		public static PlayerManager PManager
		{
			get
			{
				return PlayerManager.field_Private_Static_PlayerManager_0;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000096F2 File Offset: 0x000078F2
		public static List<Player> AllPlayers
		{
			get
			{
				return PlayerExtensions.PManager.field_Private_List_1_Player_0.ToArray().ToList<Player>();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009708 File Offset: 0x00007908
		public static ApiAvatar GetAPIAvatar(this Player player)
		{
			return player.prop_ApiAvatar_0;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009710 File Offset: 0x00007910
		public static PlayerNet GetPlayerNet(this Player player)
		{
			return player._playerNet;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009718 File Offset: 0x00007918
		public static USpeaker GetUSpeaker(this Player player)
		{
			return player.prop_USpeaker_0;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009720 File Offset: 0x00007920
		public static VRCPlayerApi GetVRCPlayerApi(this Player player)
		{
			return player.field_Private_VRCPlayerApi_0;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00009728 File Offset: 0x00007928
		public static APIUser GetAPIUser(this Player player)
		{
			return player.prop_APIUser_0;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009730 File Offset: 0x00007930
		public static bool IsQuest(this Player player)
		{
			return player.GetAPIUser().IsOnMobile;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009740 File Offset: 0x00007940
		public static void ToggleBlock(this Player player)
		{
			PageUserInfo pageUserInfo = player.GetPageUserInfo();
			if (!player.IsLocalPlayer())
			{
				pageUserInfo.ToggleBlock();
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00009764 File Offset: 0x00007964
		public static void ToggleMute(this Player player)
		{
			PageUserInfo pageUserInfo = player.GetPageUserInfo();
			if (!player.IsLocalPlayer())
			{
				pageUserInfo.ToggleMute();
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009788 File Offset: 0x00007988
		private static PageUserInfo GetPageUserInfo(this Player player)
		{
			PageUserInfo component = GameObject.Find("Screens").transform.Find("UserInfo").GetComponent<PageUserInfo>();
			component.field_Private_APIUser_0 = new APIUser
			{
				id = player.GetAPIUser().id
			};
			return component;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000097D4 File Offset: 0x000079D4
		public static void SendVRCEvent(VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType type, GameObject instagator)
		{
			if (PlayerExtensions.handler == null)
			{
				PlayerExtensions.handler = Resources.FindObjectsOfTypeAll<VRC_EventHandler>().FirstOrDefault<VRC_EventHandler>();
			}
			vrcEvent.ParameterObject = Player.prop_Player_0.prop_USpeaker_0.gameObject;
			PlayerExtensions.handler.TriggerEvent(vrcEvent, type, instagator, 0f);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009824 File Offset: 0x00007A24
		public static GameObject InstantiatePrefab(string PrefabNAME, Vector3 position, Quaternion rotation)
		{
			return Networking.Instantiate(0, PrefabNAME, position, rotation);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000982F File Offset: 0x00007A2F
		public static void Mute(bool toggle)
		{
			USpeaker.field_Private_Static_Boolean_0 = toggle;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009837 File Offset: 0x00007A37
		public static void SetVolume(this Player player, float vol)
		{
			player.GetUSpeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0 = vol;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000984A File Offset: 0x00007A4A
		public static float GetVolume(this Player player)
		{
			return player.GetUSpeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000985C File Offset: 0x00007A5C
		public static bool IsLocalMuted(this Player player)
		{
			return player.GetVolume() == 0f;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000986B File Offset: 0x00007A6B
		public static void LocalMute(this Player player)
		{
			player.SetVolume(0f);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009878 File Offset: 0x00007A78
		public static void LocalUnMute(this Player player)
		{
			player.SetVolume(1f);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009885 File Offset: 0x00007A85
		public static bool IsInVR(this Player player)
		{
			return player.GetVRCPlayerApi().IsUserInVR();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009892 File Offset: 0x00007A92
		public static void Teleport(this Player player)
		{
			PlayerExtensions.LocalVRCPlayer.transform.position = player.GetVRCPlayer().transform.position;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000098B3 File Offset: 0x00007AB3
		public static void ReloadAvatar(this Player Instance)
		{
			VRCPlayer.Method_Public_Static_Void_APIUser_0(Instance.GetAPIUser());
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000098C0 File Offset: 0x00007AC0
		public static QuickMenu GetQuickMenu()
		{
			return GameObject.Find("UserInterface/QuickMenu").GetComponent<QuickMenu>();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000098D1 File Offset: 0x00007AD1
		public static Player GetSelectedPlayer()
		{
			return PlayerExtensions.GetQuickMenu().field_Private_Player_0;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000098DD File Offset: 0x00007ADD
		public static void ReloadAllAvatars()
		{
			PlayerExtensions.LocalVRCPlayer.Method_Public_Void_Boolean_0(false);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000098EA File Offset: 0x00007AEA
		public static VRCPlayer GetVRCPlayer(this Player player)
		{
			return player.prop_VRCPlayer_1;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000098F2 File Offset: 0x00007AF2
		public static VRCAvatarManager GetVRCAvatarManager(this VRCPlayer player)
		{
			return player.field_Private_VRCAvatarManager_0;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000098FA File Offset: 0x00007AFA
		public static string GetName(this Player player)
		{
			return player.GetAPIUser().displayName;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00009907 File Offset: 0x00007B07
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000990E File Offset: 0x00007B0E
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00009916 File Offset: 0x00007B16
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000991D File Offset: 0x00007B1D
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009925 File Offset: 0x00007B25
		public static float DefaultGain
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000992C File Offset: 0x00007B2C
		public static float MaxGain
		{
			get
			{
				return float.MaxValue;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009933 File Offset: 0x00007B33
		public static bool IsMaster(this Player player)
		{
			return player.GetVRCPlayerApi().isMaster;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009940 File Offset: 0x00007B40
		public static int GetActorNumber(this Player player)
		{
			return player.GetVRCPlayerApi().playerId;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000994D File Offset: 0x00007B4D
		public static int GetPlayerFrames(this Player player)
		{
			if (player.GetPlayerNet().field_Private_Byte_0 == 0)
			{
				return 0;
			}
			return (int)(1000f / (float)player.GetPlayerNet().field_Private_Byte_0);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009971 File Offset: 0x00007B71
		public static int GetPlayerPing(this Player player)
		{
			return (int)player.GetPlayerNet().field_Private_Int16_0;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000997E File Offset: 0x00007B7E
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

		// Token: 0x06000186 RID: 390 RVA: 0x000099A7 File Offset: 0x00007BA7
		public static void SetLocalPlayerWalkSpeed(float speed)
		{
			PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetWalkSpeed(speed);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000099B9 File Offset: 0x00007BB9
		public static void SetLocalPlayerWalkSpeed()
		{
			PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetWalkSpeed(0f);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000099CF File Offset: 0x00007BCF
		public static void SetLocalPlayerRunSpeed(float speed)
		{
			PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetRunSpeed(speed);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000099E1 File Offset: 0x00007BE1
		public static void SetLocalPlayerRunSpeed()
		{
			PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetRunSpeed(0f);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000099F7 File Offset: 0x00007BF7
		public static void SetLocalPlayerStrafeSpeed(float speed)
		{
			PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetStrafeSpeed(speed);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00009A09 File Offset: 0x00007C09
		public static void SetLocalPlayerStrafeSpeed()
		{
			PlayerExtensions.LocalPlayer.GetVRCPlayerApi().SetStrafeSpeed(0f);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009A1F File Offset: 0x00007C1F
		public static void ToggleBlock(string player)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009A24 File Offset: 0x00007C24
		public static Player GetPlayer(int ActorNumber)
		{
			return (from p in PlayerExtensions.AllPlayers
					where p.GetActorNumber() == ActorNumber
					select p).FirstOrDefault<Player>();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static string GetSteamID(this VRCPlayer player)
		{
			return player.field_Private_UInt64_0.ToString();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009A78 File Offset: 0x00007C78
		public static Player GetPlayer(string Displayname)
		{
			return (from p in PlayerExtensions.AllPlayers
					where p.GetName() == Displayname
					select p).FirstOrDefault<Player>();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009AAD File Offset: 0x00007CAD
		public static Player GetPlayer(this VRCPlayer player)
		{
			return player._player;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009AB5 File Offset: 0x00007CB5
		public static bool IsLocalPlayer(this Player player)
		{
			return player.GetAPIUser().id == APIUser.CurrentUser.id;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public static Player GetPlayerByID(string UserID)
		{
			return (from p in PlayerExtensions.AllPlayers
					where p.GetAPIUser().id == UserID
					select p).FirstOrDefault<Player>();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009B09 File Offset: 0x00007D09
		public static void SetGain(float Gain)
		{
			PlayerExtensions.LocalGain = Gain;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009B11 File Offset: 0x00007D11
		public static void ResetGain()
		{
			USpeaker.field_Internal_Static_Single_1 = PlayerExtensions.DefaultGain;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009B1D File Offset: 0x00007D1D
		public static bool IsInWorld()
		{
			return RoomManager.field_Internal_Static_ApiWorld_0 != null;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009B27 File Offset: 0x00007D27
		public static Player[] GetAllPlayers()
		{
			return PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
		}

		// Token: 0x040000F8 RID: 248
		private static VRC_EventHandler handler;
	}
}
