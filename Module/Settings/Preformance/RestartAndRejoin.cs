using Area51.SDK;
using Area51.SDK.ButtonAPI;
using Area51.SDK.Security;
using System;
using System.Diagnostics;
using System.IO;
using VRC.Core;


namespace Area51.Module.Settings.Preformance
{
	internal class RestartAndRejoin : BaseModule
	{
		public RestartAndRejoin() : base("Restart\nReJoin", "Restart VRChat can also be triggerd by pressing \nctrl alt backspace", Main.Instance.SettingsButtonpreformance, QMButtonIcons.CreateSpriteFromBase64(Alien.rocket), false, false)
		{
		}
		public override void OnEnable()
		{
			ApiWorldInstance Instance = RoomManager.field_Internal_Static_ApiWorldInstance_0;
			if (VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.IsUserInVR()) Process.Start(Directory.GetCurrentDirectory() + "/VRChat.exe", $"vrchat://launch?id={Instance.id}");
			else Process.Start(Directory.GetCurrentDirectory() + "/VRChat.exe", $"vrchat://launch?id={Instance.id} --no-vr");
            try
            {
                if (File.Exists(SecurityCheck.key) && SecurityCheck.CleanOnExit(File.ReadAllText(SecurityCheck.key))) { LogHandler.Log(LogHandler.Colors.Yellow, "[Area51] Shutting down, GoodBye......!", false, false); Process.GetCurrentProcess().Kill(); }
                else
                {
                    LogHandler.Log(LogHandler.Colors.Red, "[Area51] Failed to logout, please contact owner!", false, false);
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception EX) { }
        }
	}
}
