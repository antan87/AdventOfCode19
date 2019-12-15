using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode19App.Common
{
    public static class LongHelper
    {
        public static long[] LongToLongArray(long num) =>
            num.ToString().Select(digit => long.Parse(digit.ToString())).ToArray();

        public static long LongArrayToLong(IEnumerable<long> numbers)
        {
            if (!numbers.Any())
                return 0;

            string digitText = string.Join(string.Empty, numbers.Select(digit => Convert.ToString(digit)));

            return Int64.Parse(digitText);
        }

        public static long CharToLong(char value) => Convert.ToInt64(value);

        public static long StringToLong(string value) => Convert.ToInt64(value);

        public static IEnumerable<long> StringToLongIEnumerable(string value)
        {
            foreach (var stringValue in StringToIEnumerable(value))
                yield return Convert.ToInt64(stringValue);

            static IEnumerable<string> StringToIEnumerable(string stringValue)
            {
                for (var index = 0; index < stringValue.Length; index++)
                    yield return Convert.ToString(stringValue[index]);
            }
        }
    }
}