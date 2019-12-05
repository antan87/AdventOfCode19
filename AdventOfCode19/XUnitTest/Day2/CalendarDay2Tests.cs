using AdventOfCode19App.Day2;
using System.Collections.Generic;
using Xunit;

namespace XUnitTest.Day2
{
    public class CalendarDay2Tests
    {
        [Fact]
        public void CalendarDay2_RunOperation()
        {
            var testData = new List<(int[] numbers, int[] expected)>
            {
                (new int []{ 1,0,0,0,99 }, new int[]{ 2,0,0,0,99}),
                (new int []{ 2,4,4,5,99,0 }, new int[]{ 2,4,4,5,99,9801}),
                (new int []{ 1,1,1,4,99,5,6,0,99 }, new int[]{30,1,1,4,2,5,6,0,99}),
            };

            foreach (var data in testData)
            {
                CalenderDay2.RunOperation(data.numbers);

                Assert.Equal(data.expected, data.numbers);
            }
        }
    }
}