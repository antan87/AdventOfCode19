using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day2
{
    public sealed class CalenderDay2 : ICalenderDay
    {
        private Func<int, int, int>? GetArithmeticOperation(int opcode)
        {
            return opcode switch
            {
                99 => null,
                1 => (valueOne, valueTwo) => valueOne + valueTwo,
                2 => (valueOne, valueTwo) => valueOne * valueTwo,
            };
        }

        public Task<string> Run()
        {
            List<string> messages = new List<string>();
            var integers = this.GetTestData().AsSpan();
            for (int index = 0; index < integers.Length; index += 4)
            {
                var chunk = GetChunk(integers, index, 4);
                if (chunk.Length == 0)
                    break;

                var opcode = chunk[0];
                var operation = GetArithmeticOperation(chunk[0]);
                if (operation == null)
                    break;

                var integerOne = integers[chunk[1]];
                var integerTwo = integers[chunk[2]];
                var position = integers[chunk[3]];
                if (chunk.Length - 1 >= position)
                    integers[position] = operation(integerOne, integerTwo);
            }

            return Task.FromResult(string.Join(',', integers.ToArray()));
        }

        private static Span<int> GetChunk(Span<int> array, int start, int length) => start + length > array.Length ? array.Slice(0, 0) : array.Slice(start, length);

        private int[] GetTestData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "AdventOfCode19App.Day2.DataSet.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result.Split(',').Select(number => Int32.Parse(number)).ToArray();
            }
        }

        public string Header() => "--- Day 2: 1202 Program Alarm ---";
    }
}