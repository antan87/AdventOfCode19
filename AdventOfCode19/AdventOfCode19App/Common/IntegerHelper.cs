﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode19App.Common
{
    public static class IntegerHelper
    {
        public static int[] IntToIntArray(int num) =>
            num.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();

        public static int IntArrayToInt(IEnumerable<int> numbers)
        {
            if (!numbers.Any())
                return 0;

            string digitText = string.Join(string.Empty, numbers.Select(digit => Convert.ToString(digit)));

            return Int32.Parse(digitText);
        }

        public static int CharToInt(char value) => Convert.ToInt32(value);

        public static IEnumerable<int> StringToIntIEnumerable(string value)
        {
            foreach (var stringValue in StringToIEnumerable(value))
                yield return Convert.ToInt32(stringValue);

            static IEnumerable<string> StringToIEnumerable(string stringValue)
            {
                for (var index = 0; index < stringValue.Length; index++)
                    yield return Convert.ToString(stringValue[index]);
            }
        }
    }
}