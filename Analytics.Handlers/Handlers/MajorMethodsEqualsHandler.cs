using Analytics.Methods;
using Analytics.Shared;
using Analytics.Shared.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers
{
    public class MajorMethodsEqualsHandler : BaseHandler<EqualsResult, MajorMethodInfo>
    {
        public override void Handle(string text, IEnumerable<MajorMethodInfo> funks, ref EqualsResult result)
        {
            foreach (var item in funks)
            {
                var methodInfo = new ExtendedMethodInfo();

                try
                {
                    methodInfo.MethodName = item.MethodName;
                    methodInfo.IsEqual = item.Func(text);
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
