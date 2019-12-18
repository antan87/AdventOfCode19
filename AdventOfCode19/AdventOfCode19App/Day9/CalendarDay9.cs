using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day9
{
    public sealed class CalendarDay9 : ICalenderDay
    {
        public string Header() => "--- Day 9: Sensor Boost ---";

        private static ReadOnlySpan<long> GetChunk(ReadOnlySpan<long> array, long start, int length) => start + length > array.Length ? array.Slice(0, 0) : array.Slice(Convert.ToInt32(start), length);

        private static (long? index, long? output, long? relativeBase) RunOptcodeAction(long index, (long optcode, ParameterMode valueOne, ParameterMode valueTwo, ParameterMode valueThree) parameters, ReadOnlySpan<long> chunk, ref long[] array, int? input, long relativeBase)
        {
            try
            {
                switch (parameters.optcode)
                {
                    case 1:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array, relativeBase);
                            int positionCase1 = GetPosition(parameters.valueThree, 3, chunk, array, relativeBase);
                            SetValueToArray(ref array, positionCase1, integerOne + integerTwo);

                            return (index + 4, null, relativeBase);
                        }

                    case 2:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array, relativeBase);
                            int positionCase2 = GetPosition(parameters.valueThree, 3, chunk, array, relativeBase);
                            SetValueToArray(ref array, positionCase2, integerOne * integerTwo);
                            return (index + 4, null, relativeBase);
                        }

                    case 3:
                        {
                            if (!input.HasValue)
                                throw new Exception($"Input is not set.");

                            int positionCase3 = GetPosition(parameters.valueOne, 1, chunk, array, relativeBase);
                            SetValueToArray(ref array, positionCase3, input.Value);
                            return (index + 2, null, relativeBase);
                        }

                    case 4:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            return (index + 2, integerOne, relativeBase);
                        }

                    case 5:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            if (integerOne == 0)
                                return (index + 3, null, relativeBase);

                            var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array, relativeBase);
                            return (integerTwo, null, relativeBase);
                        }

                    case 6:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            if (integerOne != 0)
                                return (index + 3, null, relativeBase);

                            var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array, relativeBase);
                            return (integerTwo, null, relativeBase);
                        }

                    case 7:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array, relativeBase);
                            int value = integerOne < integerTwo ? 1 : 0;

                            var positionCase7 = GetPosition(parameters.valueThree, 3, chunk, array, relativeBase);
                            SetValueToArray(ref array, positionCase7, value);
                            return (index + 4, null, relativeBase);
                        }

                    case 8:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            var integerTwo = GetValue(parameters.valueTwo, 2, chunk, array, relativeBase);
                            int value = integerOne == integerTwo ? 1 : 0;

                            array[GetPosition(parameters.valueThree, 3, chunk, array, relativeBase)] = value;
                            return (index + 4, null, relativeBase);
                        }

                    case 9:
                        {
                            var integerOne = GetValue(parameters.valueOne, 1, chunk, array, relativeBase);
                            var newRelativeBase = relativeBase + integerOne;
                            return (index + 2, null, newRelativeBase);
                        }
                    default:
                        return (null, null, null);
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw e;
            }
        }

        private static int GetPosition(ParameterMode mode, long index, ReadOnlySpan<long> chunk, Span<long> array, long relativeBase)
        {
            switch (mode)
            {
                case ParameterMode.Relative:
                    var value = chunk[Convert.ToInt32(index)];
                    var newIndex = relativeBase + value;

                    return Convert.ToInt32(newIndex);

                case ParameterMode.Position:
                    return Convert.ToInt32(chunk[Convert.ToInt32(index)]);

                default:
                    throw new Exception($"{mode} is not supported.");
            }
        }

        private static long GetValue(ParameterMode mode, long index, ReadOnlySpan<long> chunk, Span<long> array, long relativeBase)
        {
            switch (mode)
            {
                case ParameterMode.Immediate:
                    return chunk[Convert.ToInt32(index)];

                case ParameterMode.Relative:
                    var value = chunk[Convert.ToInt32(index)];
                    var newIndex = Convert.ToInt32(relativeBase + value);

                    return GetValueFromArray(ref array, newIndex);

                default:
                    var newIndex2 = Convert.ToInt32(chunk[Convert.ToInt32(index)]);
                    return GetValueFromArray(ref array, newIndex2);
            }
        }

        private static (long optcode, ParameterMode valueOne, ParameterMode valueTwo, ParameterMode valueThree) GetInstructions(long indexValue)
        {
            long[] integers = LongHelper.LongToLongArray(indexValue);
            long optcode = GetOptcode(integers);
            var modes = GetParameterModes(integers);

            return (optcode, modes.valueOne, modes.valueTwo, modes.valueThree);

            static long GetOptcode(long[] integers)
            {
                switch (integers.Length)
                {
                    case 1:
                        return integers[0];

                    case 2:
                        return LongHelper.LongArrayToLong(new[] { integers[0], integers[1] });

                    case 3:
                        return LongHelper.LongArrayToLong(new[] { integers[1], integers[2] });

                    case 4:
                        return LongHelper.LongArrayToLong(new[] { integers[2], integers[3] });

                    case 5:
                        return LongHelper.LongArrayToLong(new[] { integers[3], integers[4] });

                    default:
                        throw new ArgumentException("Optcode not in range.");
                }
            }

            static (ParameterMode valueOne, ParameterMode valueTwo, ParameterMode valueThree) GetParameterModes(long[] integers)
            {
                switch (integers.Length)
                {
                    case 1:
                    case 2:
                        return (ParameterMode.Position, ParameterMode.Position, ParameterMode.Position);

                    case 3:
                        return (GetParameterMode(integers[0]), ParameterMode.Position, ParameterMode.Position);

                    case 4:
                        var case4ValueOne = GetParameterMode(integers[1]);
                        var case4ValueTwo = GetParameterMode(integers[0]);

                        return (case4ValueOne, case4ValueTwo, ParameterMode.Position);

                    case 5:
                        var case5ValueOne = GetParameterMode(integers[2]);
                        var case5ValueTwo = GetParameterMode(integers[1]);
                        var case5ValueThree = GetParameterMode(integers[0]);

                        return (case5ValueOne, case5ValueTwo, case5ValueThree);

                    default:
                        throw new ArgumentException("Optcode not in range.");
                }
            }

            static ParameterMode GetParameterMode(long number)
            {
                switch (number)
                {
                    case 0:
                        return ParameterMode.Position;

                    case 1:
                        return ParameterMode.Immediate;

                    case 2:
                        return ParameterMode.Relative;

                    default:
                        throw new ArgumentException($"{number} is not in range parameter mode range.");
                }
            }
        }

        private static long GetValueFromArray(ref Span<long> array, int index)
        {
            if (index >= array.Length)
            {
                List<long> temp = array.ToArray().ToList();

                while (index > temp.Count() - 1)
                    temp.Add(0);

                array = temp.ToArray();
            }

            return array[index];
        }

        public async Task<string> Run()
        {
            var resourceName = "AdventOfCode19App.Day9.Dataset.txt";
            var integers = await DataHelper.GetLongTestData(resourceName);
            StringBuilder builder = new StringBuilder();
            var output = GetOutput((long[])integers.Clone(), 1);
            builder.AppendLine($"Part 1 with input value 1: {output.GetValueOrDefault(0)}");

            output = GetOutput((long[])integers.Clone(), 2);
            builder.AppendLine($"Part 2 with input value 1: {output.GetValueOrDefault(0)}");

            return builder.ToString();
        }

        public static long? GetOutput(long[] integers, int? input)
        {
            long relativeBase = 0;
            int chunkSize = integers.Length < 4 ? integers.Length : 4;
            for (long index = 0; index < integers.Length;)
            {
                ReadOnlySpan<long> chunk = GetChunk(integers.AsSpan(), index, chunkSize);
                if (chunk.Length == 0)
                    break;

                var parameters = GetInstructions(chunk[0]);
                var inputArg = input;
                var response = RunOptcodeAction(index, parameters, chunk, ref integers, inputArg, relativeBase);
                if (!response.index.HasValue)
                    break;

                if (!response.relativeBase.HasValue)
                    break;

                index = response.index.Value;
                relativeBase = response.relativeBase.Value;
                if (response.output.HasValue)
                    return response.output.Value;
            }

            return null;
        }

        private static long SetValueToArray(ref long[] array, int index, long value)
        {
            if (index >= array.Length)
            {
                List<long> temp = array.ToArray().ToList();

                while (index > temp.Count() - 1)
                    temp.Add(0);

                array = temp.ToArray();
            }

            return array[index] = value;
        }
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2,
    }
}