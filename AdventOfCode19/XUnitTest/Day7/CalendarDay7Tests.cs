using AdventOfCode19App.Day7;
using System.Collections.Generic;
using Xunit;

namespace XUnitTest.Day7
{
    public sealed class CalendarDay7Tests
    {
        [Fact]
        public void CalendarDay7_GetMaxOutput()
        {
            var testData = new List<(int input, int[] integers, int expected)>
            {
                (0, new int []{ 3,23,3,24,1002,24,10,24,1002,23,-1,23, 101,5,23,23,1,24,23,23,4,23,99,0,0},54321),
                (0, new int []{ 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0}, 65210)
            };

            var settings = new List<int> { 0, 1, 2, 3, 4 };
            foreach (var data in testData)
            {
                var maxOutput = CalendarDay7.GetMaxOutput(data.integers, settings, data.input);

                Assert.Equal(data.expected, maxOutput.output);
            }
        }
    }
}