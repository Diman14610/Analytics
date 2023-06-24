using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class TextFactoryEqualsHandler : BaseHandler<EqualsResult, TextFactoryMethodInfo>
    {
        public TextFactoryEqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override EqualsResult Handle(string text, IEnumerable<TextFactoryMethodInfo> funks)
        {
            var result = new EqualsResult();
            var methodInfos = new List<ExtendedMethodInfo>();

            try
            {
                foreach (var item in funks)
                {
                    methodInfos.Add(new ExtendedMethodInfo()
                    {
                        MethodName = item.MethodName,
                        Arguments = item.Arguments,
                    });
                }

                IEnumerable<bool> matchesWithText = funks.Select(method => method.Func(text, (string[])method.Arguments));

                result.IsEqual = matchesWithText.All(m => m);
                result.ExtendedMethodInfos = methodInfos;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Exception = ex;
            }

            return result;
        }
    }
}
