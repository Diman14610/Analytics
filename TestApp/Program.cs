using Analytics;
using Analytics.Core;
using Analytics.Handlers;
using Analytics.Methods;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s1 = new AnalyticsFactory(new HandlersManager(new MethodsManager()));
            var t2 = s1
                .CheckFor(r => r.Imsi, r => r.Hex, r => r.Str)
                .EqualsTo(r => r.Hex)
                .EqualsTo(r => r.Int)
                .Analysis("#fff");

            Console.ReadKey();
        }
    }
}