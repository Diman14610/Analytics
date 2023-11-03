using Analytics.Handlers.Abstractions.MethodsHandler;
using Analytics.Handlers.Abstractions.MethodsStorageHandler;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Implementations.MethodsStorageHandler
{
    public class StringsMethodsStorageHandler : MethodsStorageBaseHandler
    {
        public StringsMethodsStorageHandler(IMethodsHandlersManager methodsHandlersManager) : base(methodsHandlersManager)
        {
        }

        public override void Handle<ResultType>(string text, MethodsStorage selectedMethods, ref ResultType result)
        {
            if (selectedMethods.StringsMethodsInfos.Count == 0) return;
            _methodsHandlersManager.Handle(text, selectedMethods.StringsMethodsInfos, ref result);
        }
    }
}
