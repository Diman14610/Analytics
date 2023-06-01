using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    internal class CheckHandler : BaseHandler<CheckResult>
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
    }
}
