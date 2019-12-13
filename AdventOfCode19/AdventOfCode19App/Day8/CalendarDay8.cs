using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day8
{
    public sealed class CalendarDay8 : ICalenderDay
    {
        public string Header() => "--- Day 8: Space Image Format ---";

        public async Task<string> Run()
        {
            var resourceName = "AdventOfCode19App.Day8.Dataset.txt";
            var strings = await DataHelper.GetStringTestDataAsync(resourceName);
            var integers = IntegerHelper.StringToIntIEnumerable(strings);
            var layers = GetLayers(integers.ToArray(), 12, 25);

            return "Test";
        }

        private static IEnumerable<int> GetLayerNumbers (ReadOnlySpan<int[,]> layers, int rowIndex, int layerHeight, int layerWidth)
        {
          var layer =  layers.Slice(rowIndex, 2);
           
        }

        private static int[,] GetLayers(int[] integers, int rows, int columns)
        {
            int[,] layers = new int[rows, columns];
            int index = 0;
            for (int row = 0; row < rows; row++)
                for (int column = 0; column < columns; column++)
                {
                    layers[row, column] = integers[index];
                    index++;
                }

            return layers;
        }
    }
}