using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers
{
    public class MajorMethodsCheckHandler : BaseHandler<CheckResult, MajorMethodInfo>
    {
        public override void Handle(string text, IEnumerable<MajorMethodInfo> methods, ref CheckResult result)
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
