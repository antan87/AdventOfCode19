using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode19App.Common
{
    internal static class DataHelper
    {
        public static int[] GetIntTestData(string resourceNamePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result.Split(',').Select(number => Int32.Parse(number)).ToArray();
            }
        }

        public static async Task<string[]> GetStringTestDataAsync(string resourceNamePath, string splitCharacter)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = await reader.ReadToEndAsync();
                return result.Split(splitCharacter).ToArray();
            }
        }

        public async static Task<List<string>> GetStringsTestDataAsync(string resourceNamePath)
        {
            List<string> values = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                string nextLine = await reader.ReadLineAsync();
                while (!string.IsNullOrWhiteSpace(nextLine))
                {
                    values.Add(nextLine);
                    nextLine = await reader.ReadLineAsync();
                }

                return values;
            }
        }

        public async static Task<string> GetStringTestDataAsync(string resourceNamePath)
        {
            string output = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                string nextLine = await reader.ReadLineAsync();
                while (!string.IsNullOrWhiteSpace(nextLine))
                {
                    output += nextLine;
                    nextLine = await reader.ReadLineAsync();
                }

                return output;
            }
        }
    }
}