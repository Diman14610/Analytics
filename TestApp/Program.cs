using Analytics;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var analytics = new AnalyticsFactory()
                .Configure(con =>
                {
                    con.AddMethod(new CustomMethod
                    {
                        MethodName = "test",
                        MajorFunc = (text) => text.Length > 0,
                        ArgumentsFunc = (text, arm) => arm.All(text => text.Length > 0),
                    });
                })
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", "z"))
                .EqualsTo(r => r.StartsWith("h").Contains("168").EndsWith("1"))
                .EqualsTo(r => r.Hex())
                .EqualsTo(r => r.Int())
                .EqualsTo(r => r.Str().UseCustomMethod("test"))
                .EqualsTo(r => r.Ip().Str());

            AnalyticsResult analyticsResult = analytics.Analysis("hi");

            Console.ReadKey();
        }
    }
}