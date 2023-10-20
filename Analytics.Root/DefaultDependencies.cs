using Analytics.Configuration;
using Analytics.Handlers;
using Analytics.Handlers.Handlers;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Root
{
    public static class DefaultDependencies
    {
        private static readonly MajorMethods _majorMethods = new();
        private static readonly MethodsWithArguments methodsWithArguments = new();

        public static IHandlersManager GetHandlersManager()
        {
            var handlersManager = new HandlersManager(new Dictionary<(Type, Type), object>()
            {
                [(typeof(EqualsResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsEqualsHandler(),
                [(typeof(CheckResult), typeof(ArgumentsMethodInfo))] = new MethodsWithArgumentsCheckHandler(),
                [(typeof(EqualsResult), typeof(MajorMethodInfo))] = new MajorMethodsEqualsHandler(),
                [(typeof(CheckResult), typeof(MajorMethodInfo))] = new MajorMethodsCheckHandler(),
            });

            return handlersManager;
        }

        public static AnalyticsConfigurationProvider GetAnalyticsConfigurationProvider()
        {
            return new AnalyticsConfigurationProvider();
        }

        public static MajorMethods GetMajorMethods()
        {
            return _majorMethods;
        }

        public static MethodsWithArguments GetMethodsWithArguments()
        {
            return methodsWithArguments;
        }
    }
}