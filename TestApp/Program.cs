using Analytics;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;
using Analytics.Shared.Core.Assertion;

namespace TestApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var exampleText = "Hi, it's just a suggestion.";

            var testBlock = new AnalyticsBlock()
                .Configure(con =>
                {
                    con.AddMethod(new CustomMethod
                    {
                        MethodName = "test",
                        MajorFunc = (text) => text.Length > 0,
                        ArgumentsFunc = (text, arm) => arm.All(text => text.Length > 0),
                    });
                })
                .CheckFor(r => r.Str().Contains("hi").StartsWith("h", "."))
                .CheckFor(r => r.Str().Contains("hi").StartsWith("h", ".").SetStringComparison(StringComparison.InvariantCultureIgnoreCase))
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", "."))
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", ".").SetStringComparison(StringComparison.InvariantCultureIgnoreCase))
                .EqualsTo(r => r.StartsWith("h").Contains("168").EndsWith("1"))
                .EqualsTo(r => r.Hex())
                .EqualsTo(r => r.Int())
                .EqualsTo(r => r.Str().UseCustomMethod("test"))
                .EqualsTo(r => r.Ip().Str());

            var test = new AssertionBlock()
                .Assert(testBlock, new AssertionSettings { Name = "Test" });

            var result = await test.Proccess(exampleText).ConfigureAwait(false);

            Console.ReadKey();
        }
    }
}