using Analytics.Configuration;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public sealed class MethodsConstructorProvider : MethodsConstructor
    {
        public MethodsConstructorProvider(
            RegularMethods majorMethods,
            StringMethods methodsWithArguments,
            AnalyticsConfiguration analyticsConfiguration) : base(majorMethods, methodsWithArguments, analyticsConfiguration) { }

        public MethodsStorage GetSelectedMethods() => SelectedMethods;
    }
}
