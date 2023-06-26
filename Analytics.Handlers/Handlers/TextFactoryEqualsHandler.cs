using Analytics.Methods;
using Analytics.Shared;

namespace Analytics.Handlers.Handlers
{
    public class TextFactoryEqualsHandler : BaseHandler<EqualsResult, TextFactoryMethodInfo>
    {
        public TextFactoryEqualsHandler(IMethodsList methodsList) : base(methodsList)
        {
        }

        public override void Handle(string text, IEnumerable<TextFactoryMethodInfo> funks, ref EqualsResult result)
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
