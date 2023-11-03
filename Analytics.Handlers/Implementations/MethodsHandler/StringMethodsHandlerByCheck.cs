using Analytics.Handlers.Abstractions.MethodsHandler;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;

namespace Analytics.Handlers.Implementations.MethodsHandler
{
    public class StringMethodsHandlerByCheck : MethodsBaseHandler<CheckResult, StringMethodInfo>
    {
        public override void Handle(string text, IEnumerable<StringMethodInfo> methods, ref CheckResult result)
        {
            foreach (var method in methods)
            {
                var check = new ExtendedMethodInfo
                {
                    MethodName = method.MethodName,
                    Arguments = method.Arguments
                };

                try
                {
                    check.IsEqual = method.Func(text, (string[])method.Arguments);
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
