using Analytics.Configuration;
using Analytics.Methods.SharedMethods;
using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public class MethodsFactoryProvider : MethodsFactory, IMethodsFactoryProvider
    {
        public MethodsFactoryProvider(MajorMethods majorMethods, MethodsWithArguments methodsWithArguments, IConfigurationProvider configurationProvider) : base(majorMethods, methodsWithArguments, configurationProvider)
        {
        }

        public MethodsFactoryStruct GetSelectedMethods() => _selectedMethods;
    }
}
