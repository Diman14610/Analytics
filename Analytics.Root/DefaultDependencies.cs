﻿using Analytics.Handlers;
using Analytics.Handlers.Handlers;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Root
{
    public static class DefaultDependencies
    {
        private static readonly HandlersManager _handlersManager = new(new Dictionary<(Type, Type), object>()
        {
            [(typeof(EqualsResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsEqualsHandler(),
            [(typeof(CheckResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsCheckHandler(),
            [(typeof(EqualsResult), typeof(MajorMethodInfo))] = new MajorMethodsEqualsHandler(),
            [(typeof(CheckResult), typeof(MajorMethodInfo))] = new MajorMethodsCheckHandler(),
        });

        public static IHandlersManager GetHandlersManager()
        {
            return _handlersManager;
        }
    }
}