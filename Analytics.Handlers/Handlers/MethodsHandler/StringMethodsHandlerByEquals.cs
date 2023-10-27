using Analytics.Handlers.Abstractions;
using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;

namespace Analytics.Handlers.Handlers.MethodsHandler
{
    public class StringMethodsHandlerByEquals : MethodsBaseHandler<EqualsResult, StringMethodInfo>
    {
        public override void Handle(string text, IEnumerable<StringMethodInfo> methods, ref EqualsResult result)
        {
            foreach (var method in methods)
            {
                var methodInfo = new ExtendedMethodInfo
                {
                    MethodName = method.MethodName,
                    Arguments = method.Arguments
                };

                try
                {
                    methodInfo.IsEqual = method.Func(text, (string[])method.Arguments);
                }
                catch (Exception ex)
                {
                    result.IsError = true;
                    result.Exception = ex;
                    methodInfo.IsError = true;
                    methodInfo.Exception = ex;
                }

                result.ExtendedMethodInfos.Add(methodInfo);
            }
        }
    }
}
