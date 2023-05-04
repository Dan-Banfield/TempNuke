using System;

namespace TempNuke
{
    internal static class Utilities
    {
        internal static void SetUpConsole()
        {
            Console.Title = "TempNuke";
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void ExitApplication() => Environment.Exit(0);

        internal static class Logger
        {
            internal static void LogError(string error)
            {
                ConsoleColor foregroundColor = Console.ForegroundColor;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR");

                Console.ForegroundColor = foregroundColor;
                Console.Write("] " + error);
            }

            internal static void LogInput(string message)
            {
                ConsoleColor foregroundColor = Console.ForegroundColor;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("INPUT");

                Console.ForegroundColor = foregroundColor;
                Console.Write("] " + message);
            }
        }
    }
}