using Analytics;
using Analytics.Core;
using Analytics.Handlers;
using Analytics.Methods;
using Analytics.Shared;
using System.Collections.Concurrent;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s1 = new AnalyticsFactory(new HandlersManager(new MethodsManager()))
                .CheckFor(r => r.Imsi, r => r.Hex, r => r.Str)
                .EqualsTo(r => r.Hex)
                .EqualsTo(r => r.Int)
                .EqualsTo(r => r.Ip, r => r.Str);

            var asdsad = new ConcurrentBag<AnalyticsResult>();
            new string[] { "192.168.20.1", "#ff", "----" }.AsParallel().ForAll(a =>
            {
                Thread.Sleep(1000);
                asdsad.Add(s1.Analysis(a));
                Console.WriteLine(a);
            });

            Console.ReadKey();
        }
    }
}