using System.Threading.Tasks;

namespace AdventOfCode19App.Interface
{
    public interface ICalenderDay
    {
        string Header();

        Task<string> Run();
    }
}