using Analytics.Shared.Analytics;
using Analytics.Shared.Core.Analytics;
using Analytics.Shared.Methods;

namespace Analytics.Handlers.Handlers
{
    public class MajorMethodsEqualsHandler : BaseHandler<EqualsResult, MajorMethodInfo>
    {
        public override void Handle(string text, IEnumerable<MajorMethodInfo> methods, ref EqualsResult result)
        {
            foreach (var item in methods)
            {
                var methodInfo = new ExtendedMethodInfo
                {
                    MethodName = item.MethodName
                };

                try
                {
                    methodInfo.IsEqual = item.Func(text);
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
