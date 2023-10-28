using Analytics.Handlers.Abstractions.MethodsHandler;
using Analytics.Handlers.Abstractions.MethodsStorageHandler;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers.MethodsInfosHandler
{
    public class RegularsMethodsStorageHandler : MethodsStorageBaseHandler
    {
        public RegularsMethodsStorageHandler(IMethodsHandlersManager handlersManager) : base(handlersManager)
        {
        }

        public override void Handle<ResultType>(string text, MethodsStorage selectedMethods, ref ResultType result)
        {
            if (selectedMethods.RegularsMethodsInfos.Count == 0) return;
            _methodsHandlersManager.Handle(text, selectedMethods.RegularsMethodsInfos, ref result);
        }
    }
}
