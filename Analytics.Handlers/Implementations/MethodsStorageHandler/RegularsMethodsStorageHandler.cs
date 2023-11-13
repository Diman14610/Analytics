using Analytics.Handlers.Abstractions.MethodsHandler;
using Analytics.Handlers.Abstractions.MethodsStorageHandler;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Implementations.MethodsStorageHandler
{
    public class RegularsMethodsStorageHandler : MethodsStorageBaseHandler
    {
        public RegularsMethodsStorageHandler(IMethodsHandlersManager methodsHandlersManager) : base(methodsHandlersManager)
        {
        }

        public override void Handle<TResultType>(string text, MethodsStorage selectedMethods, ref TResultType result)
        {
            if (selectedMethods.RegularsMethodsInfos.Count == 0) return;
            _methodsHandlersManager.Handle(text, selectedMethods.RegularsMethodsInfos, ref result);
        }
    }
}
