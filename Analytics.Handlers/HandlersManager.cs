using Analytics.Handlers.Exceptions;
using Analytics.Handlers.Handlers;

namespace Analytics.Handlers
{
    public class HandlersManager : IHandlersManager
    {
        private readonly IDictionary<(Type, Type), object> _handlers;

        public HandlersManager(IDictionary<(Type, Type), object> handlers)
        {
            _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public void Handle<T, U>(string text, IEnumerable<U> methods, ref T result)
        {
            if (_handlers.TryGetValue((typeof(T), typeof(U)), out var handlerObject) && handlerObject is BaseHandler<T, U> handler)
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
