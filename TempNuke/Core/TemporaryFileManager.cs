using System;
using System.IO;
using System.Collections.Generic;

namespace TempNuke.Core
{
    internal static class TemporaryFileManager
    {
        private const int BYTES_TO_MEGABYTES = 1048576;

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
                        string[] filesList = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                        foreach (string file in filesList)
                        {
                            result.Add(file);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
                return result;
            }

            internal static bool DeleteFile(string filePath, out double size)
            {
                try 
                {
                    size = new FileInfo(filePath).Length;
                    size = (size / BYTES_TO_MEGABYTES);
                    File.Delete(filePath); 
                    return true;  
                }
                catch 
                {
                    size = 0;
                    return false; 
                }
            }

            internal static void CleanUpDirectories()
            {
                try
                {
                    foreach (string directory in temporaryFileLocations)
                    {
                        foreach (string subDirectory in Directory.GetDirectories(directory))
                        {
                            Directory.Delete(subDirectory, true);
                        }
                    }
                }
                catch { }
            }
        }
    }
}