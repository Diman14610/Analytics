using Analytics.Handlers.Exceptions;
using Analytics.Handlers.Handlers;
using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers
{
    public class HandlersManager : IHandlersManager
    {
        private readonly IMethodsList _methodsList;

        private readonly Dictionary<Type, object> _handlers;

        public HandlersManager(IMethodsList methodsList)
        {
            _methodsList = methodsList ?? throw new ArgumentNullException(nameof(methodsList));

            _handlers = new Dictionary<Type, object>()
            {
                [typeof(EqualsResult)] = new EqualsHandler(_methodsList),
                [typeof(CheckResult)] = new CheckHandler(_methodsList),
            };
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
