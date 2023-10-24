using Analytics;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commonConfiguration = new AnalyticsBlock()
                .Configure(con =>
                {
                    con.AddMethod(new CustomMethod
                    {
                        MethodName = "test",
                        MajorFunc = (text) => text.Length > 0,
                        ArgumentsFunc = (text, arm) => arm.All(text => text.Length > 0),
                    });
                })
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", "."))
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", ".").SetStringComparison(StringComparison.InvariantCultureIgnoreCase))
                .EqualsTo(r => r.StartsWith("h").Contains("168").EndsWith("1"))
                .EqualsTo(r => r.Hex())
                .EqualsTo(r => r.Int())
                .EqualsTo(r => r.Str().UseCustomMethod("test"))
                .EqualsTo(r => r.Ip().Str())
                .AsAnalyticsConfiguration();

            var analytics = new AnalyticsBlock()
                .Configure(config =>
                {
                    config.ApplyConfiguration(commonConfiguration);
                });

            var text = "Hi, it's just a suggestion.";

            AnalyticsResult analyticsResult = analytics.Analysis(text);

            var test = new AssertionBlock()
                .Assert(analytics, new AssertionSettings { Name = "Test", Weight = 0.1 });

            var result = test.Proccess(text).ToList();

            Console.ReadKey();
        }
    }
}