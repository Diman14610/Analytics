using Analytics.Handlers.Abstractions;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers.MethodsHandler
{
    public class RegularMethodsHandlerByEquals : MethodsBaseHandler<EqualsResult, RegularMethodInfo>
    {
        public override void Handle(string text, IEnumerable<RegularMethodInfo> methods, ref EqualsResult result)
        {
            foreach (var method in methods)
            {
                var methodInfo = new ExtendedMethodInfo
                {
                    MethodName = method.MethodName
                };

                try
                {
                    methodInfo.IsEqual = method.Func(text);
                }
                catch (Exception ex)
                {
                    methodInfo.IsError = true;
                    methodInfo.Exception = ex;
                }

                result.ExtendedMethodInfos.Add(methodInfo);
            }
        }
    }
}
