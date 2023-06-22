﻿using Analytics.Handlers.Exceptions;
using Analytics.Handlers.Handlers;
using Analytics.Shared;

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
            BaseHandler<T>? handler = GetHandler<T>();

            CheckCorrectOfHandler(handler);

            return handler!.Handle(methods, text);
        }

        public T Handle<T>(string text, IEnumerable<TextFactoryMethodInfo> funks)
        {
            BaseHandler<T>? handler = GetHandler<T>();

            CheckCorrectOfHandler(handler);

            return handler!.Handle(text, funks);
        }

        protected BaseHandler<T>? GetHandler<T>()
        {
            return _handlers[typeof(T)] as BaseHandler<T>;
        }

        protected void CheckCorrectOfHandler<T>(BaseHandler<T>? handler)
        {
            if (handler == null)
            {
                throw new HandlerNotFoundException("The handler could not be found in the list of handlers.");
            }
            if (handler.Type != typeof(T))
            {
                throw new HandlerNotMatchException($"The received handler does not match the type: ${typeof(T)}.");
            }
        }
    }
}
