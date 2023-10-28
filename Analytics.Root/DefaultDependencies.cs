using Analytics.Handlers.Abstractions.AnalyticsResultHandler;
using Analytics.Handlers.Abstractions.MethodsStorageHandler;
using Analytics.Handlers.Abstractions.ResultHandler;
using Analytics.Handlers.Handlers.AnalyticsResultHandler;
using Analytics.Handlers.Handlers.MethodsHandler;
using Analytics.Handlers.Handlers.MethodsInfosHandler;
using Analytics.Handlers.Handlers.ResultHandler;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Root
{
    public static class DefaultDependencies
    {
        public static readonly MethodsHandlersManager MethodsHandlersManager = new(new Dictionary<(Type, Type), object>()
        {
            [(typeof(EqualsResult), typeof(StringMethodInfo))] = new StringMethodsHandlerByEquals(),
            [(typeof(CheckResult), typeof(StringMethodInfo))] = new StringMethodsHandlerByCheck(),
            [(typeof(EqualsResult), typeof(RegularMethodInfo))] = new RegularMethodsHandlerByEquals(),
            [(typeof(CheckResult), typeof(RegularMethodInfo))] = new RegularMethodsHandlerByCheck(),
        });

        public static readonly IMethodsStorageHandler MethodsStorageHandler = new MethodsStorageHandlers(new IMethodsStorageHandler[]
        {
            new RegularsMethodsStorageHandler(MethodsHandlersManager),
            new StringsMethodsStorageHandler(MethodsHandlersManager),
        });

        public static readonly IResultHandler<AnalyticsResult> AnalyticsResultHandler =
            new AnalyticsResultHandler(
                new AnalyticsResultHandlers(new IAnalyticsResultHandler[] { new EqualsResultHandler(), new CheckResultHandler(), })
                );
    }
}