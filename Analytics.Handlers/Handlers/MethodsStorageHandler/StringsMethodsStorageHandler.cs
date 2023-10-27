﻿using Analytics.Handlers.Abstractions;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers.MethodsInfosHandler
{
    public class StringsMethodsStorageHandler : MethodsStorageBaseHandler
    {
        public StringsMethodsStorageHandler(IMethodsHandlersManager handlersManager) : base(handlersManager)
        {
        }

        public override void Handle<ResultType>(string text, MethodsStorage selectedMethods, ref ResultType result)
        {
            if (selectedMethods.StringsMethodsInfos.Count == 0) return;
            _methodsHandlersManager.Handle(text, selectedMethods.StringsMethodsInfos, ref result);
        }
    }
}
