using Analytics.Handlers.Exceptions;
using Analytics.Handlers.Handlers;

namespace Analytics.Handlers
{
    public class HandlersManager : IHandlersManager
    {
        private readonly IDictionary<Type, object> _handlers;

        public HandlersManager(IDictionary<Type, object> handlers)
        {
            _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public T Handle<T>(IEnumerable<string> methods, string text)
        {
            BaseHandler<T>? handler = _handlers[typeof(T)] as BaseHandler<T>;

            if (handler == null)
            {
                throw new HandlerNotFoundException("The handler could not be found in the list of handlers.");
            }
            if (handler.Type != typeof(T))
            {
                throw new HandlerNotMatchException($"The received handler does not match the type: ${typeof(T)}.");
            }

            return handler.Handle(methods, text);
        }
    }
}
