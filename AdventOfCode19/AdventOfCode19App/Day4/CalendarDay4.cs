using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day4
{
    public sealed class CalendarDay4 : ICalenderDay
    {
        public static bool ContainsDuplicates(int[] numbers)
        {
            int counter = 0;
            int currentValue = 0;
            for (int index = 0; index < numbers.Length; index++)
            {
                if (currentValue == numbers[index])
                    counter++;
                else
                {
                    counter = 1;
                    currentValue = numbers[index];
                }

                if (counter > 1)
                    return true;
            }

            return false;
        }

        public string Header() => "--- Day 4: Secure Container ---";

        public static int GetNextValidNumber(int digit)
        {
            int minimum = 0;
            int[] numbers = IntegerHelper.IntToIntArray(digit);
            for (int index = 0; index < numbers.Length; index++)
            {
                if (minimum <= numbers[index])
                    minimum = numbers[index];
                else
                    UpdateArray(index, numbers, minimum);
            }

            int newDigit = IntegerHelper.IntArrayToInt(numbers);
            if (!ContainsDuplicates(numbers))
            {
                newDigit++;
                return GetNextValidNumber(newDigit);
            }
            return newDigit;

            static void UpdateArray(int index, Span<int> array, int value)
            {
                for (; index < array.Length; index++)
                    array[index] = value;
            }
        }

        public Task<string> Run()
        {
            StringBuilder builder = new StringBuilder();
            int startDigit = 264793;
            int endDigit = 803935;
            builder.AppendLine($"Input is {startDigit} - {endDigit}");
            builder.AppendLine();
            builder.AppendLine($"Valid number of bumbers is {ValidNumbers(startDigit, endDigit)}");

            return Task.FromResult(builder.ToString());
        }

        public static int ValidNumbers(int startDigit, int endDigit)
        {
            List<int> validNumbers = new List<int>();
            int digit = startDigit;

            while (digit <= endDigit)
            {
                digit = GetNextValidNumber(digit);
                if (digit <= endDigit)
                    validNumbers.Add(digit);

                digit++;
            }

            return validNumbers.Count;
        }
    }
}