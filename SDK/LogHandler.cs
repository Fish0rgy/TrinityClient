using Trinity.Utilities;
using Trinity.Module.Settings.Render;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using VRC.Core;

namespace Trinity.SDK
{
    class LogHandler
    {
        private static List<string> DebugLogs = new List<string>();
        private static int duplicateCount = 1;
        private static string lastMsg = "";
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern System.IntPtr FindWindow(System.String className, System.String windowName);

        public static void DisplayLogo()
        {
            APIUser currentUser = APIUser.CurrentUser;
            string fileVersion = "1.0.0.3";
            Console.Title = $"Trinity || v{fileVersion}";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                  ████████╗██████╗ ██╗███╗   ██╗██╗████████╗██╗   ██╗                                   ");
            Console.WriteLine("                                  ╚══██╔══╝██╔══██╗██║████╗  ██║██║╚══██╔══╝╚██╗ ██╔╝                                   ");
            Console.WriteLine("                                     ██║   ██████╔╝██║██╔██╗ ██║██║   ██║    ╚████╔╝                                    ");
            Console.WriteLine("                                     ██║   ██╔══██╗██║██║╚██╗██║██║   ██║     ╚██╔╝                                     ");
            Console.WriteLine("                                     ██║   ██║  ██║██║██║ ╚████║██║   ██║      ██║                                      ");
            Console.WriteLine("                                     ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚═╝   ╚═╝      ╚═╝                                      ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("========================================================================================================================\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LogDebug(string message)
        {
            if (message == lastMsg)
            {
                DebugLogs.RemoveAt(DebugLogs.Count - 1);
                duplicateCount++;
                DebugLogs.Add($"<b>[<color=#ff00ffff>{DateTime.Now.ToString("hh:mm tt")}</color>] {message} <color=red><i>x{duplicateCount}</i></color></b>");
            }
            else
            {
                lastMsg = message;
                duplicateCount = 1;
                DebugLogs.Add($"<b>[<color=#ff00ffff>{DateTime.Now.ToString("hh:mm tt")}</color>] {message}</b>");
                if (DebugLogs.Count > 15)
                {
                    DebugLogs.Clear();
                }
            }
            DebugLog.debugLog.text.text = string.Join("\n", DebugLogs.Take(25));
            DebugLog.debugLog.text.enableWordWrapping = false;
            DebugLog.debugLog.text.fontSizeMin = 30;
            DebugLog.debugLog.text.fontSizeMax = 30;
            DebugLog.debugLog.text.alignment = TMPro.TextAlignmentOptions.Left;
            DebugLog.debugLog.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            DebugLog.debugLog.text.color = Color.white;
        }


        public static void Log(LogHandler.Colors color, string message, bool timeStamp = false, bool logToRpc = false)
        {
            if (timeStamp)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(DateTime.Now.ToString("HH:mm:ss.fff"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("] ");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Trinity");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ~> ");
            Console.ForegroundColor = LogHandler.getColor(color);
            Console.Write(message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static ConsoleColor getColor(LogHandler.Colors color)
        {
            if (color == LogHandler.Colors.Default)
            {
                return ConsoleColor.White;
            }
            switch (color)
            {
                case LogHandler.Colors.Red:
                    return ConsoleColor.Red;
                case LogHandler.Colors.Blue:
                    return ConsoleColor.Blue;
                case LogHandler.Colors.Black:
                    return ConsoleColor.Black;
                case LogHandler.Colors.Green:
                    return ConsoleColor.Green;
                case LogHandler.Colors.Yellow:
                    return ConsoleColor.Yellow;
                case LogHandler.Colors.Cyan:
                    return ConsoleColor.Cyan;
                case LogHandler.Colors.DarkRed:
                    return ConsoleColor.DarkRed;
                case LogHandler.Colors.DarkGreen:
                    return ConsoleColor.DarkGreen;
                case LogHandler.Colors.DarkBlue:
                    return ConsoleColor.DarkBlue;
                case LogHandler.Colors.Grey:
                    return ConsoleColor.Gray;
            }
            return ConsoleColor.White;
        }
        public enum Colors
        {
            Red,
            Blue,
            Black,
            White,
            Green,
            Yellow,
            Cyan,
            DarkRed,
            DarkGreen,
            DarkBlue,
            Default,
            Grey
        }
    }
}
