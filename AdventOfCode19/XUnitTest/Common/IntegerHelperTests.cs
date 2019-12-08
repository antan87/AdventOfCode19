using AdventOfCode19App.Common;
using System.Collections.Generic;
using Xunit;

namespace XUnitTest.Common
{
    public class IntegerHelperTests
    {
        [Fact]
        public void IntegerHelper_IntArrayToInt()
        {
            var testData = new List<(int[] array, int expected)>
            {
                (new[] { 2, 6, 4, 7, 9, 3 }, 264793),
                (new[] { 0,2 }, 2),
            };

            foreach (var data in testData)
            {
                int result = IntegerHelper.IntArrayToInt(data.array);

                Assert.Equal(data.expected, result);
            }
        }

        [Fact]
        public void IntegerHelper_IntToIntArray()
        {
            var testData = new List<(int digit, int[] expected)>
            {
                (264793, new[] { 2, 6, 4, 7, 9, 3 }),
            };

            foreach (var data in testData)
            {
                int[] result = IntegerHelper.IntToIntArray(data.digit);

                Assert.Equal(data.expected, result);
            }
        }
    }
}