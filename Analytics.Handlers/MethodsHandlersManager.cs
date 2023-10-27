using Analytics.Handlers.Abstractions;
using Analytics.Handlers.Exceptions;

namespace Analytics.Handlers
{
    public class MethodsHandlersManager : IMethodsHandlersManager
    {
        private readonly IDictionary<(Type, Type), object> _methodsHandlers;

        public MethodsHandlersManager(IDictionary<(Type, Type), object> methodsHandlers)
        {
            _methodsHandlers = methodsHandlers ?? throw new ArgumentNullException(nameof(methodsHandlers));
        }

        public void Handle<T, U>(string text, IEnumerable<U> methods, ref T result)
        {
            if (_methodsHandlers.TryGetValue((typeof(T), typeof(U)), out var handlerObject) && handlerObject is MethodsBaseHandler<T, U> handler)
            {
                handler.Handle(text, methods, ref result);
            }
            else
            {
                throw new HandlerNotFoundException($"Handler not found for ({typeof(T)}, {typeof(U)}).");
            }
        }
    }
}
