using Analytics.Methods;

namespace Analytics.Core
{
    public class EqualsHandler : BaseHandler<EqualsResult>
    {
        public EqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override EqualsResult Handle(IEnumerable<string> methods, string text)
        {
            var result = new EqualsResult()
            {
                Methods = methods,
            };

            var matchesWithText = methods.Select(method =>
            {
                var gettedMethod = _methodsList.TryGetMethod(method);

                if (gettedMethod == null) return false;

                return gettedMethod(text);
            });

            try
            {
                result.IsEqual = matchesWithText.All(v => v);
            }
            catch (Exception ex)
            {
                result.IsEqual = false;
                result.IsError = true;
                result.Exception = ex;
            }

            return result;
        }
    }
}