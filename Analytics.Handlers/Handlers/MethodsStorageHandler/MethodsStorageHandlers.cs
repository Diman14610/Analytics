using Analytics.Handlers.Abstractions.MethodsStorageHandler;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers.MethodsStorageHandler
{
    public class MethodsStorageHandlers : IMethodsStorageHandler
    {
        private readonly IEnumerable<IMethodsStorageHandler> _methodsStorageHandlers;

        public MethodsStorageHandlers(IEnumerable<IMethodsStorageHandler> methodsStorageHandlers)
        {
            _methodsStorageHandlers = methodsStorageHandlers ?? throw new ArgumentNullException(nameof(methodsStorageHandlers));
        }

        public void Handle<ResultType>(string text, MethodsStorage selectedMethods, ref ResultType result)
        {
            foreach (var handler in _methodsStorageHandlers)
            {
                handler.Handle(text, selectedMethods, ref result);
            }
        }
    }
}
