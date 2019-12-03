using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day3
{
    public sealed class CalendarDay3 : ICalenderDay
    {
        public string Header() => "--- Day 3: Crossed Wires ---";

        public async Task<string> Run()
        {
            var resourceName = "AdventOfCode19App.Day3.DataSet.txt";

            StringBuilder builder = new StringBuilder();
            var directionData = await DataHelper.GetStringTestDataAsync(resourceName, Environment.NewLine);
            builder.AppendLine($"Wire: {directionData}");
            var directions = GetDirections(directionData);
            (int x, int y) start = (x: 1, y: 1);
            var coordinateSections = GetCoordinates(start, directions);

            builder.AppendLine();
            foreach (var coordinates in coordinateSections)
                builder.AppendLine(string.Join(" ", coordinates));

            var firstSection = coordinateSections.First();
            var lastSection = coordinateSections.Last();

            var intersections = firstSection.Intersect(lastSection);

            decimal manhattanDistance = decimal.MaxValue;
            foreach (var intersection in intersections)
            {
                var distance = GetManhattanDistance(start, intersection);
                if (distance < manhattanDistance)
                    manhattanDistance = distance;
            }

            builder.AppendLine();
            builder.AppendLine($"The manhattan distance is {manhattanDistance}");

            return builder.ToString();
        }

        private decimal GetManhattanDistance((int x, int y) coordinate, (int x, int y) coordinateTwo) => Math.Abs(coordinateTwo.x - coordinate.x) + Math.Abs(coordinateTwo.y - coordinate.y);

        private void Move(ref (int x, int y) position, string direction)
        {
            switch (direction)
            {
                case "U":
                    position.y++;
                    break;

                case "D":
                    position.y--;
                    break;

                case "L":
                    position.x--;
                    break;

                case "R":
                    position.x++;
                    break;

                default:
                    throw new ArgumentException("Direction is not valid.");
            }
        }

        private IEnumerable<IEnumerable<(int x, int y)>> GetCoordinates((int x, int y) position, IEnumerable<IEnumerable<(string direction, int steps)>> directions)
        {
            foreach (var sectionDirections in directions)
                yield return GetCoordinates(position, sectionDirections);

            IEnumerable<(int x, int y)> GetCoordinates((int x, int y) start, IEnumerable<(string direction, int steps)> directions)
            {
                foreach (var direction in directions)
                {
                    for (int count = 0; count < direction.steps; count++)
                    {
                        Move(ref start, direction.direction);
                        yield return start;
                    }
                }
            }
        }

        private IEnumerable<IEnumerable<(string direction, int steps)>> GetDirections(string[] direactionData)
        {
            foreach (var directionText in direactionData)
                yield return GetRowDirections(directionText.Split(','));

            static IEnumerable<(string direction, int steps)> GetRowDirections(string[] directions)
            {
                foreach (var direction in directions)
                    yield return GetDirection(direction);
            }

            static (string direction, int steps) GetDirection(ReadOnlySpan<char> directionText)
            {
                try
                {
                    string direction = string.Empty;
                    List<char> digits = new List<char>();
                    for (int index = 0; index < directionText.Length; index++)
                    {
                        if (index == 0)
                        {
                            direction = new string(directionText.Slice(0, 1));
                            continue;
                        }

                        digits.Add(directionText[index]);
                    }

                    string digitText = string.Join(string.Empty, digits);

                    return (direction, Int32.Parse(digitText));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}