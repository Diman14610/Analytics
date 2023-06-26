using Analytics.Handlers.Exceptions;
using Analytics.Handlers.Handlers;
using Analytics.Shared;
using System;

namespace Analytics.Handlers
{
    public class HandlersManager : IHandlersManager
    {
        private readonly IDictionary<(Type, Type), object> _handlers;

        public HandlersManager(IDictionary<(Type, Type), object> handlers)
        {
            _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public void Handle<T, U>(string text, IEnumerable<U> funks, ref T result)
        {
            BaseHandler<T, U>? handler = GetHandler<T, U>();

            CheckCorrectOfHandler(handler);

            handler!.Handle(text, funks, ref result);
        }

        protected BaseHandler<T, U>? GetHandler<T, U>()
        {
            return _handlers[(typeof(T), typeof(U))] as BaseHandler<T, U>;
        }

        protected void CheckCorrectOfHandler<T, U>(BaseHandler<T, U>? handler)
        {
            if (handler == null)
            {
                throw new HandlerNotFoundException("The handler could not be found in the list of handlers.");
            }
            if (handler.ReturnType != typeof(T))
            {
                throw new HandlerNotMatchException($"The received handler does not match the type: ${typeof(T)}.");
            }
            if (handler.FunctionType != typeof(U))
            {
                throw new HandlerNotMatchException($"The received handler does not match the function type: ${typeof(U)}.");
            }
        }
    }
}
