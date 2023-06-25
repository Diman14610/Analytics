using Analytics;
using Analytics.Core;
using Analytics.Handlers;
using Analytics.Handlers.Handlers;
using Analytics.Methods;
using Analytics.Shared;
using System.Collections.Concurrent;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMethodsList methodManager = new MethodsManager();
            var handlers = new Dictionary<(Type, Type), object>()
            {
                [(typeof(EqualsResult), typeof(TextFactoryMethodInfo))] = new TextFactoryEqualsHandler(methodManager),
                [(typeof(CheckResult), typeof(TextFactoryMethodInfo))] = new TextFactoryCheckHandler(methodManager),
                [(typeof(EqualsResult), typeof(MajorFactoryMethodInfo))] = new MajorFactoryEqualsHandler(methodManager),
                [(typeof(CheckResult), typeof(MajorFactoryMethodInfo))] = new MajorFactoryCheckHandler(methodManager),
            };

            var s1 = new AnalyticsFactory(new HandlersManager(handlers))
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", "z"))
                .EqualsTo(r => r.StartsWith("h").Contains("168").EndsWith("1"))
                .EqualsTo(r => r.Hex())
                .EqualsTo(r => r.Int())
                .EqualsTo(r => r.Ip().Str());

            var a = s1.Analysis("hi");

            //var asdsad = new ConcurrentBag<AnalyticsResult>();
            //new string[] { "192.168.20.1", "#ff", "----" }.AsParallel().ForAll(a =>
            //{
            //    Thread.Sleep(1000);
            //    asdsad.Add(s1.Analysis(a));
            //    Console.WriteLine(a);
            //});

            Console.ReadKey();
        }
    }
}