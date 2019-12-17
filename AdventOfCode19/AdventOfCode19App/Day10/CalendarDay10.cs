using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day10
{
    public sealed class CalendarDay10 : ICalenderDay
    {
        public string Header() => "--- Day 10: Monitoring Station ---";

        public async Task<string> Run()
        {
            string resourceName = "AdventOfCode19App.Day10.Dataset.txt";
            List<string> input = await DataHelper.GetStringsTestDataAsync(resourceName);
            string[,] grid = GetMeteoritGrid(input);
            (int x, int y, int count) bestMonitorStation = GetMonitoringStation(grid);
            return $"Best monitor station is at x: {bestMonitorStation.x} y: {bestMonitorStation.y} Count: {bestMonitorStation.count}";
        }

        public static string[,] GetMeteoritGrid(List<string> input)
        {
            var arrayList = new List<string[]>();
            foreach (string inputRowData in input)
            {
                var inputRowArray = GetInputRow(inputRowData);
                arrayList.Add(inputRowArray);
            }

            if (!arrayList.Any())
                return new string[0, 0];

            int rows = arrayList.Count();
            int columns = arrayList[0].Count();

            var grid = new string[rows, columns];
            int rowIndex = 0;
            foreach (var array in arrayList)
            {
                for (int index = 0; index < array.Length; index++)
                    grid[rowIndex, index] = array[index];

                rowIndex++;
            }

            return grid;

            static string[] GetInputRow(string inputRowData)
            {
                return inputRowData.Select(value => char.ToString(value)).ToArray();
            }
        }

        public static (int x, int y, int count) GetMonitoringStation(string[,] asteroids)
        {
            List<(int x, int y)> metroits = new List<(int x, int y)>();
            for (int row = 0; row < asteroids.GetLength(0); row++)
                for (int column = 0; column < asteroids.GetLength(1); column++)
                {
                    {
                        if (asteroids[row, column] == "#")
                            metroits.Add((column, row));
                    }
                }

            (int x, int y, int count) maxValue = (0, 0, 0);
            foreach ((int x, int y) metroit in metroits)
            {
                int count = GetMetroitViewCount(metroit, metroits.Except(new List<(int x, int y)> { metroit }));
                if (maxValue.count < count)
                    maxValue = (metroit.x, metroit.y, count);
            }

            return maxValue;
        }

        private static int GetMetroitViewCount((int x, int y) metorit, IEnumerable<(int x, int y)> metroits)
        {
            var slopes = new List<(int x, int y, decimal slope)>();
            foreach (var nextMetroit in metroits)
            {
                int xValue = nextMetroit.x - metorit.x;
                decimal slope = 0;
                if (xValue != 0)
                    slope = (decimal)(nextMetroit.y - metorit.y) / (nextMetroit.x - metorit.x);
                else
                    slope = nextMetroit.y - metorit.y < 0 ? decimal.MinValue : decimal.MaxValue;

                slopes.Add((nextMetroit.x, nextMetroit.y, slope));
            }
            int counter = 0;
            foreach (var slopeGroup in slopes.GroupBy(met => met.slope))
            {
                counter++;
            }

            return counter;
        }
    }
}