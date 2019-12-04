using AdventOfCode19App.Day4;
using System.Collections.Generic;
using Xunit;

namespace XUnitTest.Day4
{
    public class CalendarDay4Tests
    {
        [Fact]
        public void CalendarDay4_ContainsDuplicates()
        {
            var testData = new List<(int[] numbers, bool expected)>
            {
                (new int []{ 1,0,5,5,3,8}, true),
                (new int []{ 0,1,0,2,3,5}, false),
                (new int []{ 3,0,1,6,7,7}, true),
            };

            foreach (var data in testData)
            {
                bool result = CalendarDay4.ContainsDuplicates(data.numbers);
                Assert.Equal(data.expected, result);
            }
        }

        [Fact]
        public void CalendarDay4_GetNextValidNumber()
        {
            var testData = new List<(int digit, int expected)>
            {
                (6670, 6677),
                (1637, 1666),
                (9623, 9999)
            };

            foreach (var data in testData)
            {
                int result = CalendarDay4.GetNextValidNumber(data.digit);
                Assert.Equal(data.expected, result);
            }
        }

        [Fact]
        public void CalendarDay4_ValidNumbers()
        {
            var testData = new List<(int startDigit, int endDigit, int expected)>
            {
                (264793, 803935, 966),
            };

            foreach (var data in testData)
            {
                int result = CalendarDay4.ValidNumbers(data.startDigit, data.endDigit);
                Assert.Equal(data.expected, result);
            }
        }
    }
}