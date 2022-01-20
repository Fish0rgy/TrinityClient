using Area51.Module.Settings.Render;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using VRC.Core;

namespace Area51.SDK
{
    class Logg
    {
        private static List<string> DebugLogs = new List<string>();
        private static int duplicateCount = 1;
        private static string lastMsg = "";
        public static void DisplayLogo()
        {
            APIUser currentUser = APIUser.CurrentUser;
            string fileVersion = FileVersionInfo.GetVersionInfo("Area51/DLL/Area51.dll").FileVersion;
            Console.Title = $"Area 51 Client | Stable Beta: {fileVersion} | Fish, Silly & Josh";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                                                                                                        ");
            Console.WriteLine("                                     █████╗ ██████╗ ███████╗ █████╗     ███████╗ ██╗                                    ");
            Console.WriteLine("                                    ██╔══██╗██╔══██╗██╔════╝██╔══██╗    ██╔════╝███║                                    ");
            Console.WriteLine("                                    ███████║██████╔╝█████╗  ███████║    ███████╗╚██║                                    ");
            Console.WriteLine("                                    ██╔══██║██╔══██╗██╔══╝  ██╔══██║    ╚════██║ ██║                                    ");
            Console.WriteLine("                                    ██║  ██║██║  ██║███████╗██║  ██║    ███████║ ██║                                    ");
            Console.WriteLine("                                    ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝    ╚══════╝ ╚═╝                                    ");
            Console.WriteLine("                                                                                                                        ");                                   
            Console.WriteLine("                                I OR SOMEONE I'M CHILL WITH IS COOL WITH YOU, Congrats!                                 ");
            Console.WriteLine("                                               Here's A Fucking Cookie *                                                ");
            Console.WriteLine("                               Update Log -> SpaceshipLoader √ , API √ , BOT √, TWEAKS √                                ");
            Console.WriteLine("                                           The Developers's: Fish, Josh, Silly                                          ");
            Console.WriteLine($"                                               Client Version {fileVersion}                                            ");
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


        public static void Log(Logg.Colors color, string message, bool timeStamp = false, bool logToRpc = false)
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Area 51");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ~> ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = Logg.getColor(color);
            Console.Write(message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static ConsoleColor getColor(Logg.Colors color)
        {
            if (color == Logg.Colors.Default)
            {
                return ConsoleColor.White;
            }
            switch (color)
            {
                case Logg.Colors.Red:
                    return ConsoleColor.Red;
                case Logg.Colors.Blue:
                    return ConsoleColor.Blue;
                case Logg.Colors.Black:
                    return ConsoleColor.Black;
                case Logg.Colors.Green:
                    return ConsoleColor.Green;
                case Logg.Colors.Yellow:
                    return ConsoleColor.Yellow;
                case Logg.Colors.Cyan:
                    return ConsoleColor.Cyan;
                case Logg.Colors.DarkRed:
                    return ConsoleColor.DarkRed;
                case Logg.Colors.DarkGreen:
                    return ConsoleColor.DarkGreen;
                case Logg.Colors.DarkBlue:
                    return ConsoleColor.DarkBlue;
                case Logg.Colors.Grey:
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
