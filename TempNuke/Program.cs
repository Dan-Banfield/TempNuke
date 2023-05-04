using System;
using TempNuke.Core;
using System.Runtime.InteropServices;

namespace TempNuke
{
    internal class Program
    {
        internal static void Main()
        {
            Utilities.SetUpConsole();
            PerformStartupChecks();
        }

        internal static void PerformStartupChecks()
        {
            if (!IsSupportedOS()) UnsupportedOS();
            AskForConfirmation();
        }

        internal static void UnsupportedOS()
        {
            Utilities.Logger.LogError("Sorry! Your OS is not supported right now. It will be soon, though!\n");
            Utilities.Logger.LogInput("Press any key to exit: ");

            Console.ReadKey();
            Utilities.ExitApplication();
        }

        internal static bool IsSupportedOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return true;
            return false;
        }

        internal static void AskForConfirmation()
        {
            Utilities.Logger.LogInput("Are you sure you want to delete all temporary files found on your system? (type yes or no): ");
            string input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "yes":
                    //TODO: Delete temporary files.
                    break;
                default:
                    Utilities.ExitApplication();
                    break;
            }
        }
    }
}