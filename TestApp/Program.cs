using Analytics.Core;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r = new BaseAnalytics()
                .CheckFor(
                s => s.Hex,
                s => s.Ip,
                s => s.Int,
                s => s.Str)
                .EqualsTo(s => s.Str)
                .Analysis("#ffffff")
                .ToList();

            var an = new BaseAnalytics()
                .EqualsTo(s => s.Datetime, s => s.Date)
                .Analysis("20.05.2023")
                .ToList();

            Console.ReadKey();
        }
    }
}