using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day8
{
    public sealed class CalendarDay8 : ICalenderDay
    {
        public string Header() => "--- Day 8: Space Image Format ---";

        public async Task<string> Run()
        {
            string resourceName = "AdventOfCode19App.Day8.Dataset.txt";
            var strings = await DataHelper.GetStringTestDataAsync(resourceName);
            IEnumerable<int> integers = IntegerHelper.StringToIntIEnumerable(strings);
            int[,] array = ToMultiDimensionalArray(integers.ToArray(), 25);
            (int min, IEnumerable<int> numbers) minGroup = GetLayerWithMinNumber(array, 0, array.GetLength(0) / 6, 25);

            var countOneNumbers = minGroup.numbers.Where(x => x == 1).Count();
            var countTwoNumbers = minGroup.numbers.Where(x => x == 2).Count();

            //var mew = GetMinValuesForNumber(integers.ToArray());
            //var countOneNumbers2 = mew.Where(x => x == 1).Count();
            //var countTwoNumbers2 = mew.Where(x => x == 2).Count();

            return Convert.ToString(countOneNumbers * countTwoNumbers);
        }

        private string GetImage(int[,] array)
        {
            StringBuilder builder = new StringBuilder();
            for (int row = 0; row < array.GetLength(0); row++)
            {
                builder.AppendLine();
                for (int column = 0; column < array.GetLength(1); column++)
                {
                    int pixel = array[row, column];
                    if (pixel == 0)
                        builder.Append("0");
                    else if (pixel == 1)
                        builder.Append("1");
                    else if (pixel == 2)
                        builder.Append("2");
                }
            }

            return builder.ToString();
        }

        private static int[] GetMinValuesForNumber(int[] integers)
        {
            int[] minValues = null;
            int count = int.MaxValue;
            for (int index = 0; index < integers.Count(); index += 2500)
            {
                var layer = GetLayer(integers, index, 2500);
                int layerCount = layer.ToArray().Where(x => x == 0).Count();
                if (count > layerCount)
                {
                    count = layerCount;
                    minValues = layer.ToArray();
                }
            }

            return minValues;

            static Span<int> GetLayer(Span<int> integers, int index, int length)
            {
                return integers.Slice(index, length);
            }
        }

        public static (int min, IEnumerable<int> numbers) GetLayerWithMinNumber(int[,] layers, int number, int layerHeight, int layerWidht)
        {
            (int min, IEnumerable<int> numbers) minGroup = (int.MaxValue, new List<int>());
            for (int y = 0; y < layers.GetLength(0); y += layerHeight)
            {
                var numbers = GetLayerNumbers(layers, y, layerHeight, layerWidht);
                var count = numbers.Where(value => value == number).Count();
                if (minGroup.min > count)
                    minGroup = (count, numbers);
            }

            return minGroup;
        }

        private static IEnumerable<int> GetLayerNumbers(int[,] layers, int rowIndex, int layerHeight, int layerWidth)
        {
            List<int> numbers = new List<int>();
            for (int y = rowIndex; y < rowIndex + layerHeight; y++)
                for (int x = 0; x < layerWidth; x++)
                    numbers.Add(layers[y, x]);

            return numbers;
        }

        public static int[,] ToMultiDimensionalArray(IEnumerable<int> integers, int numberOfColumns) => ToMultiDimensionalArray(integers.ToArray(), integers.Count() / numberOfColumns, numberOfColumns);

        private static int[,] ToMultiDimensionalArray(int[] integers, int rows, int columns)
        {
            int[,] layers = new int[rows, columns];
            int index = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    layers[row, column] = integers[index];
                    index++;
                }
            }

            return layers;
        }
    }
}