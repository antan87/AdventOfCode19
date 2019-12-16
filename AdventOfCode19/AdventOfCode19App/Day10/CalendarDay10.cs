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

            return string.Empty;
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

        public static ((int x, int y) coordinate, int count) GetMonitoringStation(int[,] asteroids)
        {
            return ((0, 0), 0);
        }
    }
}