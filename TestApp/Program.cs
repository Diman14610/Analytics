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
                .EqualsTo(s => s.Datetime, s => s.Date)
                .EqualsTo(s => s.Str, s => s.Hex);

            var rest = new List<AnalyticsResult>
            {
                r.Analysis("#dadasdasd"),
                r.Analysis("#fff"),
                r.Analysis("#192.168.82.0"),
            };
        

            var an = new BaseAnalytics()
                .CheckFor(r => r.Datetime, r => r.Date)
                .EqualsTo(s => s.Datetime, s => s.Date)
                .EqualsTo(r => r.Date)
                .Analysis("20.05.2023");

            Console.ReadKey();
        }
    }
}