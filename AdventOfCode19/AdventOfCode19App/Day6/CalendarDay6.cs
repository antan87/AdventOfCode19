using AdventOfCode19App.Common;
using AdventOfCode19App.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode19App.Day6
{
    public sealed class CalendarDay6 : ICalenderDay
    {
        private static int CalculateOrbitCount(Dictionary<string, string> routes)
        {
            int count = 0;
            foreach (var pair in routes)
            {
                bool isEnd = false;
                var relation = pair.Value;
                while (!isEnd)
                {
                    count++;
                    if (routes.ContainsKey(relation))
                        relation = routes[relation];
                    else
                        isEnd = true;
                }
            }
            return count;
        }

        public string Header() => "--- Day 6: Universal Orbit Map ---";

        public async Task<string> Run()
        {
            var resourceName = "AdventOfCode19App.Day6.Dataset.txt";
            var orbits = await DataHelper.GetStringsTestDataAsync(resourceName);
            var count = await GetOrbitsCounts(orbits);

            return count.ToString();
        }

        public static Task<int> GetOrbitsCounts(IEnumerable<string> orbits)
        {
            Dictionary<string, string> routes = new Dictionary<string, string>();
            foreach (string orbit in orbits)
            {
                string parent = orbit.Split(')')[0];
                string child = orbit.Split(')')[1];

                routes[child] = parent;
            }

            return Task.FromResult(CalculateOrbitCount(routes));
        }
    }
}