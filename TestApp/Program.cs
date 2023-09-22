using Analytics;
using Analytics.Handlers;
using Analytics.Handlers.Handlers;
using Analytics.Shared.Analytics;
using Analytics.Shared.Configuration;
using Analytics.Shared.Methods;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var handlers = new Dictionary<(Type, Type), object>()
            {
                [(typeof(EqualsResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsEqualsHandler(),
                [(typeof(CheckResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsCheckHandler(),
                [(typeof(EqualsResult), typeof(MajorMethodInfo))] = new MajorMethodsEqualsHandler(),
                [(typeof(CheckResult), typeof(MajorMethodInfo))] = new MajorMethodsCheckHandler(),
            };
            var handlersManager = new HandlersManager(handlers);

            var analytics = new AnalyticsFactory(handlersManager)
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