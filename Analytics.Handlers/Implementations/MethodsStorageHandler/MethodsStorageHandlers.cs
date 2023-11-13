using Analytics.Handlers.Abstractions.MethodsStorageHandler;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Implementations.MethodsStorageHandler
{
    public class MethodsStorageHandlers : IMethodsStorageHandler
    {
        private readonly IEnumerable<IMethodsStorageHandler> _methodsStorageHandlers;

        public MethodsStorageHandlers(IEnumerable<IMethodsStorageHandler> methodsStorageHandlers)
        {
            _methodsStorageHandlers = methodsStorageHandlers ?? throw new ArgumentNullException(nameof(methodsStorageHandlers));
        }

        public void Handle<TResultType>(string text, MethodsStorage selectedMethods, ref TResultType result)
        {
            foreach (var handler in _methodsStorageHandlers)
            {
                handler.Handle(text, selectedMethods, ref result);
            }
        }
    }
}
