using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class CheckHandler : BaseHandler<CheckResult>
    {
        public CheckHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override CheckResult Handle(IEnumerable<string> methods, string text)
        {
            var result = new List<MethodInfo>();

            foreach (var method in methods)
            {
                var checkResult = new MethodInfo
                {
                    MethodName = method,
                };

                try
                {
                    Func<string, bool>? getedMethod = _methodsList.TryGetMethod(method);

                    checkResult.IsMethodFound = getedMethod != null;

                    if (!checkResult.IsMethodFound) continue;

                    checkResult.IsEqual = getedMethod != null && getedMethod(text);
                }
                catch (Exception ex)
                {
                    checkResult.IsError = true;
                    checkResult.Exception = ex;
                }

                result.Add(checkResult);
            }

            return new CheckResult() { MethodInfos = result };
        }

        public override CheckResult Handle(string text, IEnumerable<TextFactoryMethodInfo> funks)
        {
            var result = new List<TextMethodInfo>();

            foreach (var item in funks)
            {
                var check = new TextMethodInfo()
                {
                    MethodName = item.MethodName,
                };

                try
                {
                    check.IsEqual = item.Func(text, (string[])item.Arguments);
                    check.Arguments = item.Arguments;
                }
                catch (Exception ex)
                {
                    check.IsError = true;
                    check.Exception = ex;
                }

                result.Add(check);
            }

            return new CheckResult() { TextMethodInfos = result };
        }
    }
}
