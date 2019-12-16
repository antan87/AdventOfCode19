using AdventOfCode19App.Day10;
using System.Collections.Generic;
using Xunit;

namespace XUnitTest.Day10
{
    public sealed class CalendarDay10Tests
    {
        [Fact]
        public void CalendarDay10_GetMeteoritGrid()
        {
            List<string> input = new List<string>
            {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            };

            string[,] grid = CalendarDay10.GetMeteoritGrid(input);
            string[,] expected = new string[5, 5]
            {
                { ".", "#", ".", ".", "#" },
                { ".", ".", ".", ".", "." },
                { "#", "#", "#", "#", "#" },
                { ".", ".", ".", ".", "#" },
                { ".", ".", ".", "#", "#" }
            };

            Assert.Equal(expected, grid);
        }
    }
}