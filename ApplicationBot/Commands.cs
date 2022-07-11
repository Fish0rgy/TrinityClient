

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Trinity.SDK.Photon;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using Trinity.SDK;
using System.Collections;

namespace Trinity.Bot.Commands;

public class Commands
{
	public static VRC.Player OrbitTarget;
	public static bool e1toggle;
	public static void LoginBots(int Profile)
	{
		Process.Start(Directory.GetCurrentDirectory() + "\\VRChat.exe", string.Format("--profile={0} --fps=25 --no-vr --trinity-bot %2", Profile));
	}
	public static void UpdateShit()
	{
		if (Main.IsApplicationBot && Main.LastActionOnMainThread != null)
		{
			Main.LastActionOnMainThread();
			Main.LastActionOnMainThread = null;
		}
		Main.HandleBotFunctions();
	}

	public static IEnumerator RapeThyEars()
	{
		while (e1toggle)
		{
			byte[] VoiceData = Convert.FromBase64String(SDK.Security.SecurityCheck.ExploitData.EarrapeData);
			byte[] nulldata = new byte[4];
			byte[] ServerTime = BitConverter.GetBytes(VRC.SDKBase.Networking.GetServerTimeInMilliseconds());
			Buffer.BlockCopy(nulldata, 0, VoiceData, 0, 4);
			Buffer.BlockCopy(ServerTime, 0, VoiceData, 4, 4);
			for (int i = 0; i < 80; i++)
			{
				PhotonExtensions.OpRaiseEvent(1, VoiceData, new Photon.Realtime.RaiseEventOptions() { field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others, field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache }, default);
			}
			yield return new WaitForSecondsRealtime(0.1f);
		}
		yield break;
	}

	// Token: 0x04000177 RID: 375
	private static Action LastActionOnMainThread;

	// Token: 0x04000178 RID: 376
	private static bool EventCachingDC = false;

	// Token: 0x04000179 RID: 377
	private static bool Spinbot = false;

	// Token: 0x0400017A RID: 378
	private static int SpinbotSpeed = 20;

	// Token: 0x0400017B RID: 379
	private static bool EmojiSpam = false;

	// Token: 0x0400017C RID: 380
	private static bool WestCoastLagger = false;

	// Token: 0x0400017D RID: 381
	private static string _PrefabName = "";

	// Token: 0x0400017E RID: 382
	public static float OrbitSpeed = 5f;

	// Token: 0x0400017F RID: 383
	public static float alpha = 0f;

	// Token: 0x04000180 RID: 384
	public static float a = 1f;

	// Token: 0x04000181 RID: 385
	public static float b = 1f;

	// Token: 0x04000182 RID: 386
	public static float Range = 1f;

	// Token: 0x04000183 RID: 387
	public static float Height = 0f;

	// Token: 0x04000184 RID: 388
	public static VRCPlayer currentPlayer;

	// Token: 0x04000185 RID:

	// Token: 0x0600022D RID: 557 RVA: 0x0000CC8D File Offset: 0x0000AE8D
	public static void StartBots(int Profile)
	{
		Process.Start(Directory.GetCurrentDirectory() + "\\VRChat.exe", string.Format("--profile={0} --fps=25 --no-vr --trinity-bot -batchmode -noUpm -nographics -disable-gpu-skinning -no-stereo-rendering -nolog %2", Profile));

	}
	public static Dictionary<string, Action<string>> Cmd = new Dictionary<string, Action<string>>
	{
	{
		"ChangeAvatar",
		delegate(string AvatarID)
		{
			//PlayerWrapper.ChangeAvatar(AvatarID);
		}
	},
	{
		"JoinWorld",
		delegate(string Parameters)
		{
			Networking.GoToRoom(Parameters);
			LogHandler.Log(LogHandler.Colors.DarkBlue, "Joining World: " + Parameters);
		}
	},
	{
		"OrbitUser",
		delegate(string Parameters)
		{
			OrbitTarget = PlayerExtensions.GetPlayerByID(Parameters);
		}
	},
	{
		"Unmute",
		delegate(string Parameters)
		{
			PlayerExtensions.Mute(false);
		}
	},
        {
			"PlayAudio", delegate(string URL)
            {
				
			}
        },
	{
		"Mute",
		delegate(string Parameters)
		{
			PlayerExtensions.Mute(true);
		}
	},
	{
		"EarrapeON",
		delegate(string Parameters)
		{
			RapeThyEars();
			e1toggle = true;
		}
	},
	{
		"EarrapeOFF",
		delegate(string Parameters)
		{
			e1toggle = false;
		}
	},
	{
		"LoudMicON",
		delegate(string bruh)
		{
			USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
			VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_512k;
		}
	},
	{
		"LoudMicOFF",
		delegate(string idk)
		{
			  USpeaker.field_Internal_Static_Single_1 = 1;
			VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_USpeaker_0.field_Public_BitRate_0 = BitRate.BitRate_24K;
		}
	},
	{
		"StopBots",
		delegate(string k)
		{
			Process.GetCurrentProcess().Kill();

		}
		},
	{
		"GoHome",
		delegate(string v)
		{
			string homeLocation = APIUser.CurrentUser.homeLocation;
			Networking.GoToRoom(homeLocation);
		}
	}
};
}