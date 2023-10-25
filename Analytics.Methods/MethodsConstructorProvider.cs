using Analytics.Configuration;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public sealed class MethodsConstructorProvider : MethodsConstructor
    {
        public MethodsConstructorProvider(
            MajorMethods majorMethods,
            MethodsWithArguments methodsWithArguments,
            AnalyticsConfigurationProvider configurationProvider) : base(majorMethods, methodsWithArguments, configurationProvider) { }

        public MethodsStruct GetSelectedMethods() => SelectedMethods;
    }
}
