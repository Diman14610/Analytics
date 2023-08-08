using Analytics.Shared.Methods;

namespace Analytics.Methods
{
    public interface IMethodsFactoryProvider
    {
        MethodsFactoryStruct GetSelectedMethods();
    }
}
