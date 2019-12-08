using AdventOfCode19App.Day6;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest.Day6
{
    public sealed class CalendarDay6Tests
    {
        [Fact]
        public async Task CalendarDay6_CalculateOrbitCount()
        {
            var testData = new List<(string[] orbits, int expected)>
            {
                (new string []
                {
                        "COM)B",
                        "B)C",
                        "C)D",
                        "D)E",
                        "E)F",
                        "B)G",
                        "G)H",
                        "D)I",
                        "E)J",
                        "J)K",
                        "K)L"
                },42)
            };

            foreach (var data in testData)
            {
                var outputs = await CalendarDay6.GetOrbitsCounts(data.orbits);

                Assert.Equal(data.expected, outputs);
            }
        }
    }
}