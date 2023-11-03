using Analytics.Shared.Methods;

namespace Analytics.Handlers.Abstractions.MethodsStorageHandler
{
    public interface IMethodsStorageHandler
    {
        void Handle<TResultType>(string text, MethodsStorage selectedMethods, ref TResultType result);
    }
}
