using Analytics.Shared.Analytics;

namespace Analytics.Handlers.Handlers
{
    public class MethodsWithArgumentsEqualsHandler : BaseHandler<EqualsResult, ArgumentsMethodInfo>
    {
        public override void Handle(string text, IEnumerable<ArgumentsMethodInfo> methods, ref EqualsResult result)
        {
            foreach (var item in methods)
            {
                var methodInfo = new ExtendedMethodInfo
                {
                    MethodName = item.MethodName,
                    Arguments = item.Arguments
                };

                try
                {
                    methodInfo.IsEqual = item.Func(text, (string[])item.Arguments);
                }
                catch (Exception ex)
                {
                    result.IsError = true;
                    result.Exception = ex;
                }

                result.ExtendedMethodInfos.Add(methodInfo);
            }
        }
    }
}
