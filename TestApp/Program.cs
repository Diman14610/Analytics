﻿using Analytics;
using Analytics.Configuration;
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
            var handlers = new Dictionary<(Type, Type), object>()
            {
                [(typeof(EqualsResult), typeof(ArgumentsMethodInfo))] = new ArgumentsMethodsEqualsHandler(),
                [(typeof(CheckResult), typeof(ArgumentsMethodInfo))] = new ArgumentsMethodsCheckHandler(),
                [(typeof(EqualsResult), typeof(MajorMethodInfo))] = new MajorMethodsEqualsHandler(),
                [(typeof(CheckResult), typeof(MajorMethodInfo))] = new MajorMethodsCheckHandler(),
            };

            var analytics = new AnalyticsFactory(new HandlersManager(handlers))
                .EqualsTo(r => r.Str().Contains("hi").StartsWith("h", "z"))
                .EqualsTo(r => r.StartsWith("h").Contains("168").EndsWith("1"))
                .EqualsTo(r => r.Hex())
                .EqualsTo(r => r.Int())
                .EqualsTo(r => r.Ip().Str());

            analytics.Configuration.AddMethod(new CustomMethodsBuilder().SetMethodName("test").SetMethod((text) => text.Length > 5));

            AnalyticsResult analyticsResult = analytics.Analysis("hi");


            Console.ReadKey();
        }
    }
}