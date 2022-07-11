

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
	private static Action LastActionOnMainThread;
	private static bool EventCachingDC = false;
	private static bool Spinbot = false;
	private static int SpinbotSpeed = 20;
	private static bool EmojiSpam = false;
	private static bool WestCoastLagger = false;
	private static string _PrefabName = "";
	public static float OrbitSpeed = 5f;
	public static float alpha = 0f;
	public static float a = 1f;
	public static float b = 1f;
	public static float Range = 1f;
	public static float Height = 0f;
	public static VRCPlayer currentPlayer;
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
			"PlayAudio", delegate(string audioname)
			{
				byte[] audioarray = File.ReadAllBytes("Trinity/Audios/" + audioname);
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