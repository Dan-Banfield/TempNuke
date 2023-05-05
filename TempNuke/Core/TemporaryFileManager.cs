using System;
using System.IO;
using System.Collections.Generic;

namespace TempNuke.Core
{
    internal static class TemporaryFileManager
    {
        internal static class Windows
        {
            /* private const string DRIVE = @"C:\";
            private const string SEARCH_PATTERN = "*.tmp|*.bak|*.old|*~|*.dmp|*.log"; */

            private static string[] temporaryFileLocations = new string[] { $@"C:\Users\{Environment.UserName}\AppData\Local\Temp" };

            internal static List<string> GetTemporaryFilesList() 
            {
                List<string> result = new List<string>();
                try
                {
                    foreach (string directory in temporaryFileLocations)
                    {
                        string[] filesList = Directory.GetFiles(directory);

                        foreach (string file in filesList)
                        {
                            result.Add(file);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
                return result;
            }

            internal static bool DeleteFile(string filePath)
            {
                try { File.Delete(filePath); return true;  }
                catch { return false; }
            }
        }
    }
}