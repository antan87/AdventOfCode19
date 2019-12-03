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

        public async static IAsyncEnumerable<string> GetStringsTestDataAsync(string resourceNamePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (await reader.ReadLineAsync() != null)
                    yield return await reader.ReadLineAsync();
            }
        }
    }
}