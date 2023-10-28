using Analytics.Shared.Methods;

namespace Analytics.Handlers.Abstractions.MethodsStorageHandler
{
    public interface IMethodsStorageHandler
    {
        void Handle<ResultType>(string text, MethodsStorage selectedMethods, ref ResultType result);
    }
}
