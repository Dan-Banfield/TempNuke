using System;
using System.IO;
using System.Threading.Tasks;

namespace TempNuke.Core
{
    internal static class TemporaryFileManager
    {
        internal static class Windows
        {
            internal static async Task<string[]> GetTemporaryFilesAsync()
            {
                string[] result = new string[0];
                await Task.Run(() =>
                {
                    result = Directory.GetFiles($"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Temp");
                });
                return result;
            }

            internal static async Task DeleteFilesAsync(string[] inputFiles)
            {
                await Task.Run(() => 
                {
                    int length = inputFiles.Length;
                    for (int i = 0; i < length; i++)
                    {
                        File.Delete(inputFiles[length]);
                    }
                });
            }
        }
    }
}