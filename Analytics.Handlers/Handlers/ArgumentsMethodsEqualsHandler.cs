using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class ArgumentsMethodsEqualsHandler : BaseHandler<EqualsResult, ArgumentsMethodInfo>
    {
        public override void Handle(string text, IEnumerable<ArgumentsMethodInfo> funks, ref EqualsResult result)
        {
            foreach (var item in funks)
            {
                var methodInfo = new ExtendedMethodInfo();

                try
                {
                    methodInfo.MethodName = item.MethodName;
                    methodInfo.IsEqual = item.Func(text, (string[])item.Arguments);
                    methodInfo.Arguments = item.Arguments;
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
