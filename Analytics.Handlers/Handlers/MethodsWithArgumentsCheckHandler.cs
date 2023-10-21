using Analytics.Shared.Analytics;

namespace Analytics.Handlers.Handlers
{
    public class MethodsWithArgumentsCheckHandler : BaseHandler<CheckResult, ArgumentsMethodInfo>
    {
        public override void Handle(string text, IEnumerable<ArgumentsMethodInfo> methods, ref CheckResult result)
        {
            foreach (var item in methods)
            {
                var check = new ExtendedMethodInfo
                {
                    MethodName = item.MethodName,
                    Arguments = item.Arguments
                };

                try
                {
                    check.IsEqual = item.Func(text, (string[])item.Arguments);
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
