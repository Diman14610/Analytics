using Analytics.Methods;

namespace Analytics.Core
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
                Func<string, bool>? getedMethod = _methodsList.TryGetMethod(method);

                var checkResult = new MethodInfo
                {
                    MethodName = method,
                    IsMethodFound = getedMethod != null
                };

                try
                {
                    checkResult.IsEqual = getedMethod != null && getedMethod(text);
                }
                catch (Exception ex)
                {
                    checkResult.IsEqual = false;
                    checkResult.IsError = true;
                    checkResult.Exception = ex;
                }

                result.Add(checkResult);
            }

            return new CheckResult() { MethodInfos = result };
        }
    }
}