using Analytics.Shared.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers
{
    public class MajorMethodsCheckHandler : BaseHandler<CheckResult, MajorMethodInfo>
    {
        public override void Handle(string text, IEnumerable<MajorMethodInfo> funks, ref CheckResult result)
        {
            foreach (var item in funks)
            {
                var check = new ExtendedMethodInfo
                {
                    MethodName = item.MethodName
                };

                try
                {
                    check.IsEqual = item.Func(text);
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
