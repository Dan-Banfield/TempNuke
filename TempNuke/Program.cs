using System;
using TempNuke.Core;
using System.Collections.Generic;
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
                    ListTemporaryFiles();
                    break;
                default:
                    Utilities.ExitApplication();
                    break;
            }
        }

        internal static void ListTemporaryFiles()
        {
            Console.Clear();

            List<string> temporaryFiles = TemporaryFileManager.Windows.GetTemporaryFilesList();
            int length = temporaryFiles.Count;

            for (int i = 0; i < length; i++)
            {
                switch (TemporaryFileManager.Windows.DeleteFile(temporaryFiles[i]))
                {
                    case true:
                        Utilities.Logger.LogDeletion(i == length - 1 ? (temporaryFiles[i] + "\n\n") : (temporaryFiles[i] + "\n"));
                        break;
                    case false:
                        Utilities.Logger.LogError("Failed to delete the file: " + (i == length - 1 ? (temporaryFiles[i] + "\n\n") : (temporaryFiles[i] + "\n")));
                        break;
                }
            }
            Utilities.Logger.LogInfo("All temporary files that could be deleted were deleted successfully!");
            Console.ReadKey();
        }
    }
}