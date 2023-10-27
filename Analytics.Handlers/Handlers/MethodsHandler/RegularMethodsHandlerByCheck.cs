using Analytics.Handlers.Abstractions;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers.MethodsHandler
{
    public class RegularMethodsHandlerByCheck : MethodsBaseHandler<CheckResult, RegularMethodInfo>
    {
        public override void Handle(string text, IEnumerable<RegularMethodInfo> methods, ref CheckResult result)
        {
            foreach (var method in methods)
            {
                var check = new ExtendedMethodInfo
                {
                    MethodName = method.MethodName
                };

                try
                {
                    check.IsEqual = method.Func(text);
                }
                catch (Exception ex)
                {
                    check.IsError = true;
                    check.Exception = ex;
                }

                result.ExtendedMethodInfos.Add(check);
            }
        }
    }
}
