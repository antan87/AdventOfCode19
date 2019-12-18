using AdventOfCode19App.Day9;
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
            try
            {
                await new Program().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

        private List<ICalenderDay> CalenderDays { get; } = new List<ICalenderDay>
        {
            //new CalendarDay2(),
            //new CalendarDay3(),
            //new CalendarDay4(),
            //new CalendarDay5(),
            //new CalendarDay6(),
            //new CalendarDay7(),
            //new CalendarDay8(),
            new CalendarDay9()
            //new CalendarDay10()
        };
    }
}