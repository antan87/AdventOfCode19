using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day5
{
    public sealed class CalendarDay5 : ICalenderDay
    {
        public string Header() => "--- Day 5: Sunny with a Chance of Asteroids ---";

        private static ReadOnlySpan<int> GetChunk(ReadOnlySpan<int> array, int start, int length) => start + length > array.Length ? array.Slice(0, 0) : array.Slice(start, length);

        private static (int? increment, int? output) RunOptcodeAction((int optcode, ParameterMode valueOne, ParameterMode valueTwo) parameters, ReadOnlySpan<int> chunk, Span<int> array)
        {
            switch (parameters.optcode)
            {
                case 1:
                    {
                        var integerOne = GetValue(parameters.valueOne, 1, chunk, array);
                        var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array);
                        array[chunk[3]] = integerOne + integerTwo;

                        return (4, null);
                    }

                case 2:
                    {
                        var integerOne = GetValue(parameters.valueOne, 1, chunk, array);
                        var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array);
                        array[chunk[3]] = integerOne * integerTwo;
                        return (4, null);
                    }

                case 3:
                    {
                        var integerTwo = 1;
                        array[chunk[1]] = integerTwo;
                        return (2, null);
                    }

                case 4:
                    {
                        var integerOne = GetValue(parameters.valueOne, 1, chunk, array);
                        return (2, integerOne);
                    }

                default:
                    return (null, null);
            };
        }

        private static int GetValue(ParameterMode mode, int index, ReadOnlySpan<int> chunk, Span<int> array)
        {
            switch (mode)
            {
                case ParameterMode.Immediate:
                    return chunk[index];

                default:
                    return array[chunk[index]];
            }
        }

        private static (int optcode, ParameterMode valueOne, ParameterMode valueTwo) GetInstructions(int indexValue)
        {
            int[] integers = IntegerHelper.IntToIntArray(indexValue);
            int optcode = GetOptcode(integers);
            var modes = GetParameterModes(integers);

            return (optcode, modes.valueOne, modes.valueTwo);

            static int GetOptcode(int[] integers)
            {
                switch (integers.Length)
                {
                    case 1:
                        return integers[0];

                    case 2:
                        return IntegerHelper.IntArrayToInt(new[] { integers[0], integers[1] });

                    case 3:
                        return IntegerHelper.IntArrayToInt(new[] { integers[1], integers[2] });

                    case 4:
                        return IntegerHelper.IntArrayToInt(new[] { integers[2], integers[3] });

                    default:
                        throw new ArgumentException("Optcode not in range.");
                }
            }

            static (ParameterMode valueOne, ParameterMode valueTwo) GetParameterModes(int[] integers)
            {
                switch (integers.Length)
                {
                    case 1:
                    case 2:
                        return (ParameterMode.Position, ParameterMode.Position);

                    case 3:
                        return (integers[0] == 1 ? ParameterMode.Immediate : ParameterMode.Position, ParameterMode.Position);

                    case 4:
                        var valueOne = integers[1] == 1 ? ParameterMode.Immediate : ParameterMode.Position;
                        var valueTwo = integers[0] == 1 ? ParameterMode.Immediate : ParameterMode.Position;

                        return (valueOne, valueTwo);

                    default:
                        throw new ArgumentException("Optcode not in range.");
                }
            }
        }

        public Task<string> Run()
        {
            var resourceName = "AdventOfCode19App.Day5.Dataset.txt";
            var integers = DataHelper.GetIntTestData(resourceName);
            var outputs = GetOutputs(integers);

            return Task.FromResult(string.Join(',', outputs));
        }

        public static IEnumerable<int> GetOutputs(int[] integers)
        {
            int? increase = 0;
            for (int index = 0; index < integers.Length; index += increase.Value)
            {
                ReadOnlySpan<int> chunk = GetChunk(integers.AsSpan(), index, 4);
                if (chunk.Length == 0)
                    break;

                var parameters = GetInstructions(chunk[0]);
                var response = RunOptcodeAction(parameters, chunk, integers.AsSpan());
                if (!response.increment.HasValue)
                    break;

                increase = response.increment.Value;
                if (response.output.HasValue)
                    yield return response.output.Value;
            }
        }
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1
    }
}