using Analytics.Handlers;
using Analytics.Handlers.Abstractions;
using Analytics.Handlers.Handlers.MethodsHandler;
using Analytics.Handlers.Handlers.MethodsInfosHandler;
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

        public static readonly IEnumerable<IMethodsStorageHandler> MethodsStorageHandlers = new List<IMethodsStorageHandler>()
        {
            new RegularsMethodsStorageHandler(MethodsHandlersManager),
            new StringsMethodsStorageHandler(MethodsHandlersManager),
        };
    }
}