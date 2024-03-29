﻿using Analytics.Handlers.Abstractions.MethodsHandler;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Abstractions.MethodsStorageHandler
{
    public abstract class MethodsStorageBaseHandler : IMethodsStorageHandler
    {
        protected readonly IMethodsHandlersManager _methodsHandlersManager;

        public MethodsStorageBaseHandler(IMethodsHandlersManager methodsHandlersManager)
        {
            _methodsHandlersManager = methodsHandlersManager ?? throw new ArgumentNullException(nameof(methodsHandlersManager));
        }

        public abstract void Handle<TResultType>(string text, MethodsStorage selectedMethods, ref TResultType result);
    }
}
