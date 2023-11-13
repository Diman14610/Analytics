using Analytics.Handlers.Abstractions.MethodsHandler;
using Analytics.Handlers.Exceptions;

namespace Analytics.Handlers.Implementations.MethodsHandler
{
    public class MethodsHandlersManager : IMethodsHandlersManager
    {
        private readonly IDictionary<(Type, Type), object> _methodsHandlers;

        public MethodsHandlersManager(IDictionary<(Type, Type), object> methodsHandlers)
        {
            _methodsHandlers = methodsHandlers ?? throw new ArgumentNullException(nameof(methodsHandlers));
        }

        public void Handle<TResult, TMethod>(string text, IEnumerable<TMethod> methods, ref TResult result)
        {
            if (_methodsHandlers.TryGetValue((typeof(TResult), typeof(TMethod)), out var handlerObject) && handlerObject is MethodsBaseHandler<TResult, TMethod> handler)
            {
                handler.Handle(text, methods, ref result);
            }
            else
            {
                throw new HandlerNotFoundException($"Handler not found for ({typeof(TResult)}, {typeof(TMethod)}).");
            }
        }
    }
}
