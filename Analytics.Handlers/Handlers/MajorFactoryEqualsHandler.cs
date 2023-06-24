using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class MajorFactoryEqualsHandler : BaseHandler<EqualsResult, MajorFactoryMethodInfo>
    {
        public MajorFactoryEqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override EqualsResult Handle(string text, IEnumerable<MajorFactoryMethodInfo> funks)
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
                    });
                }

                IEnumerable<bool> matchesWithText = funks.Select(method => method.Func(text));

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
