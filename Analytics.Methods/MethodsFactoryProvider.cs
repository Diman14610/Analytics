using Analytics.Configuration;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public class MethodsFactoryProvider : MethodsFactory
    {
        public MethodsFactoryProvider(
            MajorMethods majorMethods,
            MethodsWithArguments methodsWithArguments,
            AnalyticsConfigurationProvider configurationProvider) : base(majorMethods, methodsWithArguments, configurationProvider) { }

        public MethodsFactoryStruct GetSelectedMethods() => SelectedMethods;
    }
}
