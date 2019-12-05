using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day5
{
    public sealed class CalendarDay5 : ICalenderDay
    {
        public string Header() => "--- Day 5: Sunny with a Chance of Asteroids ---";

        private static Func<int, int, int>? GetArithmeticOperation(int opcode)
        {
            return opcode switch
            {
                99 => null,
                1 => (valueOne, valueTwo) => valueOne + valueTwo,
                2 => (valueOne, valueTwo) => valueOne * valueTwo,
            };
        }

        private static Span<int> GetChunk(Span<int> array, int start, int length) => start + length > array.Length ? array.Slice(0, 0) : array.Slice(start, length);

        public Task<string> Run()
        {
            var resourceName = "AdventOfCode19App.Day2.DataSet.txt";
            var integers = DataHelper.GetIntTestData(resourceName).AsSpan();

            return Task.FromResult(string.Join(',', integers.ToArray()));
        }

        public static void RunOperation(Span<int> integers)
        {
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
                integers[chunk[3]] = operation(integerOne, integerTwo);
            }
        }
    }
}