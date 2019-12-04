using AdventOfCode19App.Day4;
using AdventOfCode19App.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode19App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new Program().Run();
        }

        public async Task Run()
        {
            foreach (var calenderDay in this.CalenderDays)
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine(calenderDay.Header());
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine(await calenderDay.Run());
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private List<ICalenderDay> CalenderDays { get; } = new List<ICalenderDay> { new CalendarDay4() };
    }
}