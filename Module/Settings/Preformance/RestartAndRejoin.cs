using System;
using System.Diagnostics;
using System.IO;
using VRC.Core;


namespace Area51.Module.Settings.Preformance
{
	internal class RestartAndRejoin : BaseModule
	{
		public RestartAndRejoin() : base("Restart\nReJoin", "Restart VRChat can also be triggerd by pressing \nctrl alt backspace", Main.Instance.SettingsButtonpreformance, Main.Instance.QuickMenuStuff.Button_RespawnIcon.sprite, false, false)
		{
		}
		public override void OnEnable()
		{
			ApiWorldInstance Instance = RoomManager.field_Internal_Static_ApiWorldInstance_0;
			if (VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.IsUserInVR()) Process.Start(Directory.GetCurrentDirectory() + "/VRChat.exe", $"vrchat://launch?id={Instance.id}");
			else Process.Start(Directory.GetCurrentDirectory() + "/VRChat.exe", $"vrchat://launch?id={Instance.id} --no-vr");
			Process.GetCurrentProcess().Kill();
		}
	}
}
