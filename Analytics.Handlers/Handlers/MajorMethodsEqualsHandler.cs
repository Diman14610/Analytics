using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class MajorMethodsEqualsHandler : BaseHandler<EqualsResult, MajorMethodInfo>
    {
        public override void Handle(string text, IEnumerable<MajorMethodInfo> funks, ref EqualsResult result)
        {
            var methodInfo = new ExtendedMethodInfo();

            foreach (var item in funks)
            {
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
