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

        internal static bool IsSupportedOS() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        internal static void AskForConfirmation()
        {
            Utilities.Logger.LogInput("Are you sure you want to delete all temporary files found on your system? (type yes or no): ");
            string input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "yes":
                    RemoveTemporaryFiles();
                    break;
                default:
                    Utilities.ExitApplication();
                    break;
            }
        }

        internal static void RemoveTemporaryFiles()
        {
            Utilities.ClearConsole();

            List<string> temporaryFiles = TemporaryFileManager.Windows.GetTemporaryFilesList();
            int length = temporaryFiles.Count;

            double temp, fileSize = 0;

            for (int i = 0; i < length; i++)
            {
                switch (TemporaryFileManager.Windows.DeleteFile(temporaryFiles[i], out temp))
                {
                    case true:
                        fileSize += temp;
                        Utilities.Logger.LogDeletion(i == length - 1 ? (temporaryFiles[i] + "\n\n") : (temporaryFiles[i] + "\n"));
                        break;
                    case false:
                        Utilities.Logger.LogError("Failed to delete the file: " + (i == length - 1 ? (temporaryFiles[i] + "\n\n") : (temporaryFiles[i] + "\n")));
                        break;
                }
            }
            TemporaryFileManager.Windows.CleanUpDirectories();
            ShowResults(fileSize);
        }

        internal static void ShowResults(double spaceSaved)
        {
            Utilities.Logger.LogInfo("All temporary files that could be deleted were deleted successfully.\n");
            Utilities.Logger.LogInfo($"Total space saved: {Math.Round(spaceSaved, 2)} MB.\n\n");

            Utilities.Logger.LogInput("Press any key to exit...");
            Console.ReadKey();
            Utilities.ExitApplication();
        }
    }
}